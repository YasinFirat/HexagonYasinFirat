/// <summary>
/// Every pool member has this script.
/// When the member wants to go to the pool again, member applies to this class.
/// 
/// </summary>
public class PoolMember: Master
{
    public Pooling pooling;
    public PoolNames POOLNAMES;

    /// <summary>
    /// If you do not know which pool the object belongs to, 
    /// you can send the object to its repository with this method.
    /// </summary>
    public void GoBackToPool()
    {
        poolManager.BackToPool(POOLNAMES, this.gameObject);
    }
}