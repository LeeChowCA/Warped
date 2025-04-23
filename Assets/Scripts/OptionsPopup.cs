using UnityEngine;

public class OptionsPopup : BasePopup
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[SerializeField] private UIManager UIManager;
    [SerializeField] private SettingsPopup settingsPopup;


    public void OnSettingsButton()
    {
        base.Close();
        settingsPopup.Open();
        Debug.Log("settings clicked");
    }
    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
    public void OnReturnToGameButton()
    {
        //UIManager.SetGameActive(true);
        Debug.Log("return to game");
        base.Close();
    }
}
