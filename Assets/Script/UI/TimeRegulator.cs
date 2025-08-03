using UnityEngine;

public class TimeRegulator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Time.timeScale = 0.05f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            Time.timeScale = 1f;
        }
    }
}
