using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Oyundaki genel ayarlar buradan yapılacak ve çoğu sınıf singleton benzeri bir yapı ile bağlanacak.
/// </summary>
public class GameManager : MonoBehaviour
{
    public readonly float DISTANCEOFROW = 0.86f;
    public readonly float DISTANCEOFCOLUMN = 0.75f;
    [Header("Hexagon Oyun Ayarları")]
    [Tooltip("Sütun değerleri giriniz.")]
    public int row;
    [Tooltip("Satır değerleri giriniz.")]
    public int column;
    [Tooltip("Hexagon'lar arası mesafe.")]
    public float distanceOfHexagon = .1f;
    [HideInInspector]public List<CreativePoint> creativePoint;
    [Header("Colors")]
    public List<HexagonColor> hexagonColors;
    public ExplodeHexagon explodeHexagon;
    //Others
    [Tooltip("Dedector script'ine sahip obje'ji sürükle")]
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
    /// Girilen değer 0'dan büyük ve column limitini aşmıyorsa true döndürür.
    /// </summary>
    /// <param name="_column"></param>
    /// <returns></returns>
    public bool EqualColumn(int _column)
    {
        if (_column >= 0 && _column < column)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Girilen değer 0'dan büyük ve row limitini aşmıyorsa true döndürür.
    /// </summary>
    /// <param name="_row"></param>
    /// <returns></returns>
    public bool EqualRow(int _row)
    {
        if (_row >= 0 && _row < row)
        {
            return true;
        }
        return false;
    }
    #endregion

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
}
   
