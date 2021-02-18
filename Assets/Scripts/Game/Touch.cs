using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Master
{
    public Transform circle;
    public CircleObject circleObject;
    Vector2 point;
    public bool whenTouchExplode=false;
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


    private void OnMouseDown()
    {

#if UNITY_EDITOR
        Debug.Log("0) Dokunma işkemi yapıldı ve GetReadyTouch " + gameManager.ReadyTouch);
#endif
        if (gameManager.ReadyTouch)
        {
            BeginTouch();

            StartCoroutine(CheckTurnAvaible());
        }
    }

    public void BeginTouch()
    {
        circle.gameObject.SetActive(false);
        circleObject.SetVisibleSprite(false);
        circleObject.SetColliderReader(false);
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circle.localPosition = point;
        circleObject.SetColliderReader(true);
        circle.gameObject.SetActive(true);
    }

    IEnumerator CheckTurnAvaible()
    {
#if UNITY_EDITOR
        Debug.Log("dedector sınıfındna isReadyForTurn kontrol edilir. isREadyForTurn : "+ gameManager.dedector.isReadyForTurn);
#endif
        while (!gameManager.ReadyTurn)
        {
            yield return new WaitForFixedUpdate();
        }
        
        for (int i = 0; i < 3&& gameManager.ReadyTurn; i++)
        {
           
           gameManager.turnArround.TurnReverseClockWise();
           yield return new WaitForSeconds(1);
           
        }
#if UNITY_EDITOR
        Debug.Log("Dönme işlemi durduruldu veya tamamlandı ve dedector isReadtTurn "+ gameManager.dedector.isReadyForTurn + " edildi.");
#endif
        gameManager.ReadyTouch = false;
        gameManager.ReadyTurn = false;
    }
}
