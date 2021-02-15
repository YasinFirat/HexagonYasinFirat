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
        selectedObject = new List<Vector2Int>();
        StartCoroutine(SelectHexagon(.3f));
    }
    public IEnumerator SelectHexagon(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        Debug.Log("5) Dedector"+ colliders.Length);
        Vector2Int keep;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.parent.CompareTag("Hexagon"))
            {
                keep = new Vector2Int(colliders[i].transform.parent.GetComponent<HexagonStatus>().column,
                        colliders[i].transform.parent.GetComponent<HexagonStatus>().row);
                if (!selectedObject.Contains(keep))
                {
                    selectedObject.Add(keep);
                }

            }
           
        }
        Debug.Log("7) isReadyForTurn aktif oldu");
        if (selectedObject.Count == 3)
        {
            CancelInvoke();
            isReadyForTurn = true;
            gameObject.SetActive(false);
        }
           
    }

    
}
