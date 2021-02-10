using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStatus : Hexagon
{
    public override Hexagon DoThisWhenFirstStart()
    {
        #if UNITY_EDITOR
        Debug.Log("Şimdilik Birşey Yapma");
        #endif
        return this;
    }

    public override Hexagon SetNestPosition(Vector2 _nestPosition)
    {
        #if UNITY_EDITOR
        Debug.LogWarning("Ana yuva'nın pozisyonunu değiştirmek istediğinize emin misin ? Eğer " +
            "Değiştirmek istiyorsanız buraya gerekli kodu yerleştiriniz. Kolay Gelsin :)  ");
        #endif
        return this;
    }

    public void SendInfoHexagon()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);

       // hit.transform.GetComponent<HexagonStatus>();

        //  hexagonStatus.SetInfoNest();
    }
}
