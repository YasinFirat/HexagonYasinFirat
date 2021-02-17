using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dedector : MonoBehaviour
{
    public Transform circleObject;
    public float range;
    public LayerMask layerMask;
    public List<Vector2Int> selectedObject;
    public bool isReadyForTurn;


    public void OnEnable()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red> DEDECTOR AKTİF OLDU</color>");
#endif
#if UNITY_EDITOR
        Debug.Log("selectedObject listesi tekrardan oluşturulacak. Şimdilik boyutu : " + selectedObject.Count);
#endif

        selectedObject = new List<Vector2Int>();
#if UNITY_EDITOR
        Debug.Log("selectedObject listesi tekrardan oluşturuldu... Boyutu : " + selectedObject.Count);
#endif
        StartCoroutine(SelectHexagon(.3f));
    }
    public IEnumerator SelectHexagon(float delay)
    {
#if UNITY_EDITOR
        Debug.Log("SelectHexagon metodu çağrıldı ve seçili hexagonların tespiti için");
#endif
        yield return new WaitForSeconds(delay);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
#if UNITY_EDITOR
        Debug.Log(colliders.Length+" sayısı defa hexagon bulundu aynı hexagonları yok etmek için döngüye giriyoruz.");
#endif
       
        Vector2Int keep;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.parent.CompareTag("Hexagon"))
            {
                keep = new Vector2Int(colliders[i].transform.parent.GetComponent<Hexagon>().column,
                        colliders[i].transform.parent.GetComponent<Hexagon>().row);
                if (!selectedObject.Contains(keep))
                {
                    selectedObject.Add(keep);
                }

            }
           
        }
#if UNITY_EDITOR
        Debug.Log("Döngüden çıktık yeni selectedObject listesi  boyutu : " + selectedObject.Count);
#endif
        if (selectedObject.Count == 3)
        {
#if UNITY_EDITOR
            Debug.Log("isReadyForTurn aktif edildi");
#endif
            isReadyForTurn = true;
#if UNITY_EDITOR
            Debug.Log("Dedector kapatıldı.");
#endif
            gameObject.SetActive(false);
        }
           
    }

    
}
