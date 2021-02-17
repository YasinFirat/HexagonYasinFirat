using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonStatus : Hexagon
{
    public void OnEnable()
    {
        DoThisWhenOnEnabled();
    }
    
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }
}
