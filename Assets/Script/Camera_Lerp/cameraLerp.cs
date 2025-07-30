using UnityEngine;

public class cameraLerp : MonoBehaviour
{

    public Vector3 deltaDistance;
    public float lerpSpeed;
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - deltaDistance, lerpSpeed);
    }
}
