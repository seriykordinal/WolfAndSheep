using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 2500f;    
    private Rigidbody2D _rb;

    private Vector2 _moveDirection;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        Move();
    }
    void Move()
    {
        //transform.rotation = Quaternion.identity;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector2 (moveX, moveY).normalized;
        _rb.AddForce(new Vector2(_moveDirection.x * MovementSpeed * Time.deltaTime, _moveDirection.y * MovementSpeed * Time.deltaTime));
      
        
    }


}
