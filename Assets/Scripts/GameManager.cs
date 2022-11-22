using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject blurImage;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject replayText;
    [SerializeField] GameObject canvas;
    bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnPlayerDeath(string name)
    {
        if (!gameOver)
        {
            gameOver = true;
            canvas.SetActive(true);
            blurImage.SetActive(true);
            winText.GetComponent<TextMeshProUGUI>().text = name + " has died!";
            winText.SetActive(true);
            replayText.SetActive(true);
        }  
    }
}
