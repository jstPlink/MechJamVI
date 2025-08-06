using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Settings : MonoBehaviour
{
    public AK.Wwise.RTPC _RTPC;
    public Slider _slider;

    public void pushSettings()
    {
        _RTPC.SetGlobalValue(_slider.value);
    }
}
