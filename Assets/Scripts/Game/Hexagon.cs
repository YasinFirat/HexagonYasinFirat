using UnityEngine;

public abstract class Hexagon :Master
{
    public Vector2 nestPosition;
    public int row;
    public int column;

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
    public abstract Hexagon SetNestPosition(Vector2 _nestPosition);
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
}
