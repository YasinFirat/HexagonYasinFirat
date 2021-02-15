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
   [HideInInspector] public bool isReady;

    //Others
    public Dedector dedector;
    [HideInInspector] public TurnArround turnArround;


    #region Explodes
    public bool GetReadyExplode()
    {
        int counter = 0;
        for (int i = 0; i < creativePoint.Count; i++)
        {
            counter += creativePoint[i].IsArrivedAllMembers()?1:0;
        }

        return counter == creativePoint.Count;
       
    }
    public bool GetReadyTouch()
    {
        return !explodeHexagon.isContinueExplode;
    }
    public void SetReadyTouch(bool ready)
    {
        explodeHexagon.isContinueExplode = ready;
    }
    /// <summary>
    /// Patlama denetimi yapmak için çağrılır.
    /// </summary>
    public void ControlExplode()
    {
        explodeHexagon.isContinueExplode = true;
    }
    public void Explode()
    {
        if (!explodeHexagon.isContinueExplode)
        {
            CheckAllPoints();
            return;
        }
        Debug.Log("Patlatılacak Sayısı: " + explodeHexagon.CheckExplode().Count);
        List<Vector2Int> explodes = explodeHexagon.CheckExplode();
        if (explodes.Count == 0)
        {
            explodeHexagon.isContinueExplode = false;
        }
        for (int i = 0; i < explodes.Count; i++)
        {
            creativePoint[explodes[i].x].RemoveMember(explodes[i].y);

        }
        CheckAllPoints();

    }
    #endregion
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
        turnArround = new TurnArround();
      
    }

    #region ColumnAndRow
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
    public Vector2 DeterminePositionOfHexagon(int _row, int _column)
    {
        float restore = _column % 2 == 1 ? (-GetDistanceOfRow() / 2) : 0;
        return new Vector2(_column * GetDistanceOfColumn(), _row * GetDistanceOfRow() + restore);
    }

    #endregion
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
        yield return new WaitForSeconds(1);
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

    /// <summary>
    /// Küçük'ten büyüğe sıralar. (Döndürme işlemi içi kullanılır)
    /// </summary>
    /// <param name="hexagonStatuses"></param>
    /// <returns></returns>
    public List<Vector2Int> SortToBigArray(List<Vector2Int> hexagonStatuses) 
    {
        Vector2Int small;
    
        for (int i = 0; i < hexagonStatuses.Count-1; i++)
        {
            for (int j = i+1; j < hexagonStatuses.Count; j++)
            {

                if(ConvertToInteger(hexagonStatuses[i]) == ConvertToInteger(hexagonStatuses[j]))
                {
                    hexagonStatuses.RemoveAt(j);
                    j--;
                    continue;
                }
                if (ConvertToInteger(hexagonStatuses[i]) > ConvertToInteger(hexagonStatuses[j]))
                {
                    small = hexagonStatuses[j];
                    hexagonStatuses[j] = hexagonStatuses[i];
                    hexagonStatuses[i] = small;
                }
            }
        }
        return hexagonStatuses;
    }

    public void ChangePonts(List<Vector2Int> hexagonStatuses)
    {
        List<HexagonStatus> hex = new List<HexagonStatus>();
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hex.Add(
                creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y]
                );
        }

        if (hexagonStatuses[0].x != hexagonStatuses[1].x)
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    creativePoint[hexagonStatuses[0].x].hexagonStatuses[hexagonStatuses[0].y] =
                      hex[i];
                    continue;
                }
                creativePoint[hexagonStatuses[i + 1].x].hexagonStatuses[hexagonStatuses[i + 1].y] =
                    hex[i];
            }
        }
        else
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y] =
                      hex[0];
                    continue;
                }
                creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y] =
                    hex[i+1];
            }
        }
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            creativePoint[hexagonStatuses[i].x].SetAllInformation().MoveAll();
        }
        explodeHexagon.isContinueExplode = true;
       
    }
    public int ConvertToInteger(Vector2Int vector)
    {
        return vector.x * 10 + vector.y;
    }
        
    }
   
