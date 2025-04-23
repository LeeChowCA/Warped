using TMPro;
using UnityEngine;

public class SettingsPopup : BasePopup
{
    [SerializeField] private OptionsPopup optionsPopup;

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
}
