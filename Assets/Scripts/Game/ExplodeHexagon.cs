using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Burada patlanacak objeleri bulan algoritma geliştirildi.
/// (Oyun tamamlandıktan sonra optimize edilecek ve OOP'ye uygun hale getirilecek.)
/// </summary>
public class ExplodeHexagon
{
    public GameManager gameManager;
    public List<Vector2Int> restore;
    bool[] checks;

    public ExplodeHexagon(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }
    //list olacak restore döndürecek.
    public List<Vector2Int> CheckExplode()
    {
        restore = new List<Vector2Int>();
        //Sırayla aşağıdan yukarıya tüm noktalar kontrol edilir.
        for (int x = 0; x < gameManager.column; x++)
        {
            for (int y = 0; y < gameManager.row; y++)
            {
                ExplodeStep(x,y);
            }
        }
        return restore;
    }
    
    /// <summary>
    /// Karşılaştırma işlemleri burada yapılır ve gereken şartları sağlıyorsa
    /// bir sonraki aşamaya geçilir.
    /// </summary>
    /// <param name="_points">Kontrol edilecek noktalar</param>
    /// <param name="_checks">_points indexi şartları sağlıyorsa _checks index'ine true değerini yazdırır.</param>
    private void EqualsColors(Vector2Int[] _points,bool[] _checks)
    {
        checks[0] = true;
        int checkCounter = 0; //true sayısı 2 veya 2'den fazlaysa patlama olabilir
        int checkOddIndex = 0; // 1 ve 3. indexler false değeri ise hiçbir şekilde patlama olmaz. T T F T F T
        for (int i = 1; i < _points.Length; i++)
        {
            //eğer row veya column'dan biri limitin altında veya üstünde kalıyorsa işlem yapılmaz.false kalır.
            if (!gameManager.EqualColumn(_points[i].x) || !gameManager.EqualRow(_points[i].y))
                continue;
            //limit ile eşleşiyorsa renkler birbirine eşit ise true değerini döndürür.
            if (gameManager.creativePoint[_points[0].x].GetColorKey(_points[0].y)
                == gameManager.creativePoint[_points[i].x].GetColorKey(_points[i].y))
            {
                _checks[i] = true;
                checkCounter++;
            }
            else
            { // T0 T1 T2 T3 T4 , eğer tek sayılar F olursa patlama imkansız. TFTFT
                //patlama için 0. index haricinde yan yana iki tane T olması gerekiyor.
                if (i % 2 == 1)
                {
                    checkOddIndex++;
                    if (checkOddIndex == 2)
                        return; //bu point üzerinden daha fazla işlem yapmanın anlamı yok.
                }
                
            }
        }
        if ((!_checks[2] && !_checks[3]) || checkCounter < 2)
        {// 2 ve 3 indexlerin 2 tanesi false olur veya 2'den az true değer varsa aşağıdaki
            //işlemleri yapmak gereksiz olur. (0. index her zaman true)
            return;
        }
       
    }
    /// <summary>
    /// Patlama için hazır uygun olan noktaları bir listede toplar
    /// </summary>
    /// <param name="_points">kontrol edilen noktalar</param>
    /// <param name="_explodes">patlama için hazır olan noktalar.(bu liste boş olarak tanımlanmalı)</param>
    /// <param name="_restore">global patlama için uygun olan noktalar</param>
    private void DetermineTheBombToBeExplode(Vector2Int[] _points,List<Vector2Int> _explodes,List<Vector2Int> _restore)
    {
        for (int i = 1; i < checks.Length - 1; i++)
        {//eğer iki TT yan yana geliyorsa patlama listesine eklenir.
            if (checks[i] && checks[i + 1])
            {
                _explodes.Add(_points[i]);
                _explodes.Add(_points[i + 1]);
            }
        }
        if (_explodes.Count < 2)
            return;
        _explodes.Add(_points[0]);
        for (int i = 0; i < _explodes.Count && _explodes.Count > 1; i++)
        {
            // g.Add(gameManager.creativePoint[explodes[i].x].hexagonStatuses[explodes[i].y].gameObject.GetComponent<HexagonStatus>());
            //eğer verilen index ile alakalı point yoksa ekle
            if (!_restore.Contains(_explodes[i]))
            {
                _restore.Add(_explodes[i]);
            }
        }
    }

    public void ExplodeStep(int x,int y)
    {
        List<Vector2Int> explodes = new List<Vector2Int>();
        Vector2Int[] points= { 
            new Vector2Int(x,y),
            new Vector2Int(x,y-1),
            new Vector2Int(x+1,y-x%2),
            new Vector2Int(x+1,y+1-x%2),
            new Vector2Int(x,y+1)
        };
        checks = new bool[points.Length];
        
        EqualsColors(points,checks);

        DetermineTheBombToBeExplode(points,explodes,restore);
    }
}
