using UnityEngine;
using UnityEngine.Audio;

public class SoundManager:MonoBehaviour
{
    static public SoundManager Instance;

    [SerializeField] private AudioSource playerSfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource eggTurretSfxSource;

    
    [SerializeField] private AudioClip musicTrack;

    private float sfxVolume = 1.0f;     // for tracking sfx volume
    private float musicVolume = 1.0f;   // for tracking music volume

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        PlayMusic(musicTrack);

        if (Instance == null)                    // if Awake() has never been called before
        {
            Instance = this;                    // remember this as our (one & only) SM
        }
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

    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = Mathf.Clamp(value, 0.0f, 1.0f);
            mixer.SetFloat("SfxVolume", LinearToLog(sfxVolume));
        }
    }

    //// a property to get/set music volume
    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = Mathf.Clamp(value, 0.0f, 1.0f);
            mixer.SetFloat("MusicVolume", LinearToLog(musicVolume));
            Debug.Log("Music volume set to: " + musicVolume);
        }
    }

    private float LinearToLog(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
