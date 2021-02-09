using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHexagon : PoolMaster
{
    public PoolNames myObject;
    private void Start()
    {
        Create();
    }
    public void Create()
    {
        int row = poolManager.GetRow(myObject),
            column = poolManager.GetColumn(myObject);
        float distanceOfRow = poolManager.GetDistanceOfRow(myObject),
            distanceOfColumn = poolManager.GetDistanceOfColumn(myObject),
            restorePosition = 0f;
        Vector2 positionHexagon;
        for (int i = 0; i < column; i++)
        {
            if (i % 2 == 1)
            {
                restorePosition = -distanceOfRow / 2;
            }
            else
            {
                restorePosition = 0;
            }
            for (int j = 0; j < row; j++)
            {
                positionHexagon = new 
                    Vector2(i* distanceOfColumn, j * distanceOfRow + restorePosition);
                poolManager.PullFromPool(myObject, positionHexagon);
            }
        }
    }
}
