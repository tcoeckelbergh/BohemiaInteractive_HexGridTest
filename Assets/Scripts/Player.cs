using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 50f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizInput = -Input.GetAxis("Horizontal");
        transform.Rotate(transform.forward * horizInput * turnSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + (transform.up * moveSpeed * Time.deltaTime));
    }
}
