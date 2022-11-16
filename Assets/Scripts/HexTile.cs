using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private Color fiveLivesColor;
    [SerializeField] private Color fourLivesColor;
    [SerializeField] private Color threeLivesColor;
    [SerializeField] private Color twoLivesColor;
    [SerializeField] private Color oneLivesColor;
    private Color drainColor;

    [SerializeField] private int lives = 5;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float timer = 0f;

    private SpriteRenderer sr;
    private bool isCollidingWithPlayer = false;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = GetTileColor(lives);
        drainColor = oneLivesColor;
    }

    private void Update()
    {
        if (lives <= 0)
        {
            FadeoutTile();
        }
        else
        {
            if (isCollidingWithPlayer)
            {
                timer += Time.deltaTime;
                if (timer >= lifeTime)
                {
                    LoseLife();
                    timer -= lifeTime;
                }
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

    private void LoseLife()
    {
        lives--;
        sr.color = GetTileColor(lives);
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

    private Color GetTileColor(int lives)
    {
        switch (lives)
        {
            case 5:
                return fiveLivesColor;
            case 4:
                return fourLivesColor;
            case 3:
                return threeLivesColor;
            case 2:
                return twoLivesColor;
            case 1:
                return oneLivesColor;
            default:
                return oneLivesColor;
        }
    }
}