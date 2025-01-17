﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
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

  void Start () {
    gameOver          = false;
    restart           = false;
    score             = 0;
    restartText.text  = "";
    gameOverText.text = "";

    UpdateScore();
    StartCoroutine (SpawnWaves ());
  }

  void Update () {
    if (restart) {
      if (Input.GetKeyDown (KeyCode.R)) {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
      }
    }
  }

  IEnumerator SpawnWaves () {
    yield return new WaitForSeconds (startWait);

    while (true) {
      for (int i = 0; i < hazardCount; i++) {
        GameObject hazard        = hazards [Random.Range (0, hazards.Length)];
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 spawnPosition    = new Vector3 (
          Random.Range(-spawnValues.x, spawnValues.x), 
          spawnValues.y, 
          spawnValues.z
        );

        Instantiate (hazard, spawnPosition, spawnRotation);
        yield return new WaitForSeconds (spawnWait);
      }
      yield return new WaitForSeconds (waveWait);

      if (gameOver) {
        restartText.text = "Press 'R' for Restart";
        restart = true;
        break;
      }
    }
  }

  public void AddScore (int newScoreValue) {
    score += newScoreValue;
    UpdateScore ();
  }

  public int GetScore () {
    return score;
  }

  void UpdateScore () {
    scoreText.text = "Score: " + score;
  }

  public void GameOver () {
    gameOverText.text = "Game Over!";
    gameOver = true;
  }
}
