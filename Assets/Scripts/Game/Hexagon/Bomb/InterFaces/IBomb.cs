
public interface IBomb
{
    /// <summary>
    /// Kalan atak sayısını gösterir.Değerlerle oynamaz !
    /// </summary>
    /// <returns></returns>
    int GetAttack();
    /// <summary>
    /// Her patlamadan sonra hamleleri sayar.
    /// </summary>
    /// <returns></returns>
     int AttackCounter();
    /// <summary>
    /// Kaç hamle hakkı olduğunu belirler
    /// </summary>
    /// <param name="startAmount">Hamle sayısı</param>
     void StartAttackCounterValue(int startAmount);
    /// <summary>
    /// AttackCounter, 0 değerine ulaştığında true döndürür.
    /// </summary>
    /// <returns></returns>
    bool CanStillMove();
}
