using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : Master
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetColliderReader(false);
        Debug.Log("1) Circle Pozisyon Aldı");
        transform.localPosition = (Vector2)collision.transform.position +
           collision.GetComponent<PolygonCollider2D>().points[2];
        Debug.Log("2) Circle görünür hale geldi.");
        //görünür hale gelir.
        SetVisibleSprite(true);
        //dedector açılır.
        Debug.Log("3) Dedector Acildi");
        gameManager.dedector.transform.localPosition = transform.localPosition;
       gameManager.dedector.gameObject.SetActive(true);

    }

    
    public void SetVisibleSprite(bool _isVisible)
    {
        spriteRenderer.enabled = _isVisible;
    }
    /// <summary>
    /// Simulate açık olunca diğer colliderlarla temasa girip lag oluşmasını engelleiyoruz
    /// </summary>
    /// <param name="_canRead"></param>
    public void SetColliderReader(bool _canRead)
    {
        rb2D.simulated = _canRead;
    }


    public IEnumerator StartDedector(float delay)
    {
        gameManager.dedector.transform.localPosition = transform.localPosition;
        yield return new WaitForSeconds(delay);
        
    }
   
}
