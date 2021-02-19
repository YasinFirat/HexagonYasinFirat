using UnityEngine;

public class HexagonStatus : Hexagon
{
    public void OnEnable()
    {
        DoThisWhenOnEnabled();
    }
    
    public override Hexagon DoThisWhenFirstStart()
    {
        gameObject.SetActive(false);
        return this;
    }

    public override void DoThisWhenMovesAttack()
    {
        Debug.Log("Birşey Yapma");
    }
}
