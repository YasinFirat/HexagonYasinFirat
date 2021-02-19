using UnityEngine.UI;

public class Bomb : Hexagon, IBomb
{
    private int remainingAttacks;
    public Text text;
    public int AttackCounter()
    {
        remainingAttacks--;
        text.text = remainingAttacks.ToString();
        return remainingAttacks;
    }
    public bool CanStillMove()
    {
        return remainingAttacks <= 0;
    }

    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public override void DoThisWhenMovesAttack()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameManager.bomb = null;
            return;
        }
            

        AttackCounter();
        if (CanStillMove())
        {
            gameManager.EndGame();
        }
    }

    public int GetAttack()
    {
        return remainingAttacks;
    }

    public void StartAttackCounterValue(int startAmount)
    {
        remainingAttacks = startAmount;
        text.text = remainingAttacks.ToString();
    }

    private void OnEnable()
    {
       
        DoThisWhenOnEnabled();
        StartAttackCounterValue(gameManager.amountAttack);

    }

}
