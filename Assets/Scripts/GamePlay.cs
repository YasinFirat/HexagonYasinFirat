using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : Master
{
    void Start()
    {
        gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetReadyExplode())
        {
           gameManager.Explode();
        }

      
    }

}
