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
    public List<Hexagon> hexagonStatuses;
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
        return hexagonStatuses[index].GetColor().GetKey();
    }
    public CreativePoint(int _column, int _row, float positionX,PoolManager _poolManager)
    {
        //Her Create noktasının bir başlangıç konumu vardır.(kamera'da görünmemesi için orthographicSize kullanıldı.)
        startPosition = new Vector2(positionX, (Camera.main.orthographicSize + 1) * 2);
        poolManager=_poolManager;
        hexagonStatuses = new List<Hexagon>();
        column = _column;
        maxRow = _row;
    }
    /// <summary>
    /// CreatePoint'e obje ekle işlemini yapar.
    /// </summary>
    /// <param name="hexagonStatus"></param>
    public void AddMember(Hexagon hexagonStatus)
    {
        hexagonStatus.SetRow(hexagonStatuses.Count).SetColumn(column);
        hexagonStatuses.Add(hexagonStatus);
    }
     
    /// <summary>
    /// Patlamadan sonra yeni yerleşim yeri belirlenir ve Oluşturulacak objeyi belirler.
    /// </summary>
    public void CheckPositionsAfterExplosion()
    {//eğer maxRow'a eşitse createPoint'in dolu olduğunu anlayıp işlem yaptırmıyoruz.
        if (hexagonStatuses.Count == maxRow)
            return;
        //maxRow eksik çıkınca maxRow'a ulaşıncaya kadar havuz'dan bir obje istedik.
        while (hexagonStatuses.Count != maxRow)
        {
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
        hexagonStatuses[i].GetComponent<PoolMember>().GoBackToPool();
    }
    /// <summary>
    /// Eğer hücre'deki obje active değilse remove edilir.
    /// </summary>
    public void ActiveControl()
    {
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            if (!hexagonStatuses[i].gameObject.activeInHierarchy)
            {
                hexagonStatuses.RemoveAt(i);
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
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hexagonStatuses[i].SetRow(i).SetColumn(column).SetNestPosition();
        }
        return this;
    }
    /// <summary>
    /// Tüm objeleri hareket ettirir.
    /// </summary>
    /// <returns></returns>
    public CreativePoint MoveAll()
    {
        for (int row = 0; row < hexagonStatuses.Count; row++)
        {
            hexagonStatuses[row].SetNestPosition();
            hexagonStatuses[row].Start_Move();
        }
        return this;
    }
    /// <summary>
    /// Tüm nesneler yuvalarına yerleştiğinde true değerini döndürür.
    /// </summary>
    /// <returns></returns>
    public bool IsArrivedAllMembers()
    { arriveCounter = 0;
       
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            arriveCounter += hexagonStatuses[i].isArrived ? 1:0;
        }

        return arriveCounter == maxRow;
    }

}
