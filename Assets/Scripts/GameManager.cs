using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Oyundaki genel ayarlar buradan yapılacak ve çoğu sınıf singleton benzeri bir yapı ile bağlanacak.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Sadece okuyabilirsiniz.Çünkü normal bir hexagon'un bir birim row mesafesi olarak hesaplandı.
    /// </summary>
    public readonly float DISTANCEOFROW = 0.86f;
    /// <summary>
    /// Sadece okuyabilirsiniz.Çünkü normal bir hexagon'un bir birim column mesafesi olarak hesaplandı.
    /// </summary>
    public readonly float DISTANCEOFCOLUMN = 0.75f;
    [Header("Hexagons Settings")]
    [Tooltip("Sütun değerleri giriniz.")]
    public int row;
    [Tooltip("Satır değerleri giriniz.")]
    public int column;
    [Tooltip("Hexagon'lar arası mesafe.")]
    public float distanceOfHexagon = .1f;
    [HideInInspector]public List<CreativePoint> creativePoint;
    [Header("Bomb Settings")]
    public BombCounter bombCounter;
    public int amountAttack = 5;
    [Header("Score Settings")]
    public Score score;
    [Header("Colors")]
    public List<HexagonColor> hexagonColors;
    public ExplodeHexagon explodeHexagon;
    [Header("Others")]
    [Tooltip("Dedector script'ine sahip obje'ji sürükle")]
    public Dedector dedector;
    [HideInInspector] public TurnArround turnArround;
    [HideInInspector]public Hexagon bomb;
    public GameObject endGamePanel;
    public bool isEndGame;


    #region Explodes
    /// <summary>
    /// Aktif olan tüm hexagon'ların bilgilerine ulaşır ve hepsi yerlerine yerleştiyse true değerini döndürür.
    /// Bu sayede patlama yapıp yapamayacağınıza karar verebilirsiniz.
    /// </summary>
    /// <returns></returns>
    public bool GetReadyExplode()
    {//true ise  hexagonlar yerine yerleşti. Patlama denetimi yapılabilir.
        //false ise halen yerine yerleşmeyen var.
        int counter = 0;
        for (int i = 0; i < creativePoint.Count; i++)
        {
            counter += creativePoint[i].IsArrivedAllMembers()?1:0;
        }
        return counter == creativePoint.Count;
       
    }
   
    /// <summary>
    /// Dokunma olup olmadığını belirtir.
    /// </summary>
    public bool ReadyTouch { get { return !explodeHexagon.isContinueExplode; } }
    /// <summary>
    ///  Dönme'nin devam edip edemeyeceğini veya dönme olayı gerçekleşiyor mu gibi durumlar için kullanılır.
    /// </summary>
    public bool ReadyTurn { get { return dedector.isReadyForTurn; } set { dedector.isReadyForTurn = value; } }
    /// <summary>
    /// Döndürme işlemi yapılır yapılmaz ilk patlamada gerçekleşecek işlemler.
    /// </summary>
    public void BeginExplodeWhenTouchScreen()
    {
        score.SetTextMoves();
        if(bomb!=null)
            bomb.DoThisWhenMovesAttack();
    }
    /// <summary>
    /// Patlama denetimi yapmak için çağrılır.
    /// </summary>

    public void ContinueExplode()
    {
        explodeHexagon.isContinueExplode = true;
    }
    /// <summary>
    /// Patlama işlemleri gerçekleşir.
    /// </summary>
    public void Explode()
    {
        if (ReadyTouch)
        {
            CheckAllPoints();
            return;
        }
        score.AddScore(explodeHexagon.CheckExplode().Count);
        
        List<Vector2Int> explodes = explodeHexagon.CheckExplode();
        if (explodes.Count == 0)
        {
            return;
        }
        else
        {
            ReadyTurn = false;
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
        //key eklendi.Renk karşılaştırmaları Getkey() ile yapılacak.
        for (int i = 0; i < hexagonColors.Count; i++)
        {
            hexagonColors[i].SetKey(i);
        }
        explodeHexagon = new ExplodeHexagon(this);
        turnArround = new TurnArround();
       
       
        //Oyun başlatılır.
        StartCoroutine(StartThisGame());
    }
    /// <summary>
    /// Oyun Bittiğinde çalışır.
    /// </summary>
    public void EndGame()
    {
        isEndGame = true;
        for (int i = 0; i < creativePoint.Count; i++)
        {
            creativePoint[i].RemoveAllMember();
        }
        endGamePanel.SetActive(true);
        Debug.Log("Oyun Bittiğinde yapılacaklar. ");
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
    /// <summary>
    /// Row'lar arasındaki boşlukları yani mesafeyi döndürür.
    /// </summary>
    /// <returns></returns>
    public float GetDistanceOfRow()
    {
        return distanceOfHexagon + DISTANCEOFROW;
    }
    /// <summary>
    /// Column'lar arasındaki boşlukları yani mesafeyi döndürür.
    /// </summary>
    /// <returns></returns>
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
            creativePoint[i].CheckPositionsAfterExplosion(this);
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
                creativePoint[i].hexagonList[j].gameObject.SetActive(true);
                i++;
                yield return new WaitForFixedUpdate();
            }
            j++;
            i = 0;
        }
    }
}
   
