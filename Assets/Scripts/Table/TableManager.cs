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

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
