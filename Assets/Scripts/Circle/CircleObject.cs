using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : Master
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetColliderReader(false);

#if UNITY_EDITOR
        Debug.Log("Dokunulan bölge'de köşe noktanun koordinatları çembere verildi ve çember o noktaya yerleştirildi");
#endif
        transform.localPosition = (Vector2)collision.transform.position +
           collision.GetComponent<PolygonCollider2D>().points[2];
        
        SetVisibleSprite(true);
#if UNITY_EDITOR
        Debug.Log("Dedector acildi ve çemberin pozisyonuna yerleştirildi.");
#endif
        gameManager.dedector.transform.localPosition = transform.localPosition;
#if UNITY_EDITOR
        Debug.Log("Dedector Actif edildi.");
#endif
        gameManager.dedector.gameObject.SetActive(true);

    }

    
    public void SetVisibleSprite(bool _isVisible)
    {
#if UNITY_EDITOR
        Debug.Log("Sprite "+ _isVisible + " Edildi.");
#endif
        spriteRenderer.enabled = _isVisible;
    }
    /// <summary>
    /// Simulate açık olunca diğer colliderlarla temasa girip lag oluşmasını engelleiyoruz
    /// </summary>
    /// <param name="_canRead"></param>
    public void SetColliderReader(bool _canRead)
    {
#if UNITY_EDITOR
        Debug.Log("Collider " + _canRead + " edildi.");
#endif
        rb2D.simulated = _canRead;
    }


    public IEnumerator StartDedector(float delay)
    {
        gameManager.dedector.transform.localPosition = transform.localPosition;
        yield return new WaitForSeconds(delay);
        
    }
   
}
