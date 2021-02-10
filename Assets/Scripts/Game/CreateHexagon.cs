using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHexagon : Master
{
    public PoolNames myObject;
    public PoolNames hex;
   
    private void Start()
    {
        Create();
    }
    public void Create()
    {
         int row = gameManager.GetRow(),
            column = gameManager.GetColumn();
        float distanceOfRow = gameManager.GetDistanceOfRow(),
            restorePosition = 0f;
       
        for (int i = 0; i < column; i++)
        {
            gameManager.creativePoint.Add(new
                CreativePoint(i,gameManager.row,i*gameManager.GetDistanceOfColumn()));
            restorePosition=i%2==1?(-distanceOfRow / 2):0;
            for (int j = 0; j < row; j++)
            {
                poolManager.PullFromPool(myObject, gameManager.DeterminePositionOfHexagon(j, i, restorePosition))
                    .GetComponent<PointStatus>()
                    .SetRow(i)
                    .SetColumn(j).DoThisWhenFirstStart();
                gameManager.creativePoint[i].AddMember(poolManager.PullFromPool(hex,Vector3.zero,false)
                    .GetComponent<HexagonStatus>());
            }
        }

    }
}
