using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;


public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public GameObject titleScreen;

    public Button restartButton;
    
    private int score = 0;
    public bool isGameActive;

    public List<GameObject> targets;

    private float spawnRate = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        scoreText.gameObject.SetActive(true);
        spawnRate /= difficulty;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        if (gameOverText.gameObject.activeInHierarchy || restartButton.gameObject.activeInHierarchy)
        {
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }

        if (titleScreen.activeInHierarchy)
        {
            titleScreen.SetActive(false);
        }
        else
        {
            titleScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score:\n" + score.ToString();
    }
}
