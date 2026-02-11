using UnityEngine;
using System.Linq; //ListŒŸõ”\—ÍŠg’£

public class TableManager : MonoBehaviour
{
    public static TableManager instance;
    
    [SerializeField]
    private ItemDataTable itemDataTable;

    [SerializeField]
    private EnemyDataTable enemyDataTable;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemData GetItemData(int itemid)
    {
        return itemDataTable.itemDataList.FirstOrDefault(data => data.id == itemid);
    }

    public EnemyData GetEnemyData(int enemyid)
    {
        return enemyDataTable.enemyDataList.FirstOrDefault(data => data.id == enemyid);
    }

    public int GetItemDataCount()
    {
        return itemDataTable.itemDataList.Count;
    }
}
