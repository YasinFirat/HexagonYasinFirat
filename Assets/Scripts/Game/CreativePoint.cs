using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Nesneleri row olarak dizilmesini ve kontrol edilmesini sağlar.
/// </summary>
[System.Serializable]
public class CreativePoint
{
    private int arriveCounter;
    /// <summary>
    /// Bu sınıfta en fazla ne kadar row tanımlayacağımızın sınırını belirler.
    /// </summary>
    public int maxRow;
    /// <summary>
    /// Eğer dizi olarak tanımlandıysa bu sınıf, dizideki index numarasını tanımlayın.
    /// </summary>
    public int column; 
    /// <summary>
    /// hexagon listesine tanımlanmış obje'lerin aktif olurken bulunacağı konumu belirler.
    /// </summary>
    public Vector2 startPosition;
    /// <summary>
    /// Row olarak atanmasını istediğiniz hexagon obje'lerini ekler.
    /// </summary>
    public List<Hexagon> hexagonList;
    /// <summary>
    /// Pooling işlemini yapması ve kolay ulaşılması için eklendi.
    /// </summary>
    public PoolManager poolManager;

    /// <summary>
    /// Liste'de belirtilen obje'nin renk key'ini gösterir.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetColorKey(int index)
    {
        return hexagonList[index].GetColor().GetKey();
    }
    public CreativePoint(int _column, int _row, float positionX,PoolManager _poolManager)
    {
        //Her Create noktasının bir başlangıç konumu vardır.(kamera'da görünmemesi için orthographicSize kullanıldı.)
        startPosition = new Vector2(positionX, (Camera.main.orthographicSize + 1) * 2);
        poolManager=_poolManager;
        hexagonList = new List<Hexagon>();
        column = _column;
        maxRow = _row;
    }
    /// <summary>
    /// CreatePoint'e obje ekle işlemini yapar.
    /// </summary>
    /// <param name="hexagonStatus"></param>
    public void AddMember(Hexagon hexagonStatus)
    {
        hexagonStatus.SetRow(hexagonList.Count).SetColumn(column);
        hexagonList.Add(hexagonStatus);
    }
     
    /// <summary>
    /// Patlamadan sonra yeni yerleşim yeri belirlenir ve Oluşturulacak objeyi belirler.
    /// </summary>
    public void CheckPositionsAfterExplosion(GameManager gameManager)
    {//eğer maxRow'a eşitse createPoint'in dolu olduğunu anlayıp işlem yaptırmıyoruz.
        if (hexagonList.Count == maxRow||gameManager.isEndGame)
            return;
        //maxRow eksik çıkınca maxRow'a ulaşıncaya kadar havuz'dan bir obje istedik.
        while (hexagonList.Count != maxRow)
        {
            if (gameManager.bombCounter.CanCreateBomb(gameManager.score.GetScore()))
            {
                if (gameManager.bomb != null)
                    continue;
                
                gameManager.bomb = poolManager.PullFromPool(PoolNames.bomb, startPosition)
                .GetComponent<Hexagon>();
                AddMember(gameManager.bomb);
                continue;
            }
            AddMember(poolManager.PullFromPool(PoolNames.hexagon, startPosition)
                .GetComponent<Hexagon>());
        }
        SetAllInformation().MoveAll();
    }
    /// <summary>
    /// Obje önce havuza gönderilir ardından listeden silinir.
    /// </summary>
    /// <param name="i"></param>
    public void RemoveMember(int i)
    {
        hexagonList[i].GetComponent<PoolMember>().GoBackToPool();
    }
    /// <summary>
    /// Oyun bittiğinde tüm objeleri yok eder.
    /// </summary>
    public void RemoveAllMember()
    {
        for (int i = 0; i < hexagonList.Count; i++)
        {
            hexagonList[i].GetComponent<PoolMember>().GoBackToPool();
        }

    }
    /// <summary>
    /// Eğer hücre'deki obje active değilse remove edilir.
    /// </summary>
    public void ActiveControl()
    {
        for (int i = 0; i < hexagonList.Count; i++)
        {
            if (!hexagonList[i].gameObject.activeInHierarchy)
            {
                hexagonList.RemoveAt(i);
                i--;
            }

        }
    }
    /// <summary>
    /// Tüm objelerine gerekli row ve column değerlerini verir ve bulunması gereken noktayı belirler.
    /// </summary>
    /// <returns></returns>
    public CreativePoint SetAllInformation() 
    {
        for (int i = 0; i < hexagonList.Count; i++)
        {
            hexagonList[i].SetRow(i).SetColumn(column).SetNestPosition();
        }
        return this;
    }
    /// <summary>
    /// Tüm objeleri hareket ettirir.
    /// </summary>
    /// <returns></returns>
    public CreativePoint MoveAll()
    {
        for (int row = 0; row < hexagonList.Count; row++)
        {
            hexagonList[row].SetNestPosition();
            hexagonList[row].Start_Move();
        }
        return this;
    }
    /// <summary>
    /// Tüm nesneler yuvalarına yerleştiğinde true değerini döndürür.
    /// </summary>
    /// <returns></returns>
    public bool IsArrivedAllMembers()
    { arriveCounter = 0;
       
        for (int i = 0; i < hexagonList.Count; i++)
        {
            arriveCounter += hexagonList[i].isArrived ? 1:0;
        }

        return arriveCounter == maxRow;
    }

}
