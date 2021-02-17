using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonStatus : Hexagon
{
    bool firstRun;
    bool firstMove;
    public void OnEnable()
    {
        if (!firstRun)
        {
            firstRun = !firstRun;
            return;
        }
        //obje görünür olur olmaz listesinde olduğu creativePoint pozisyonundan başlar.
        transform.localPosition = gameManager.creativePoint[column].startPosition;
       
        SetColor(gameManager.GetRandomColor).SetNestPosition();
       
        Start_Move();
    }
    public HexagonStatus SetColor(HexagonColor _hexagonColor)
    {
        hexagonColor = _hexagonColor;
        spriteRenderer.color = hexagonColor.color32;
        return this;
    }
    public void RefreshAllCorners()
    {
        pointsOfHexagon.RefreshPoints(column,row,gameManager.GetColumn(),gameManager.GetRow());
    }
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }
    public override Hexagon SetNestPosition()
    {
        nestPosition = DeterminePositionOfHexagon(row, column);
        return this;
    }
    public void Start_Move()
    {
        StartCoroutine(MoveEnumerator());
    }

    /// <summary>
    /// Örnekteki animasyona benzemesi amacıyla bu animasyon oluşturuldu.
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveEnumerator()
    {
        isArrived = false;
        float speed = 5;
        if (!firstMove)
        {
            yield return new WaitForSeconds((column + row + Random.Range(0, 1f)) / 8);
            speed = 30;
            firstMove = true;
        }
      
        while (Vector2.SqrMagnitude(GetNestPosition() - (Vector2)transform.localPosition) > 0.01)
        {
            transform.localPosition =
                Vector2.MoveTowards(transform.localPosition, GetNestPosition(), Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = GetNestPosition();

        isArrived = true;
        RefreshAllCorners();
    }
}
