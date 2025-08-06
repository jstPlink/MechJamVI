using UnityEngine;

public class PlayMenuSound : MonoBehaviour
{
    public AK.Wwise.Event _event;


    public void push()
    {
        _event.Post(this.gameObject);
    }

}
