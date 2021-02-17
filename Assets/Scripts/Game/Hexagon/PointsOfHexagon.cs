using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PointsOfHexagon
{
    public List<GameObject> cornerPoints;

    public void RefreshPoints(int _column,int _row,int orginalColumn,int orginalRow)
    {
        for (int i = 0; i < cornerPoints.Count; i++)
        {
            cornerPoints[i].SetActive(true);
        }
        SetPointsActiveForColumn(_column, orginalColumn).SetPointsActiveForRow(_column, _row, orginalRow);
    }
    public PointsOfHexagon SetPointsActiveForColumn(int _column,int orginalColumn)
    {
        if (_column == 0)
        {
            DisableCorner(3).DisableCorner(4).DisableCorner(5);
        }
        else if (_column == orginalColumn-1)
        {
            DisableCorner(0).DisableCorner(1).DisableCorner(2);
        }
        return this;
    }
    public PointsOfHexagon SetPointsActiveForRow(int _column, int _row,int orginalRow)
    {
        if (_row == 0)
        {
            DisableCorner(2).DisableCorner(3);
            if (_column % 2 == 1)
            {
                DisableCorner(1).DisableCorner(4);
            }
        }
        else if (_row == orginalRow-1)
        {
            DisableCorner(0).DisableCorner(5);
            if (_column % 2 == 0)
            {
                DisableCorner(1).DisableCorner(4);
            }
        }
        return this;
    }

    public PointsOfHexagon DisableCorner(int index)
    {
        if (cornerPoints[index].activeInHierarchy)
            cornerPoints[index].SetActive(false);

        return this;
    }
}