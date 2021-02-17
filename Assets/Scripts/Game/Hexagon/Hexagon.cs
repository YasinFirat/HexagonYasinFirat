using System.Collections.Generic;
using UnityEngine;


public abstract class Hexagon :Master
{
    [HideInInspector]public PoolMember poolMember;
    [HideInInspector]public Vector2 nestPosition;
    [HideInInspector]public int row;
    [HideInInspector]public int column;
    [HideInInspector]public bool isArrived;
    [HideInInspector]public HexagonColor hexagonColor;
    public PointsOfHexagon pointsOfHexagon;
    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// Constructor ile sorun yaşamam durumunda bu metodu kullanacağım
    /// </summary>
    /// <returns></returns>
    public abstract Hexagon DoThisWhenFirstStart();
    
    public Hexagon SetRow(int _row)
    {
        this.row = _row;
        return this;
    }
    public Hexagon SetColumn(int _column)
    {
        this.column = _column;
        return this;
    }
    public abstract Hexagon SetNestPosition();
    public int GetRow()
    {
        return row;
    }
    public int GetColumn()
    {
        return column;
    }
    public Vector2 GetNestPosition()
    {
        return nestPosition;
    }
    
    public Vector2 DeterminePositionOfHexagon(int _row, int _column)
    {
        float restore = _column % 2 == 1 ? (-gameManager.GetDistanceOfRow() / 2) : 0;
        return new Vector2(_column * gameManager.GetDistanceOfColumn(), _row * gameManager.GetDistanceOfRow() + restore);
    }

}
