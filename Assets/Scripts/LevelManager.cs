using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    private int lastLevel = 1;

    public GameObject winScreen;
    public GameObject loseScreen;
    public Transform level2PlayerSpawn;

    public AudioClip winClip;
    public AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoseLevel()
    {
        loseScreen.SetActive(true);
    }


    public void NextLevel()
    {

        if(currentLevel == lastLevel)
        {
            winScreen.SetActive(true);
            gameMusic.clip = winClip;
            gameMusic.Play();
        }
        else
        {
            FindObjectOfType<PlayerScript>().transform.position = level2PlayerSpawn.position;
            FindObjectOfType<PlayerScript>().health = 3;
            FindObjectOfType<PlayerScript>().healthText.text = "Health: 3";

        }
        currentLevel++;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
}
