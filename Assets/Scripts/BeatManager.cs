using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BeatManager : MonoBehaviour 
{
	public AudioSettings AudioSettings;
	public AudioMixerGroup AudioMixerGroup;

	public float BeatsPerSecond => currentBps;

	private AudioSettings.Song currentSong;
	private List<ActiveAudioData> currentAudioData;
	private float currentBps;
	private int lastBeatSample;
	private float currentFrequencyInverse;

	long beatCount = 0;

	public event Action<long> OnBeat = delegate { };
	
	public class ActiveAudioData
	{
		public AudioSource source;
		public AudioLowPassFilter audioLowPassFilter;
		public bool IsPrimary;
	}

	void Start ()
	{
		Debug.Assert(AudioSettings.Songs.Length > 0, "[BeatManager] no songs in audiosettings");
		PlaySong(AudioSettings.Songs[0], false, true);
	}
	
	Coroutine last = null;
	public void MuteFor(float seconds)
	{
		if(last != null)
		{
			StopCoroutine(last);
		}
		last = StartCoroutine(PMuteFor(seconds));
	}

	private IEnumerator PMuteFor(float seconds)
	{
		ActiveAudioData a = PrimaryAudioData();
		if (a != null)
		{
			a.audioLowPassFilter.cutoffFrequency = 300;

			yield return new WaitForSeconds(seconds);

			while (a.audioLowPassFilter.cutoffFrequency < 22000)
			{
				yield return null;
				a.audioLowPassFilter.cutoffFrequency += Time.deltaTime * 10000;
			}
		}
	}

	public void SetAudioSettings(AudioSettings settings)
	{
		AudioSettings = settings;
		PlaySong(AudioSettings.Songs[0], false, true);
	}

	private void PlaySong(AudioSettings.Song song, bool swap, bool loop)
	{
		AudioSource[] oldSources = gameObject.GetComponents<AudioSource>();

		int previousSamples = 0;
		if (oldSources.Length > 0)
		{
			previousSamples = oldSources[0].timeSamples;
		}
	
		foreach (AudioSource src in oldSources)
		{
			src.enabled = false;
			Destroy(src);
		}

		//initialize
		currentAudioData = new List<ActiveAudioData>();
		bool primarySet = false;
		currentSong = song;

		foreach(AudioSettings.AudioClipData clip in song.clips)
		{
			ActiveAudioData data = new ActiveAudioData();

			data.source = gameObject.AddComponent<AudioSource>();
			data.source.clip = clip.clip;
			data.source.outputAudioMixerGroup = AudioMixerGroup;
			data.source.loop = true;

			data.audioLowPassFilter =  gameObject.GetComponent<AudioLowPassFilter>();
			if (data.audioLowPassFilter == null)
			{
				data.audioLowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
			}
			
			data.audioLowPassFilter.cutoffFrequency = 22000;
			
			data.IsPrimary = clip.IsPrimary;
			currentAudioData.Add(data);
		}

		//audio does not care about time scale afaik? 
		ActiveAudioData primary = PrimaryAudioData();

		currentBps = song.Bpm / 60f;
		currentFrequencyInverse = 1f / primary.source.clip.frequency;

		if (swap)
		{
			for (var index = 0; index < currentAudioData.Count; index++)
			{
				ActiveAudioData data = currentAudioData[index];
				data.source.Play();
				data.source.timeSamples = previousSamples;
			}
		}
		else if (loop)
		{
			for (var index = 0; index < currentAudioData.Count; index++)
			{
				ActiveAudioData data = currentAudioData[index];
				data.source.Play();
			}

			lastBeatSample = PrimaryAudioData().source.timeSamples;
		}
		else
		{
			StartCoroutine(PlayAfterDelay(song.DeplayBeforePlaySeconds));
		}
	}

	private IEnumerator PlayAfterDelay(float delay)
	{
		if (delay > 0)
		{
			yield return new WaitForSeconds(delay);
		}

		for (var index = 0; index < currentAudioData.Count; index++)
		{
			ActiveAudioData data = currentAudioData[index];
			data.source.Play();
		}

		lastBeatSample = PrimaryAudioData().source.timeSamples;
		beatCount = 0;
	}

	public ActiveAudioData PrimaryAudioData()
	{
		foreach(ActiveAudioData a in currentAudioData)
		{
			if(a == null)
			{
				continue;
			}
			if(a.IsPrimary)
			{
				return a;
			}
		}

		Debug.LogError("No primary audio data");
		return null;
	}

	private void Update()
	{
		//check beat against bps to see if its a primary beat
		ActiveAudioData a = PrimaryAudioData();
		if (a.source.isPlaying)
		{
			double secondsPerBeat = 1f / currentBps;
			int currentSamples = a.source.timeSamples;

			if (currentSamples < lastBeatSample)
			{
				lastBeatSample = lastBeatSample - a.source.clip.samples;
			}
			
			currentSamples = Mathf.Max(0, currentSamples - Mathf.FloorToInt(a.source.clip.frequency * currentSong.StartBeatDelaySeconds));
			if ((currentSamples - lastBeatSample) * currentFrequencyInverse > secondsPerBeat)
			{
				//we subtract the sample amount we are behind by so the time does not drift (since update will never run exactly on beat)
				int samplesPerBeat = (int)(a.source.clip.frequency * secondsPerBeat);
				int timeOverLastBeat = currentSamples % samplesPerBeat;

				int exactCurrentBeatTime = currentSamples - timeOverLastBeat;

				lastBeatSample = exactCurrentBeatTime;
				beatCount++;

				OnBeat(beatCount);
			}
		}
		else
		{
			//PlaySong(currentSong, false, true);
		}
	}

	public float GetAccuracyNow()
	{
		//check beat against bps to see if its a primary beat
		ActiveAudioData a = PrimaryAudioData();
		float accuracy = 1;
		if (a.source.isPlaying)
		{
			double secondsPerBeat = 1f / currentBps;
			int currentSamples = a.source.timeSamples;
			currentSamples = Mathf.Max(0, currentSamples - Mathf.FloorToInt(currentSong.clips[0].clip.frequency * currentSong.StartBeatDelaySeconds));
			int samplesPerBeat = (int)(a.source.clip.frequency * secondsPerBeat);
			int timeOverLastBeat = currentSamples % samplesPerBeat;

			float halfSamplesPerBeat = samplesPerBeat / 2f;
			float distToCenter = Mathf.Abs(halfSamplesPerBeat - timeOverLastBeat);
			accuracy = distToCenter / halfSamplesPerBeat;
		}
		
		return accuracy;
	}
}
