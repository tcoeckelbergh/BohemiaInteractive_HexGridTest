using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 50f;

    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode fireKey;

    private Rigidbody2D rb;
    private int nrCollidingTiles = 0;
    private Animator anim;
    private SpriteRenderer[] srs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float horizInput = 0;
        if (Input.GetKey(leftKey))
        {
            horizInput += 1;
        }
        if (Input.GetKey(rightKey))
        {
            horizInput -= 1;
        }

        transform.Rotate(transform.forward * horizInput * turnSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + (transform.up * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            nrCollidingTiles++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            nrCollidingTiles--;
            if (nrCollidingTiles <= 0)
            {
                InitiatePlayerDeath();
            }
        }
    }
    private void InitiatePlayerDeath()
    {
        anim.SetTrigger("Fall");
        foreach(SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = "Default";
        }
        
        Invoke("Cleanup", 1f);
    }

    private void Cleanup()
    {
        Destroy(this.gameObject);
    }
}
