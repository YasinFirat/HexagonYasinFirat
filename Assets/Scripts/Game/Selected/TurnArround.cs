using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnArround : Master
{
    public void TurnClockWise()
    {
        List<Vector2Int> hexagonStatuses = gameManager.dedector.selectedObject;

        ReadyForTurnLeft(SortToBigArray(hexagonStatuses));
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
    public void ReadyForTurnLeft(List<Vector2Int> hexagonStatuses)
    {
        //liste normalde döndürme işlemi yapılacakları VectorInt olarak tuttuğundan dolayı
        //bunlara karşılık gelen bir hex listesi oluşturuyoruz.
        List<HexagonStatus> hex = new List<HexagonStatus>();
        //sayı değerlerine karşılık gelen hexagon'ları listeye ekliyoruz.
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            hex.Add(
                gameManager.creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y]
                );
        }
        /*Algoritma 2 türlü işliyor. 
         *Eğer küçük ve orta farklı ondalık değeri taşıyorsa; küçük => orta => büyük => küçük
         *Eğer küçük ve orta aynı ondalık değeri taşıyorsa ; küçük <= orta <= büyük <=küçük
         *şeklinde işeleme alınır
         */
        if (hexagonStatuses[0].x != hexagonStatuses[1].x)
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    gameManager.creativePoint[hexagonStatuses[0].x].hexagonStatuses[hexagonStatuses[0].y] =
                      hex[i];
                    continue;
                }
                gameManager.creativePoint[hexagonStatuses[i + 1].x].hexagonStatuses[hexagonStatuses[i + 1].y] =
                    hex[i];
            }
        }
        else
        {
            for (int i = 0; i < hexagonStatuses.Count; i++)
            {
                if (i == hexagonStatuses.Count - 1)
                {
                    gameManager.creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y] =
                      hex[0];
                    continue;
                }
                gameManager.creativePoint[hexagonStatuses[i].x].hexagonStatuses[hexagonStatuses[i].y] =
                    hex[i + 1];
            }
        }
        //gerekli değişiklikler yapıldı sıra geldi. hexagon sınıfını bilgilendirmeye ve ardından harekete geçirme döngüsüe.
        for (int i = 0; i < hexagonStatuses.Count; i++)
        {
            gameManager.creativePoint[hexagonStatuses[i].x].SetAllInformation().MoveAll();
        }
        //patlamaya yapılabiir bilgisini gerekli yere ulaştırıyoruz.
        gameManager.explodeHexagon.isContinueExplode = true;

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
