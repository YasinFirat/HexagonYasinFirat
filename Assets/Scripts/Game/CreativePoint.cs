using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreativePoint
{
    int arriveCounter;
    public int maxRow;
    public int column;
    public Vector2 startPosition;
    public List<HexagonStatus> hexagonStatuses;
    public PoolManager poolManager;


    public int GetColorKey(int index)
    {
        return hexagonStatuses[index].hexagonColor.GetKey();
    }
    public CreativePoint(int _column, int _row, float positionX,PoolManager _poolManager)
    {
        //Her Create noktasının bir başlangıç konumu vardır.
        startPosition = new Vector2(positionX, (Camera.main.orthographicSize + 1) * 2);
        poolManager=_poolManager;
        hexagonStatuses = new List<HexagonStatus>();
        column = _column;
        maxRow = _row;
    }
    /// <summary>
    /// CreatePoint'e obje ekle işlemini yapar.
    /// </summary>
    /// <param name="hexagonStatus"></param>
    public void AddMember(HexagonStatus hexagonStatus)
    {
        hexagonStatus.SetRow(hexagonStatuses.Count).SetColumn(column);
        hexagonStatuses.Add(hexagonStatus);
        
    }
     
    /// <summary>
    /// Patlamadan sonra yeni yerleşim yeri belirlenir.
    /// </summary>
    public void CheckPositions()
    {//eğer maxRow'a eşitse createPoint'in dolu olduğunu anlayıp işlem yaptırmıyoruz.
        if (hexagonStatuses.Count == maxRow)
            return;
        //maxRow eksik çıkınca maxRow'a ulaşıncaya kadar havuz'dan bir obje istedik.
        //AddMember(poolManager.PullFromPool(PoolNames.hexagon, startPosition)
        //       .GetComponent<HexagonStatus>());
        while (hexagonStatuses.Count != maxRow)
        {
            AddMember(poolManager.PullFromPool(PoolNames.hexagon, startPosition)
                .GetComponent<HexagonStatus>());
        }
        SetAllInformation().MoveAll();
    }
    /// <summary>
    /// Obje önce havuza gönderilir ardından listeden silinir.
    /// </summary>
    /// <param name="i"></param>
    public void RemoveMember(int i)
    {
        hexagonStatuses[i].GetComponent<PoolMember>().GoBackToPool();
    }
    /// <summary>
    /// Eğer hücre'deki obje active değilse remove edilir.
    /// </summary>
    public void ActiveControl()
    {
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            if (!hexagonStatuses[i].gameObject.activeInHierarchy)
            {
                hexagonStatuses.RemoveAt(i);
                i--;
            }

        }
       
    }
    /// <summary>
    /// Tüm objelerine gerekli row ve column değerlerini verir.
    /// </summary>
    /// <returns></returns>
    public CreativePoint SetAllInformation() 
    {
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hexagonStatuses[i].SetRow(i).SetColumn(column).SetNestPosition();
        }
        return this;
    }
    /// <summary>
    /// Tüm objelerini hareket ettirir.
    /// </summary>
    /// <returns></returns>
    public CreativePoint MoveAll()
    {
        for (int row = 0; row < hexagonStatuses.Count; row++)
        {
            hexagonStatuses[row].SetNestPosition();
            hexagonStatuses[row].Start_Move();
        }
        return this;
    }

    public bool IsArrivedAllMembers()
    { arriveCounter = 0;
       
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            arriveCounter += hexagonStatuses[i].isArrived ? 1:0;
        }

        return arriveCounter == maxRow;
    }

    public void SetColumnAndRowMember(int index)
    {
        hexagonStatuses[index].SetColumn(column).SetRow(index).SetNestPosition();
    }
}
