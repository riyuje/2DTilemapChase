using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyDataTable", menuName = "Scriptable Objects/EnemyDataTable")]
public class EnemyDataTable : ScriptableObject
{
    public List<EnemyData> enemyDataList = new();
}
