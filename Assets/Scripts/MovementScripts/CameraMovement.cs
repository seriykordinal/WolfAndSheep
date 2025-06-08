using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerTransform;
    Vector3 _offset;

    private void Start()
    {
        _offset = new Vector3(0, 0, -10);
    }
    private void Update()
    {
        LookToPlayer();
    }
    
    void LookToPlayer()
    {
        if (PlayerTransform != null)
        {
            transform.position = PlayerTransform.position + _offset;

        }
    }
}
