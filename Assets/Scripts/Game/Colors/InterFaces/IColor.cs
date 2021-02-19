using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColor
{
    /// <summary>
    /// Tanımlandığı obje'ye renk atamasını yapar.
    /// </summary>
    /// <param name="_hexagonColor"></param>
    public void SetColor(HexagonColor _hexagonColor);
    /// <summary>
    /// Bulunduğu obje'nin renk bilgilerini verir.
    /// </summary>
    /// <returns></returns>
    public HexagonColor GetColor();
    /// <summary>
    /// Obje'ye random olarak renk ve bilgilerini döndürür.
    /// </summary>
    /// <returns></returns>
    public HexagonColor GetRandomColor();
    
}
