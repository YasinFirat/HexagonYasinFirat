using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Master
{
    public Transform circle;
    public CircleObject circleObject;
    Vector2 point;

    void Start()
    {
        //circleObject = circle.gameObject.GetComponent<CircleObject>();
        gameManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetReadyExplode())
        {
            gameManager.Explode();
        }
    }


    private void OnMouseDown()
    {

#if UNITY_EDITOR
        Debug.Log("0) Dokunma işkemi yapıldı ve GetReadyTouch " + gameManager.GetReadyTouch());
#endif
        if (gameManager.GetReadyTouch())
        {
            BeginTouch();

            StartCoroutine(CheckTurnAvaible());
        }
    }

    public void BeginTouch()
    {

#if UNITY_EDITOR
        Debug.Log("1) Begin Touch işlemleri yapılıyor.Çember Pasif edildi.");
#endif
        circle.gameObject.SetActive(false);
        circleObject.SetVisibleSprite(false);
        circleObject.SetColliderReader(false);

#if UNITY_EDITOR
        Debug.Log("dokunulan point bölgesi belirlendi");
#endif
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#if UNITY_EDITOR
        Debug.Log("Çember Point bölgesine gelir.");
#endif
        circle.localPosition = point;
#if UNITY_EDITOR
        Debug.Log("Çember Aktif edilir.");
#endif
        circleObject.SetColliderReader(true);
        circle.gameObject.SetActive(true);
    }

    IEnumerator CheckTurnAvaible()
    {
#if UNITY_EDITOR
        Debug.Log("dedector sınıfındna isReadyForTurn kontrol edilir. isREadyForTurn : "+ gameManager.dedector.isReadyForTurn);
#endif
        while (!gameManager.dedector.isReadyForTurn)
        {
            yield return new WaitForFixedUpdate();
        }
#if UNITY_EDITOR
        Debug.Log("dedector sınıfından isReadyForTurn " + gameManager.dedector.isReadyForTurn + "\n 3 defa dönme fonksiyonu çalışır.");
#endif
        for (int i = 0; i < 3&&gameManager.GetReadyTouch(); i++)
        {
            gameManager.turnArround.TurnClockWise();
           yield return new WaitForSeconds(1);
        }
#if UNITY_EDITOR
        Debug.Log("Dönme işlemi durduruldu veya tamamlandı ve dedector isReadtTurn "+ gameManager.dedector.isReadyForTurn + " edildi.");
#endif
        gameManager.SetReadyTouch(false);
        gameManager.dedector.isReadyForTurn = false;
    }
}
