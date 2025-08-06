using UnityEngine;

public class WwiseButton : MonoBehaviour
{
    public AK.Wwise.Event clickEvent = null;
    public void OnClick()
    {
        clickEvent.Post(gameObject);
    }
}
