using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonStatus : Hexagon
{
    bool firstRun;
    int count = 0;
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public override Hexagon SetNestPosition(Vector2 _nestPosition)
    {
        nestPosition = gameManager.
            DeterminePositionOfHexagon(row, column, column % 2 == 1 ? (-gameManager.DISTANCEOFROW / 2) : 0);
        return this;
    }
    public Vector2 DeterminePositionOfHexagon(int _row, int _column, float restorePosition = 0)
    {
        return new Vector2(_column *gameManager.GetDistanceOfColumn(), _row * gameManager.GetDistanceOfRow() + restorePosition);
    }
    public void OnEnable()
    {
        
        if (count==0)
        {
            firstRun = true;
            count++;
            return;
        }
        transform.localPosition = gameManager.creativePoint[column].startPosition;

        SetNestPosition(Vector2.zero);
        Move();

        
    }
    private void OnDisable()
    {
        //if (count <= 1)
        //{
        //    count++;
        //    return;
        //}
           
      
        //gameManager.creativePoint[column].RemoveMember(row);
        //gameManager.creativePoint[column].CheckPositions();

    }
    public void Move()
    {
        StartCoroutine(MoveEnumerator());
    }
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
