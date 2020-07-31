﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    private int score;

    public Button restartButton;
    public GameObject titleScreen;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTargetRoutine()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int rndIndex = Random.Range(0, targets.Count);
            Instantiate(targets[rndIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        // divid spawn rate by difficulty
        spawnRate /= difficulty;

        isGameActive = true;

        StartCoroutine(SpawnTargetRoutine());
        score = 0;
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }
}