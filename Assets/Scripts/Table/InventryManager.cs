using UnityEngine;

public class InventryManager : MonoBehaviour
{
    public static InventryManager instance;

    [SerializeField]
    private InventorySlot[] inventorySlots;

    private InventorySlot selectedSlot;
    
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

    /// <summary>
    /// アイテム取得
    /// </summary>
    /// <param name="itemData"></param>

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
        
    }

    ///<summary>
    ///左クリック時
    ///</summary>
    public void SelectSlot(InventorySlot slot)
    {
        selectedSlot = slot;
        Debug.Log("選択中アイテム:" + slot.itemData.id);
    }

    ///<summary>
    ///右クリック時
    /// </summary>
    public void DiscardSlot(InventorySlot slot)
    {
        if (!slot.HasItem()) return;

        slot.ClearItem();

        if(selectedSlot == slot)
        {
            selectedSlot = null;
        }

        Debug.Log("アイテムを捨てる");
    }

    ///<summary>
    ///QTE成功時に呼ばれる
    /// </summary>
    public bool UseSelectedItemInQTE()
    {
        if (selectedSlot == null)
        {
            Debug.Log("選択アイテム無し");
            return false;
        }

        if (!selectedSlot.HasItem())
        {
            selectedSlot = null;
            return false;
        }

        ItemData item = selectedSlot.itemData;

        bool isWeak = QTEManager.instance.CheckWeakItemid(item.id);

        if (isWeak)
        {
            selectedSlot.ClearItem();
            Debug.Log("弱点一致！アイテム消費");
            selectedSlot = null;
            return true;
        }

        Debug.Log("弱点ではない");
        selectedSlot = null;
        return false;
    }

}
