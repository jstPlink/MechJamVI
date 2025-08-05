using UnityEngine;

public delegate void ChangingListenerProbe(AkGameObj AkGO);

public class AkListenerDistanceProbeChanger : MonoBehaviour
{
    public AkListenerDistanceProbe AkLDP;
    public static AkGameObj probe;

    public static event ChangingListenerProbe OnListenerProbeChange;

    private void Awake()
    {
        if (AkLDP == null)
        { // If not assigned already
            AkLDP = GetComponent<AkListenerDistanceProbe>(); // Will automatically try to find the AkListenerDistanceProbeScript.
            probe = AkLDP.distanceProbe;

            if (AkLDP == null)
            {
                Debug.LogWarning(this.name + " cannot find AkListenerDistanceProbe script.");
            }
        }
    }

    public void SetDistanceProbe(AkGameObj newProbe)
    {
        if (AkLDP)
        {
            if (newProbe != AkLDP.distanceProbe)
            {
                AkLDP.distanceProbe = newProbe;
                probe = AkLDP.distanceProbe;
                if (AkLDP.distanceProbe)
                {
                    var listenerGameObjectID = AkSoundEngine.GetAkGameObjectID(AkLDP.gameObject);
                    var distanceProbeGameObjectID = AkSoundEngine.GetAkGameObjectID(newProbe.gameObject);
                    AkSoundEngine.SetDistanceProbe(listenerGameObjectID, distanceProbeGameObjectID);
                }

                if (OnListenerProbeChange != null)
                {
                    OnListenerProbeChange(newProbe);
                }
            }
        }
        else
        {
            Debug.LogWarning(this.name + " cannot find AkListenerDistanceProbe script.");
        }
    }
}
