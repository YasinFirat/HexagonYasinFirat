using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script was created to be accessed from everywhere
/// </summary>
public abstract class Master : MonoBehaviour
{
    private PoolManager _poolManager;
    private GameManager _gameManager;


    public GameManager gameManager
    {
        get
        {
            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
            }
            return _gameManager;
        }
    }

    public PoolManager poolManager
    {
        get
        {
            if (_poolManager == null)
            {
                _poolManager = FindObjectOfType<PoolManager>();
            }
            return _poolManager;
        }
    }
}
