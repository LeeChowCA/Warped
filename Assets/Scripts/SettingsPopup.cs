using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopup
{
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private TextMeshProUGUI volumeLabel;
    [SerializeField] private Slider VolumeSlider;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = Object.FindFirstObjectByType<SoundManager>();
        VolumeSlider.value = soundManager.MusicVolume;
    }

    public void OnOKButton()
    {


        base.Close();
        //Messenger.Broadcast(GameEvent.POPUP_OPENED);
        optionsPopup.Open();

    }

    public void OnCancelButton()
    {
        base.Close();
        optionsPopup.Open();
        //Messenger.Broadcast(GameEvent.POPUP_CLOSED);
    }

    override public void Open()
    {
        Messenger.Broadcast(GameEvent.POPUP_OPENED);

        gameObject.SetActive(true);
    }


    public void OnMusicVolumeChanged(float value)
    {
        Debug.Log("Music vol changed to:" + value);
        SoundManager.Instance.MusicVolume = value;
    }

    public void OnPlaySfx(float value)
    {
        Debug.Log("OnPlaySfx()");
        SoundManager.Instance.SfxVolume = value;
    }
}
