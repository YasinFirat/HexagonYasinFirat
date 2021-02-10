using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreativePoint:Master
{
    public int maxRow;
    public int column;
    public Vector2 startPosition;
    public List<HexagonStatus> hexagonStatuses;

    public CreativePoint(int _column, int _row, float positionX)
    {
        startPosition = new Vector2(positionX, (Camera.main.orthographicSize + 1) * 2);

        hexagonStatuses = new List<HexagonStatus>();
        column = _column;
        maxRow = _row;
    }
    public void AddMember(HexagonStatus hexagonStatus)
    {
        hexagonStatus.SetRow(hexagonStatuses.Count).SetColumn(column);
        hexagonStatuses.Add(hexagonStatus);
    }
    /// <summary>
    /// Patlamadan sonra yeni yerleşim yeri belirlenir.
    /// </summary>
    public void CheckPositions()
    {
        if (hexagonStatuses.Count == maxRow)
            return;
        AddMember(poolManager.PullFromPool(PoolNames.hexagon,startPosition).GetComponent<HexagonStatus>());
                
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hexagonStatuses[i].SetRow(i).SetColumn(column);
        }
        MoveAll();
    }
    public void RemoveMember(int i)
    {
        hexagonStatuses[i].GetComponent<PoolMember>().GoBackToPool();
        hexagonStatuses.RemoveAt(i);
    }
    public void MoveAll()
    {
        for (int row = 0; row < hexagonStatuses.Count; row++)
        {
            hexagonStatuses[row].SetNestPosition(Vector2.zero);
            hexagonStatuses[row].Move();
        }
    }
}
