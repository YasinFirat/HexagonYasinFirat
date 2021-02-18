using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public  class Score
{
    public int pointsExplosion = 5;
    public Text txtScore;
    public Text txtHighScore;
    public Text txtMoves;

    [HideInInspector]public int point;
    [HideInInspector]public int attack;
    
    public int AddScore(int _point)
    {
        point += _point* pointsExplosion;
        if (PlayerPrefs.GetInt("HighScore") < point)
        {
            PlayerPrefs.SetInt("HighScore",point);
        }
        SetTextScore(point).SetTextHighScore();
        return point;
    }
    public int GetScore()
    {
        return point;
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    public int GetMoves()
    {
        return attack;
    }

    public Score SetTextScore(int _CurrentScore)
    {
        txtScore.text = _CurrentScore.ToString();
        return this;
    }
    public Score SetTextHighScore()
    {
        txtHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        return this;
    }
    public Score SetTextMoves()
    {
        attack++;
        txtMoves.text = attack.ToString();
        return this;
    }
    

}
