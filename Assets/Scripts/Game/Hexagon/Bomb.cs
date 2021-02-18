using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Hexagon, IBomb
{
    private int remainingAttacks;
    public int AttackCounter()
    {
        remainingAttacks--;
        return remainingAttacks;
    }

    public bool ControlEndGame()
    {
        return remainingAttacks <= 0;
    }

    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public int GetAttack()
    {
        return remainingAttacks;
    }

    public void StartAttackCounterValue(int startAmount)
    {
        remainingAttacks = startAmount;
    }

    private void OnEnable()
    {
        DoThisWhenOnEnabled();
        StartAttackCounterValue(gameManager.amountAttack);
    }

}
