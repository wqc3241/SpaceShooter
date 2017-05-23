﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //public GameObject hazard;
	public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;


	private bool gameOver;
	private bool restart;
	private int score;
	private bool touched;

    void Start()
    {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

	void Update(){
		if (restart) {
			if (Input.anyKey) {
				UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_1");
				restart = false;
			}
		}
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

			if (gameOver) {
				restartText.text = "press screen to restart";
				restart = true;
				break;
			}
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}