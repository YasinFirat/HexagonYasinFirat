using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonStatus : Hexagon
{
    bool firstRun;

    public void OnEnable()
    {
        if (!firstRun)
        {
            firstRun = !firstRun;
            return;
        }
      
        Move();


    }
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public override Hexagon SetNestPosition()
    {
        nestPosition = gameManager.DeterminePositionOfHexagon(row, column);
        return this;
    }
    public Vector2 DeterminePositionOfHexagon(int _row, int _column, float restorePosition = 0)
    {
        return new Vector2(_column *gameManager.GetDistanceOfColumn(), _row * gameManager.GetDistanceOfRow() + restorePosition);
    }
   
    public void Move()
    {
        transform.localPosition = gameManager.creativePoint[column].startPosition;
        SetNestPosition();
        StartCoroutine(MoveEnumerator());
    }
    /// <summary>
    /// Örnekteki animasyona benzemesi amacıyla bu animasyon oluşturuldu.
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveEnumerator()
    {
        float ivme = 0;
        yield return new WaitForSeconds((column + row + Random.Range(0, 1f)) / 8);
        while (Mathf.Abs(GetNestPosition().y-transform.localPosition.y) > 0.1)
        {
            ivme += Time.deltaTime;
            transform.localPosition = 
                Vector2.MoveTowards(transform.localPosition, GetNestPosition(), Time.deltaTime* ivme * 100);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = GetNestPosition();
       
    }
}
