using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHexagon : Master
{
    public PoolNames nest;
    public PoolNames hexagon;
   
    private void Start()
    {
        Create();
    }
    /// <summary>
    /// Tüm nest'ler belirlenir ve createPoint belirlenir.
    /// </summary>
    public void Create()
    {
         int row = gameManager.GetRow(),
            column = gameManager.GetColumn();
        for (int i = 0; i < column; i++)
        {
            gameManager.creativePoint.Add(new
                CreativePoint(i,gameManager.row,i*gameManager.GetDistanceOfColumn()));
            
            for (int j = 0; j < row; j++)
            {
                poolManager.PullFromPool(nest, gameManager.DeterminePositionOfHexagon(j, i))
                    .GetComponent<Hexagon>()
                    .SetRow(i)
                    .SetColumn(j).DoThisWhenFirstStart();
                gameManager.creativePoint[i].AddMember(poolManager.PullFromPool(hexagon,Vector3.zero,false)
                    .GetComponent<HexagonStatus>());
            }
        }

    }
}
