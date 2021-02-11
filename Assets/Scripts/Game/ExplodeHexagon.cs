using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeHexagon : Master
{
    public List<HexagonStatus> g = new List<HexagonStatus>();
   public void CheckExplode()
    {
        for (int x = 0; x < gameManager.column; x++)
        {
            for (int y = 0; y < gameManager.row; y++)
            {
                ExplodeStep1(x,y);
            }
        }

        for (int i = 0; i < g.Count; i++)
        {
            if (g[i].isActiveAndEnabled)
            {
                g[i].DisableYourself();
            }
        }
        Debug.Log("Check Edilecek-----------------");
        gameManager.CheckAllPositions();
    }

    public void ExplodeStep1(int x,int y)
    {
        List<Vector2Int> explodes = new List<Vector2Int>();
        Vector2Int[] points= { 
            new Vector2Int(x,y),
            new Vector2Int(x,y-1),
            new Vector2Int(x+1,y-x%2),
            new Vector2Int(x+1,y+1-x%2),
            new Vector2Int(x,y+1)
        };
        bool[] checks = new bool[points.Length]; // T , T-T-T-T
        checks[0] = true;
        
        int checkCounter = 0; //true sayısı 2 veya 2'den fazlaysa patlama olabilir
        int checkOddIndex=0; // 1 ve 3. indexler false değeri ise hiçbir şekilde patlama olmaz. T T F T F T
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i].y >= gameManager.row||points[i].y<0||points[i].x>=gameManager.column||points[i].x<0)
                continue;
            if(gameManager.creativePoint[points[0].x].GetColorKey(points[0].y)
                == gameManager.creativePoint[points[i].x].GetColorKey(points[i].y))
            {
                checks[i] = true;
                checkCounter++;
            }
            else
            {
                if (i % 2 == 1)
                {
                    checkOddIndex++;
                    if (checkOddIndex == 2)
                        return;
                }

            }
        }
       
        if ((!checks[2] && !checks[3])||checkCounter<1)
        {// 2 ve 3 indexlerin 2 tanesi false olur veya 2'den az true değer varsa aşağıdaki
            //işlemleri yapmak gereksiz olur. (0. index her zaman true)
            return;
        }
        for (int i = 1; i < checks.Length-1; i++)
        {
            if (checks[i] && checks[i + 1])
            {
                //if (explodes[explodes.Count - 1] != points[i])
                //{//eğer bu nokta daha önceden eklenmediyse ekle
                //    explodes.Add(points[i]);
                //}
                explodes.Add(points[i]);
                explodes.Add(points[i+1]);
            }
        }
        if (explodes.Count >= 2)
        {
            explodes.Add(points[0]);
        }
       
        for (int i = 0; i < explodes.Count&&explodes.Count>1; i++)
        {
            g.Add(gameManager.creativePoint[explodes[i].x].hexagonStatuses[explodes[i].y].gameObject.GetComponent<HexagonStatus>());
            
        }
    }
}
/*Algoritma
    * Algoritma şu şekilde olacak; 
    * x = column , y=row diyelim
    * 
    * xy için;
    * 
    * 1.Adım ) xy == x(y-1) == (x+1)(y - x%2)
    * 
    * 2.Adım ) xy == x(y+1) == (x+1)(y+1 - x%2)
    * 
    * 3.Adım ) xy == (x+1)(y - x%2) ==(x+1)(y+1-x%2)
    * 
    * 1. Adımdaki karşılaştırmaların hepsi eşit çıkarsa eşit çıkanlar patlatılır.
    *      Eğer eşit çıkmazsa 2. Adıma geçilir.
    * 2. Adımdaki karşılaştırmaların hepsi eşit çıkarsa eşit çıkanlar patlatılır.
    *      Eğer eşit çıkmazsa 3. Adıma geçilir.
    * 3. Adımdaki karşılaştırmalar eşit çıkarsa patlatılır çıkmazsa başka bir adım yoksa sonlandırılır.
    * 
    */