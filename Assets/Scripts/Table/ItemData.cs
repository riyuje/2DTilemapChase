[System.Serializable]
public class ItemData 
{
    public int id;
    public string name;
    public string description;
    public int effectiveEnemyId;  //効果のある敵の番号
    public float stopTime;  //敵が止まる時間
}
