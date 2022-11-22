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
        blurImage.SetActive(true);
        winText.GetComponent<TextMeshProUGUI>().text = name + " has died!";
        winText.SetActive(true);
        replayText.SetActive(true);
    }
}
