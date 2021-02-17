using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    /// <summary>
    /// Hareket etmesi için gereken yazılım işleri yapılır.
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveEnumerator();
    /// <summary>
    /// Hareket etmesi için çağrılır.
    /// </summary>
    void Start_Move();
    /// <summary>
    /// Hareket hızı belirtilebilir veya okunabilir.
    /// </summary>
    public float Speed { get; set; }
}
