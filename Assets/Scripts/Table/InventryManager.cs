using UnityEngine;

public class InventryManager : MonoBehaviour
{
    public static InventryManager instance;

    [SerializeField]
    private InventorySlot[] inventorySlots;
    
    public int bombCount; //持っている爆弾の数
    public int clackerCount; //持っているクラッカーの数
    public int liteCount;  //持っている懐中電灯の数

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

    public void GetItem(ItemData itemData)
    {
        Debug.Log("GetItemメソッド");

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].HasItem() == false)
            {

                inventorySlots[i].SetItemData(itemData);

                break;
            }
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (QTEManager.instance == null)
        {
            return;
        }

        if (QTEManager.instance.IsPlayingQTE())
        {
            SetAllSlotsQTE();
        }
        else
        {
            SetAllSlotsNormal();
        }
    }

    private void SetAllSlotsQTE()
    {
        foreach(var slot in inventorySlots)
        {
            if (slot.HasItem())
            {
                slot.SetQTEButton();
            }
        }
    }

    private void SetAllSlotsNormal()
    {
        foreach(var slot in inventorySlots)
        {
            if (slot.HasItem())
            {
                slot.SetUseButton();　//通常時:使うボタン

                slot.SetDiscardButton();　//通常時:捨てるボタン
            }
        }
    }
}
