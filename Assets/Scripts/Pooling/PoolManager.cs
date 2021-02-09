
using UnityEngine;
/// <summary>
/// Build pools here
/// </summary>
public class PoolManager : MonoBehaviour
{
    public Pooling[] poolings;
    
    private void Awake()
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            poolings[i].FillPool();
        }
    }

    /// <summary>
    /// The object goes back to the pool where it was extracted.
    /// </summary>
    /// <param name="POOLNAMES">The enum tag of the Pool of which it is a member</param>
    /// <param name="gameObject">Object that needs to go to the pool</param>
    public void BackToPool(PoolNames POOLNAMES, GameObject gameObject)
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                poolings[i].AddPool(gameObject);
                break;
            }
        }
        
      
    }

    /// <summary>
    /// The object is called up and removed from the pool for use.
    /// </summary>
    /// <param name="POOLNAMES">The enum tag of the Pool of which it is a member</param>
    /// <param name="_position"> </param>
    /// <returns></returns>
    public GameObject PullFromPool(PoolNames POOLNAMES, Vector3 _position)
    {
       
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                return poolings[(int)POOLNAMES].PullFromPool(_position);
                
            }
        }
        return null;

    }

    public int GetRow(PoolNames POOLNAMES)
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                return poolings[(int)POOLNAMES].row;
            }
        }
        return 0;
    }
    public int GetColumn(PoolNames POOLNAMES)
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                return poolings[(int)POOLNAMES].column;
            }
        }
        return 0;
    }
    public float GetDistanceOfRow(PoolNames POOLNAMES)
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                return poolings[(int)POOLNAMES].distanceOfHexagon+HexagonInfo.DISTANCEOFROW;
            }
        }
        return 0;
    }
    public float GetDistanceOfColumn(PoolNames POOLNAMES)
    {
        for (int i = 0; i < poolings.Length; i++)
        {
            if (poolings[i].POOLNAMES == POOLNAMES)
            {
                return poolings[(int)POOLNAMES].distanceOfHexagon + HexagonInfo.DISTANCEOFCOLUMN;
            }
        }
        return 0;
    }


}
