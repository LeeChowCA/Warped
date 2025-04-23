using UnityEngine;

public class SoundManager:MonoBehaviour
{
    [SerializeField] private AudioSource playerSfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource eggTurretSfxSource;

    
    [SerializeField] private AudioClip musicTrack;

    private void Awake()
    {
        PlayMusic(musicTrack);
    }

    public void PlayPlayerSfx(AudioClip clip, float volume = 1.0f)
    {
        playerSfxSource.PlayOneShot(clip, volume);
    }

    public void PlayEggTurretSfx(AudioClip clip, float volume = 1.0f)
    {
        eggTurretSfxSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }



    public void StopMusic()
    {
        //musicSource.Stop();
    }
}
