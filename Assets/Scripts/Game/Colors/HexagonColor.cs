using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexagonColor
{
    public Color32 color32;
    private int keyID;

    /// <summary>
    /// İki renk karşılaştırıldığında keyID ile karşılaştırılacak ve buna göre işlem yapılacaktır.
    /// Renkleri birbirine karşılaştırmak istemedim.
    /// </summary>
    /// <param name="_keyID">Diğer keylerden eşsiz olmalı</param>
    /// <returns></returns>
    public HexagonColor SetKey(int _keyID)
    {
        keyID = _keyID;
        return this;
    }
    public int GetKey()
    {
        return keyID;
    }
}
