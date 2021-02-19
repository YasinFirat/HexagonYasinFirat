using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : Master
{

    public Text txtScore;
    public Text txtHighScore;

    private void OnEnable()
    {
        txtScore.text = gameManager.score.GetScore().ToString();
        txtHighScore.text = gameManager.score.GetHighScore().ToString();
    }
    public void RestartThisGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
