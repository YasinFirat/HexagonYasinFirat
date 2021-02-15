using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Master
{
    public Transform circle;
    CircleObject circleObject;
    Vector2 point;

    void Start()
    {
        circleObject = circle.GetComponent<CircleObject>();
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


    private void OnMouseDown()
    {
        Debug.Log("0) Dokunma işkemi yapıldı");
        if (gameManager.GetReadyTouch())
        {
            BeginTouch();

            StartCoroutine(CheckTurnAvaible());
        }
    }

    public void BeginTouch()
    {
        circle.gameObject.SetActive(false);
        circleObject.SetVisibleSprite(false);
        circleObject.SetColliderReader(true);
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circle.position = point;
        circle.gameObject.SetActive(true);
    }

    IEnumerator CheckTurnAvaible()
    {
        while (!gameManager.dedector.isReadyForTurn)
        {
            yield return new WaitForFixedUpdate();
        }
        for (int i = 0; i < 3&&gameManager.GetReadyTouch(); i++)
        {
            gameManager.turnArround.TurnClockWise();
           yield return new WaitForSeconds(1);
        }
        gameManager.SetReadyTouch(false);
        
    }
}
