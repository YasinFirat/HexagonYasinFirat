using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Hexagon :Master,IColor,ICorners,IMove
{
    private bool firstRun;
    private bool firstMove;
    private float speed;
    private HexagonColor hexagonColor;

    [HideInInspector]public PoolMember poolMember;
    [HideInInspector]public Vector2 nestPosition;
    [HideInInspector]public int row;
    [HideInInspector]public int column;
    [HideInInspector]public bool isArrived;
   
    public PointsOfHexagon pointsOfHexagon;
    public SpriteRenderer spriteRenderer;

    public float Speed { get { return speed; } set { speed = value; } }

    /// <summary>
    /// Constructor ile sorun yaşamam durumunda bu metodu kullanacağım
    /// </summary>
    /// <returns></returns>
    public abstract Hexagon DoThisWhenFirstStart();
    /// <summary>
    /// Obje aktif olduğunda standart olarak yapılması gerekenler.
    /// </summary>
    public void DoThisWhenOnEnabled()
    {
        if (!firstRun)
        {
            firstRun = !firstRun;
            return;
        }
        
        transform.localPosition = gameManager.creativePoint[column].startPosition;
        SetColor(GetRandomColor());
        SetNestPosition();
        Start_Move();
    }
    /// <summary>
    /// Hexagon için row değeri atanır
    /// </summary>
    /// <param name="_row"></param>
    /// <returns></returns>
    public Hexagon SetRow(int _row)
    {
        this.row = _row;
        return this;
    }
    /// <summary>
    /// Hexagon için column değeri atanır
    /// </summary>
    /// <param name="_column"></param>
    /// <returns></returns>
    public Hexagon SetColumn(int _column)
    {
        this.column = _column;
        return this;
    }
    /// <summary>
    /// Hexagon için column ve row değeri hesaplanarak bulunması gereken konumu hesaplar.
    /// </summary>
    /// <returns></returns>
    public Hexagon SetNestPosition()
    {
        nestPosition = DeterminePositionOfHexagon(row, column);
        return this;
    }
    /// <summary>
    /// Runtime'da hexagon'un row değerini döndürür.
    /// </summary>
    /// <returns></returns>
    public int GetRow()
    {
        return row;
    }
    /// <summary>
    /// Runtime'da hexagon'un column değerini döndürür.
    /// </summary>
    /// <returns></returns>
    public int GetColumn()
    {
        return column;
    }
    /// <summary>
    /// Hexagon'un bulunması gereken konumu gösterir.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetNestPosition()
    {
        return nestPosition;
    }
    /// <summary>
    /// Her hexagon'un kendine göre bir konumu bulunmaktadır ve bulunması gereken konumun hesabını yapar
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_column"></param>
    /// <returns></returns>
    private Vector2 DeterminePositionOfHexagon(int _row, int _column)
    {
        float restore = _column % 2 == 1 ? (-gameManager.GetDistanceOfRow() / 2) : 0;
        return new Vector2(_column * gameManager.GetDistanceOfColumn(), _row * gameManager.GetDistanceOfRow() + restore);
    }
    public void SetColor(HexagonColor _hexagonColor)
    {
        hexagonColor = _hexagonColor;
        spriteRenderer.color = GetColor().color32;
    }
    public HexagonColor GetColor()
    {
        return hexagonColor;
    }
    public HexagonColor GetRandomColor()
    {
    return gameManager.hexagonColors[Random.Range(0, gameManager.hexagonColors.Count)];
    }
    public void RefreshAllCorners()
    {
        pointsOfHexagon.RefreshPoints(column, row, gameManager.GetColumn(), gameManager.GetRow());
    }
    public IEnumerator MoveEnumerator()
    {
        isArrived = false;
        Speed = 5;
        if (!firstMove)
        {
            yield return new WaitForSeconds((column + row + Random.Range(0, 1f)) / 8);
            Speed = 30;
            firstMove = true;
        }
        while (Vector2.SqrMagnitude(GetNestPosition() - (Vector2)transform.localPosition) > 0.01)
        {
            transform.localPosition =
                Vector2.MoveTowards(transform.localPosition, GetNestPosition(), Time.deltaTime * Speed);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = GetNestPosition();

        isArrived = true;
        RefreshAllCorners();
    }
    public void Start_Move()
    {
        StartCoroutine(MoveEnumerator());
    }

   
}
