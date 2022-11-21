using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public enum HexTileType
    {
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5
    }

    public HexTileType hexType
    {
        get;
        private set;
    } = HexTileType.five;

    private Color drainColor = Color.white;

    [SerializeField] private int lives = 5;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private HexTileColors hexTileColors;
    [SerializeField] private SpriteRenderer sr;

    private bool isCollidingWithPlayer = false;
    private bool isLosingLife = false;
    private Animator anim;

    private void Awake()
    {
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

        UpdateColor();
    }

    private void Update()
    {
        if (lives <= 0)
        {
            FadeoutTile();
        }
        else
        {
            if (isCollidingWithPlayer && !isLosingLife)
            {
                isLosingLife = true;
                LoseLife();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollidingWithPlayer = false;
        }
    }

    public void UpdateType(HexTileType type)
    {
        hexType = type;
        UpdateColor();
    }

    private void UpdateColor()
    {
        sr.color = hexTileColors.GetColor(hexType);
    }

    private void LoseLife()
    {
        anim.SetTrigger("Pulse");
        Invoke("Reset", 1f);
    }

    private void Reset()
    {
        lives--;
        UpdateColor();
        Invoke("LoseProtection", 0.5f);
    }

    private void LoseProtection()
    {
        isLosingLife = false;
    }

    private void FadeoutTile()
    {
        drainColor = sr.color;
        drainColor.a -= 0.01f;

        if (drainColor.a <= 0f)
        {
            Destroy(this.gameObject);
        }

        sr.color = drainColor;
    }
}
