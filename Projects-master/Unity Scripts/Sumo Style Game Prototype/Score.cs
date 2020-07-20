using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //use wave number for level
    public int waveNumber;
    public Text scoreText;
    private float timer = 0f;
    private bool increaseTime = true;
    PlayerController playerController;
    SpawnManager spawnManager;
    private float scoreModifier = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

        waveNumber = spawnManager.waveNumber;

        if (increaseTime)
        {
            timer += Time.deltaTime * (waveNumber * scoreModifier);
        }

        if(playerController.isGameOver == true)
        {
            increaseTime = false;
        }

        scoreText.text = "Level: " + waveNumber.ToString() + "\nScore: " + timer.ToString("0.0");

    }


}
