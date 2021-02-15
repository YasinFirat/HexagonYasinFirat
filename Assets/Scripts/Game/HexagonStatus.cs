using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PointsOfHexagon
{
   
    public List<GameObject> cornerPoints;

    public void RefreshPoints()
    {
        for (int i = 0; i < cornerPoints.Count; i++)
        {
            cornerPoints[i].SetActive(true);
        }
    }

    public PointsOfHexagon DisableCorner(int index)
    {
        if (cornerPoints[index].activeInHierarchy)
                cornerPoints[index].SetActive(false);

        return this;
       
    }
}
public class HexagonStatus : Hexagon
{
    public PointsOfHexagon pointsOfHexagon;
    bool firstRun;
    bool firstMove;
    [HideInInspector] public HexagonColor hexagonColor;
    SpriteRenderer spriteRenderer;
    public bool isArrived;
    public int id 
    { 
        get
        {
            return column * 10 + row;
        } 
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    public void OnEnable()
    {
        if (!firstRun)
        {
            firstRun = !firstRun;
            return;
        }
        transform.localPosition = gameManager.creativePoint[column].startPosition;
        RefreshAllCorner().SetPointsActiveForColumn(column).SetPointsActiveForRow(column,row);
        SetColor(gameManager.GetRandomColor).SetNestPosition();
        Start_Move();
    }
    public HexagonStatus SetColor(HexagonColor _hexagonColor)
    {
        hexagonColor = _hexagonColor;
        spriteRenderer.color = hexagonColor.color32;
        return this;
    }
    public HexagonStatus RefreshAllCorner()
    {
        pointsOfHexagon.RefreshPoints();
        return this;
    }
    public HexagonStatus SetPointsActiveForColumn(int _column)
    {
        if (_column==0)
        {
            pointsOfHexagon.DisableCorner(3).DisableCorner(4).DisableCorner(5);
        }
        else if (_column == gameManager.GetColumn())
        {
            pointsOfHexagon.DisableCorner(0).DisableCorner(1).DisableCorner(2);
        }
        return this;
    }
    public HexagonStatus SetPointsActiveForRow(int _column,int _row)
    {
        if (_row == 0)
        {
            pointsOfHexagon.DisableCorner(2).DisableCorner(3);
            if (_column % 2 == 1)
            {
                pointsOfHexagon.DisableCorner(1).DisableCorner(4);
            }
        }
        else if (_row == gameManager.GetRow())
        {
            pointsOfHexagon.DisableCorner(0).DisableCorner(5);
            if (_column % 2 == 1)
            {
                pointsOfHexagon.DisableCorner(1).DisableCorner(4);
            }
        }
        return this;
    }
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public override Hexagon SetNestPosition()
    {
        nestPosition = gameManager.DeterminePositionOfHexagon(row, column);
        return this;
    }
    public Vector2 DeterminePositionOfHexagon(int _row, int _column, float restorePosition = 0)
    {
        return new Vector2(_column *gameManager.GetDistanceOfColumn(), _row * gameManager.GetDistanceOfRow() + restorePosition);
    }
   
    public void Start_Move()
    {
      
        StartCoroutine(MoveEnumerator());
    }

    /// <summary>
    /// Örnekteki animasyona benzemesi amacıyla bu animasyon oluşturuldu.
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveEnumerator()
    {

        isArrived = false;
        float speed = 3;
        if (!firstMove)
        {
            yield return new WaitForSeconds((column + row + Random.Range(0, 1f)) / 8);
            speed = 30;
            firstMove = true;
        }
      
        while (Vector2.SqrMagnitude(GetNestPosition() - (Vector2)transform.localPosition) > 0.01)
        {
            transform.localPosition =
                Vector2.MoveTowards(transform.localPosition, GetNestPosition(), Time.deltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = GetNestPosition();

        isArrived = true;
    }
}
