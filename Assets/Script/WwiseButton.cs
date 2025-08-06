using UnityEngine;

public class WwiseButton : MonoBehaviour
{
    public AK.Wwise.Event ClickEvent;
    public void OnClick()
    {
        ClickEvent.Post(gameObject);
    }
    public AK.Wwise.Event HoverEvent;
    public void OnHover()
    {
        HoverEvent.Post(gameObject);
    }
}
