using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [field: Header("Object References")]
    [field: SerializeField] public AudioSource AudioPrefab {get; private set;}
    [field: SerializeField] public AudioMixer Mixer {get; private set;}
    [field: SerializeField] public AudioSource MusicPlayer {get; private set;}
    [field: Header("Audio Arrays")]
    [field: SerializeField] public AudioClip[] SFXList {get; private set;}
    [field: SerializeField] public AudioClip[] MusicList {get; private set;}

    public void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(int soundIndex, Vector3 pos, float vol)
    {
        float pitchVariance = (float) (Random.value * 0.2 + 0.9);
        AudioSource source = Instantiate(AudioPrefab, pos, Quaternion.identity);
        source.clip = SFXList[soundIndex];
        source.volume = vol;
        if (soundIndex >= 3 && soundIndex <= 6)
        {
            source.pitch = pitchVariance;
        }
        source.Play();
        float length = source.clip.length;
        Destroy(source.gameObject, length);
    }

    public void PlayMusic(int musicIndex, float vol)
    {
        if (MusicPlayer.isPlaying)
        {
            MusicPlayer.Stop();
        }
        MusicPlayer.clip = MusicList[musicIndex];
        MusicPlayer.volume = vol;
        MusicPlayer.Play();
    }

    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    public void SetSFXVolume(float level)
    {
        Mixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        Mixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
}
