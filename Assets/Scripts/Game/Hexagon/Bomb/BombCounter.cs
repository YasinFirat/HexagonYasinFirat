[System.Serializable]
public class BombCounter
{
    /// <summary>
    /// Skor hangi değere ulaştığında bomba oluşsun ?
    /// </summary>
    public int eachScore = 1000;
    int counter = 0;
    int keep;

    /// <summary>
    /// Eğer istenilen değere ulaşırsa bomba kurmaya izin verilecek.
    /// </summary>
    /// <param name="_score">Skor değerini giriniz</param>
    /// <returns></returns>
    public bool CanCreateBomb(int _score)
    {
        keep = _score / eachScore;
        if (counter < keep)
        {
            counter = keep;
            return true;
        }
        return false;
    }
}
