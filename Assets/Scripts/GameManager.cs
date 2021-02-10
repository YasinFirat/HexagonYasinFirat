using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public readonly float DISTANCEOFROW = 0.86f;
    public readonly float DISTANCEOFCOLUMN = 0.75f;
    public int row;
    public int column;
    public float distanceOfHexagon = .1f;
    public List<CreativePoint> creativePoint;

    public void StartGame()
    {
        StartCoroutine(StartThisGame());
    }
    public void RandomDestroy()
    {
        creativePoint[Random.Range(0, column)]
            .RemoveMember(Random.Range(0, row));
        

        for (int i = 0; i < creativePoint.Count; i++)
        {
            creativePoint[i].CheckPositions();
        }
    }
    
    public IEnumerator StartThisGame()
    {
        int i = 0;
        int j = 0;
        while (j < row)
        {
            while (i<column)
            {
                creativePoint[i].hexagonStatuses[j].gameObject.SetActive(true);
                i++;
                yield return new WaitForFixedUpdate();
            }
           
            j++;
            i = 0;
        }

       
    }
    public int GetRow()
    {
        return row;
    }
    public int GetColumn()
    {
        return column;
    }
    public float GetDistanceOfRow()
    {
        return distanceOfHexagon + DISTANCEOFROW;
    }
    public float GetDistanceOfColumn()
    {
        return distanceOfHexagon + DISTANCEOFCOLUMN;
    }
    public Vector2 DeterminePositionOfHexagon(int _row,int _column,float restorePosition=0)
    {
        return new Vector2(_column * GetDistanceOfColumn(), _row * GetDistanceOfRow() + restorePosition);
    }
}