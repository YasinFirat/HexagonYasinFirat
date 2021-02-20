using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnArround : Master
{
    public void TurnReverseClockWise()
    {
        List<Vector2Int> hexagonStatuses = gameManager.dedector.selectedObject;

        ReadyForTurn(SortToBigArray(hexagonStatuses),true);
        Debug.Log("Çark Sesi eklenebilir.");
    }
    public void StartTurn(bool _direction)
    {
        List<Vector2Int> hexagonStatuses = gameManager.dedector.selectedObject;

        ReadyForTurn(SortToBigArray(hexagonStatuses), _direction);
    }

    /// <summary>
    /// Küçük'ten büyüğe sıralar. (Döndürme işlemi içi kullanılır)
    /// </summary>
    /// <param name="hexagonStatuses"></param>
    /// <returns></returns>
    public List<Vector2Int> SortToBigArray(List<Vector2Int> hexagonStatuses)
    {
        Vector2Int small;

        for (int i = 0; i < hexagonStatuses.Count - 1; i++)
        {
            for (int j = i + 1; j < hexagonStatuses.Count; j++)
            {
                if (ConvertToInteger(hexagonStatuses[i]) == ConvertToInteger(hexagonStatuses[j]))
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
    /// <summary>
    /// Sol tarafa doğru işlem yapar.
    /// </summary>
    /// <param name="hexagonStatuses"></param>
    public void ReadyForTurn(List<Vector2Int> hexagonStatuses,bool isTurnLeft)
    {
      
        //liste normalde döndürme işlemi yapılacakları VectorInt olarak tuttuğundan dolayı
        //bunlara karşılık gelen bir hex listesi oluşturuyoruz.
        List<Hexagon> hex = new List<Hexagon>();
        //sayı değerlerine karşılık gelen hexagon'ları listeye ekliyoruz.
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hex.Add(
                gameManager.creativePoint[hexagonStatuses[i].x].hexagonList[hexagonStatuses[i].y]
                );
        }
        /*Algoritma 2 türlü işliyor. 
         *Eğer küçük ve orta farklı ondalık değeri taşıyorsa; küçük => orta => büyük => küçük
         *Eğer küçük ve orta aynı ondalık değeri taşıyorsa ; küçük <= orta <= büyük <=küçük
         *şeklinde işeleme alınır
         */
        if (hexagonStatuses[0].x != hexagonStatuses[1].x&&! isTurnLeft)
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    gameManager.creativePoint[hexagonStatuses[0].x].hexagonList[hexagonStatuses[0].y] =
                      hex[i];
                    continue;
                }
                gameManager.creativePoint[hexagonStatuses[i + 1].x].hexagonList[hexagonStatuses[i + 1].y] =
                    hex[i];
            }
        }
        else
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    gameManager.creativePoint[hexagonStatuses[i].x].hexagonList[hexagonStatuses[i].y] =
                      hex[0];
                    continue;
                }
                gameManager.creativePoint[hexagonStatuses[i].x].hexagonList[hexagonStatuses[i].y] =
                    hex[i + 1];
            }
        }
        //gerekli değişiklikler yapıldı sıra geldi. hexagon sınıfını bilgilendirmeye ve ardından harekete geçirme döngüsüe.
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            gameManager.creativePoint[hexagonStatuses[i].x].SetAllInformation().MoveAll();
        }
        //patlama arayışındayım bilgisini veriyor.
        gameManager.ContinueExplode();

    }

    /// <summary>
    /// Vector2Int tanımlı değişkeni integer'a çevirir.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public int ConvertToInteger(Vector2Int vector)
    {
        return vector.x * 10 + vector.y;
    }


}
