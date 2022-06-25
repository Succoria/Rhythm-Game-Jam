using System;
using UnityEngine;

[CreateAssetMenu(fileName ="AudioSettings", menuName ="Jam/AudioSettings")]
public class AudioSettings : ScriptableObject
{
	[Serializable]
	public class AudioClipData
	{
		public AudioClip clip;

		[Space]
		//Only one song will be used for beat detection
		public bool IsPrimary;
	}

	[Serializable]
	public class Song
	{
		public AudioClipData[] clips;
		
		[Space]
		public float Bpm;
		public float DeplayBeforePlaySeconds;
		public float StartBeatDelaySeconds;
	}

	public Song[] Songs;
}
