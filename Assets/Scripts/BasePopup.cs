using UnityEngine;

public class BasePopup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Open()
    {
        if (!IsActive())
        {
            this.gameObject.SetActive(true);
            Messenger.Broadcast(GameEvent.POPUP_OPENED);
            Debug.Log("open");
        }
        else
        {
            Debug.LogError(this + ".Open() – trying to open a popup that is active!");
        }

        //gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        if (IsActive())
        {
            this.gameObject.SetActive(false);
            Messenger.Broadcast(GameEvent.POPUP_CLOSED);
            Debug.Log("closed");
        }
        else
        {
            Debug.LogError(this + ".Open() – trying to open a popup that is active!");
        }

        //gameObject.SetActive(false);
    }
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
