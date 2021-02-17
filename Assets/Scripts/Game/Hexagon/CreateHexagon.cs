using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHexagon : Master
{
    public PoolNames hexagon;
    private void Start()
    {
        Create();
    }
    /// <summary>
    /// Tüm hexagon'ları oluşturur ve başlangıç için creativePoint dizisine ekler.
    /// </summary>
    public void Create()
    {
         int row = gameManager.GetRow(),
            column = gameManager.GetColumn();
        for (int i = 0; i < column; i++)
        {
            gameManager.creativePoint.Add(new
                CreativePoint(i,gameManager.row,i*gameManager.GetDistanceOfColumn(),poolManager));
            
            for (int j = 0; j < row; j++)
            {
                gameManager.creativePoint[i].AddMember(poolManager.PullFromPool(hexagon,Vector3.zero,false)
                    .GetComponent<HexagonStatus>());
            }
        }

    }
}
