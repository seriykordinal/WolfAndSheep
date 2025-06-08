using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRotation : MonoBehaviour
{
    private Vector2 _mousePosition;
    public Vector2 MousePostion { get { return _mousePosition; } }
    private void Update()
    {
        RotateAim();

    }

    private void RotateAim()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookPosition = _mousePosition - new Vector2(transform.position.x, transform.position.y);
        float rotateZ = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateZ - 90f);

    }
}
