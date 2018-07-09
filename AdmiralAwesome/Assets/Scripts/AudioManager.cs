using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
    public String[] backgroundMusic;

    private int curBackgroundTrack = -1;
    private AudioSource curMusic;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

            if (s.mixerGroup == null)
            {
                s.source.outputAudioMixerGroup = mixerGroup;
            } else
            {
                s.source.outputAudioMixerGroup = s.mixerGroup;
            }
		}

        PlayBackgroundMusic();
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		s.source.Play();
	}

    private void Update()
    {
        if (!curMusic.isPlaying)
        {
            PlayBackgroundMusic();
        }
    }

    public void PlayBackgroundMusic()
    {
        int i = 0;
        if (backgroundMusic.Length > 1)
        {
            i = UnityEngine.Random.Range(0, backgroundMusic.Length);
            if (i == curBackgroundTrack)
            {
                PlayBackgroundMusic();
                return;
            }
        }
        curBackgroundTrack = i;
        String sound = backgroundMusic[curBackgroundTrack];
        Sound s = Array.Find(sounds, item => item.name == sound);
        curMusic = s.source;
        Play(backgroundMusic[curBackgroundTrack]);
    }

}
