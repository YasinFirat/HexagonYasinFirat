using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Master
{
    public Transform circle;
    public CircleObject circleObject;
    Vector2 point;
    Vector2 positionOfCircle;
    public bool afterExp=false;
    public bool isTouchAvaible=true;
    public bool isMoved = false;
    void Start()
    {
        gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetReadyExplode())
        {
           
            if (afterExp)
            {
                Invoke("Circle",1);
                afterExp = false;
            }
            //start yapsın bir saniye sonra circle açılsın. (patlamadan geldiğini bildir)
            gameManager.Explode();

        }
        if (isTouchAvaible)
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
                 //  TouchMoved(Input.GetTouch(0).position);
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
        
        //if (isMoved)
        //{
        //    Debug.Log("end2");
        //    isMoved = false;
        //    return;
        //}


        position = Camera.main.ScreenToWorldPoint(position);
        if (Vector2.Distance(point,position) > .05f&&circle.gameObject.activeInHierarchy)
        {
            StartCoroutine(CheckTurnAvaible((point.x - position.x) > 0|| (point.y - position.y > 0)));
        }
        else if(Vector2.Distance(point, position) < .05f)
        {
            positionOfCircle = point;
            Circle();
        }
    }
    private void TouchMoved(Vector2 position)
    {
        position = Camera.main.ScreenToWorldPoint(position);
        Debug.Log(Vector2.Distance(point, position));
        if (isMoved)
            return;
      
        if (Vector2.Distance(point, position) > .13f && circle.gameObject.activeInHierarchy)
        {
            StartCoroutine(CheckTurnAvaible((point.x - position.x) > 0 || (point.y - position.y > 0)));
            isMoved = true;
        }
    }

    private void TouchStart(Vector2 position)
    {
        Debug.Log("Dokundu");
        point= Camera.main.ScreenToWorldPoint(position);
        
    }
    public void CloseCircle()
    {
        #region Cemberin birçok ozelligi kapatılır.Yer değiştirmek için hazır olur.
        circle.gameObject.SetActive(false);
        circleObject.SetVisibleSprite(false);
        circleObject.SetColliderReader(false);
        #endregion


    }
    public void Circle()
    {CloseCircle();
        SetCirclePosition();
        OpenCircle();
    }
    public void OpenCircle()
    {
        circleObject.SetColliderReader(true);
        circle.gameObject.SetActive(true);
       
    }
    public void SetCirclePosition()
    {
        circle.localPosition = positionOfCircle;
    }


    IEnumerator CheckTurnAvaible(bool _direction)
    {
        while (!gameManager.ReadyTurn)
        {
            yield return new WaitForFixedUpdate();
        }
        isTouchAvaible = false;
        for (int i = 0; i < 3&& gameManager.ReadyTurn; i++)
        {
            gameManager.turnArround.StartTurn(_direction);
           yield return new WaitForSeconds(1);
           
        }
        if (!gameManager.ReadyTurn)
        {//döngüden patlama olduğundan çıkmıştır.
            gameManager.BeginExplodeWhenTouchScreen();
            CloseCircle();
            afterExp = true;
        }
        else
        {
            Circle();
        }
        gameManager.ReadyTurn = false;
       
        isTouchAvaible = true;


    }
}
