using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    [SerializeField] private HexTileType hexType = HexTileType.five;
    public HexTileType HexType
    {
        get
        {
            return hexType;
        }
    }

    [SerializeField] private HexTileColors hexTileColors;
    [SerializeField] private SpriteRenderer sr;

    private Color drainColor = Color.white;
    private bool isCollidingWithPlayer = false;
    private bool isLosingLife = false;
    private Animator anim;

    private void Awake()
    {
        //hexType = (HexTileType)lives;

        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();

        UpdateColor();
    }

    private void Update()
    {
        if ((int)hexType <= 0)
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
        hexType--;
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
