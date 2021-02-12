using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public readonly float DISTANCEOFROW = 0.86f;
    public readonly float DISTANCEOFCOLUMN = 0.75f;
    public int row;
    public int column;
    public float distanceOfHexagon = .1f;
    public List<CreativePoint> creativePoint;
    [Header("Colors")]
    public List<HexagonColor> hexagonColors;
    public ExplodeHexagon explodeHexagon;

    

    /// <summary>
    /// Oyun Başladığında çalışır.
    /// </summary>
    public void StartGame()
    {
        //key eklendi.karşılaştırma Getkey() ile yapılacak.
        for (int i = 0; i < hexagonColors.Count; i++)
        {
            hexagonColors[i].SetKey(i);
        }
        StartCoroutine(StartThisGame());
        explodeHexagon = new ExplodeHexagon(this);
    }
    public bool EqualColumn(int _column)
    {
        if (_column >= 0 && _column < column)
        {
            return true;
        }
        return false;
    }
    public bool EqualRow(int _row)
    {
        if (_row >= 0 && _row < row)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Patlatılacak noktaların listesi verilmeli
    /// </summary>
    /// <param name="explodes"></param>
    public void Explode()
    {
        List<Vector2Int> explodes = explodeHexagon.CheckExplode();
        for (int i = 0; i < explodes.Count; i++)
        {
            creativePoint[explodes[i].x].RemoveMember(explodes[i].y);
            
        }
        CheckAllPoints();
       
    }
    /// <summary>
    /// Patlama'dan sonra kullanılması önerilir. Patlayan obje'lerin yerine yeni objeleri yerleştirerek hareket ettirir.
    /// </summary>
    public void CheckAllPoints()
    {
        for (int i = 0; i < creativePoint.Count; i++)
        {
            creativePoint[i].ActiveControl();
        }
        for (int i = 0; i < creativePoint.Count; i++)
        {
            creativePoint[i].CheckPositions();
        }
    }
    public HexagonColor GetRandomColor
    {
        get
        {
            return hexagonColors[Random.Range(0, hexagonColors.Count)];
        }
       
    }
    /// <summary>
    /// Oyun başında objelerin düşme animasyonu için sıralamayı belirler.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartThisGame()
    {
        int i = 0;
        int j = 0;
        while (j < row)
        {
            while (i<column)
            {
                creativePoint[i].hexagonStatuses[j].gameObject.SetActive(true);
                i++;
                yield return new WaitForFixedUpdate();
            }
           
            j++;
            i = 0;
        }

       
    }
    public int GetRow()
    {
        return row;
    }
    public int GetColumn()
    {
        return column;
    }
    public float GetDistanceOfRow()
    {
        return distanceOfHexagon + DISTANCEOFROW;
    }
    public float GetDistanceOfColumn()
    {
        return distanceOfHexagon + DISTANCEOFCOLUMN;
    }
    /// <summary>
    /// Hexagon'un konum işlemlerini yapar
    /// </summary>
    /// <param name="_row"></param>
    /// <param name="_column"></param>
    /// <returns></returns>
    public Vector2 DeterminePositionOfHexagon(int _row,int _column)
    {
        float restore= _column % 2 == 1 ? (-GetDistanceOfRow() / 2) : 0;
        return new Vector2(_column * GetDistanceOfColumn(), _row * GetDistanceOfRow() + restore);
    }
}
