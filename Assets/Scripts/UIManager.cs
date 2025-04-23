using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //private int initialScore = 0;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;

    //[SerializeField] private GameOverPopup gameOverPopup;

    private int popupsActive = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    private void OnPopupOpened()
    {
        Debug.Log("Popup opened");
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;
        Debug.Log("popupsActive: " + popupsActive);
    }
    private void OnPopupClosed()
    {
        Debug.Log("Popup closed");
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
        Debug.Log("popupsActive: " + popupsActive);
    }

    void Update()
    {
        //personally think we shouldn't add the !settingsPopup.IsActive() here, because we can't open the options popup when the settings popup is open
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsPopup.IsActive() && !settingsPopup.IsActive() && popupsActive == 0)
        {
            SetGameActive(false);
            optionsPopup.Open();
        }
    }
    // update score display
    public void UpdateScore(int newScore)
    {
        if (score != null)
        {
            score.text = "Score:" + newScore.ToString();
        }
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Time.timeScale = 1; // unpause the game
            Cursor.lockState = CursorLockMode.Locked; // lock cursor at center
            Cursor.visible = false; // hide cursor

            //Messenger.Broadcast(GameEvent.GAME_ACTIVE);
        }
        else
        {
            Time.timeScale = 0; // pause the game
            Cursor.lockState = CursorLockMode.None; // let cursor move freely
            Cursor.visible = true; // show the cursor

            //Messenger.Broadcast(GameEvent.GAME_INACTIVE);
        }
    }

    //public void ShowGameOverPopup()
    //{
    //    gameOverPopup.Open();
    //}
}
