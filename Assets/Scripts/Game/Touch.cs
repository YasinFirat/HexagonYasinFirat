using System;
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
        if (gameManager.ReadyTouch)
        {
            TouchAndroid();
        }

    }
    public void TouchAndroid()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    TouchStart(Input.GetTouch(0).position);
                    break;
                case TouchPhase.Moved:
                    TouchMoved(Input.GetTouch(0).position);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    TouchEnd(Input.GetTouch(0).position);
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

        }
    }

    private void TouchEnd(Vector2 position)
    {
        position = Camera.main.ScreenToWorldPoint(position);

        if (Vector2.Distance(point,position) > .5f)
        {
            Debug.Log("Döndürme işlemi yapılır.");
            StartCoroutine(CheckTurnAvaible());

        }
        else
        {
            BeginTouch();
            Debug.Log("seçim işlemini yap");
        }
    }

    private void TouchStart(Vector2 position)
    {
       point= Camera.main.ScreenToWorldPoint(position);
        
    }
    private void TouchMoved(Vector2 position)
    {
        position = Camera.main.ScreenToWorldPoint(position);

        if (Vector2.Distance(point, position) > .5f)
        {
            Debug.Log("Döndürme işlemi yapılır.");
           // StartCoroutine(CheckTurnAvaible());

        }
    }

    private void OnMouseDown()
    {
        if (gameManager.ReadyTouch)
        {
           // BeginTouch();

         //   StartCoroutine(CheckTurnAvaible());
        }
    }
    /// <summary>
    /// Çember yeni konumuna yerleştirilir.
    /// </summary>
    public void BeginTouch()
    {
        #region Cemberin birçok ozelligi kapatılır.Yer değiştirmek için hazır olur.
        circle.gameObject.SetActive(false);
        circleObject.SetVisibleSprite(false);
        circleObject.SetColliderReader(false);
        #endregion

       // point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circle.localPosition = point;
        circleObject.SetColliderReader(true);
        circle.gameObject.SetActive(true);
    }

    IEnumerator CheckTurnAvaible()
    {
        while (!gameManager.ReadyTurn)
        {
            yield return new WaitForFixedUpdate();
        }
        
        for (int i = 0; i < 3&& gameManager.ReadyTurn; i++)
        {
            gameManager.turnArround.TurnReverseClockWise();
           yield return new WaitForSeconds(1);
           
        }
        if (!gameManager.ReadyTurn)
        {//döngüden patlama olduğundan çıkmıştır.
            gameManager.BeginExplodeWhenTouchScreen();
        }
        gameManager.ReadyTouch = false;
        gameManager.ReadyTurn = false;
    }
}
