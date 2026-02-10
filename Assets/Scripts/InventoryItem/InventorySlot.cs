using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private Image itemIcon;

    public bool hasItem; //trueなら持っている,falseなら持っていない(名前によってtrueとfalseの処理を変える)

    public ItemData itemData;

    public void SetItemData(ItemData newItemData)
    {
        Debug.Log("newItemData取得" + newItemData?.id);

        itemData = newItemData;

        if (itemData != null)
        {
            hasItem = true;
            itemIcon.sprite = itemData.iconSprite; //画像登録
            itemIcon.enabled = true;
        }
        else
        {
            hasItem = false;
            itemIcon.sprite = null; //アイテム欄、空にする
            itemIcon.enabled = false;
        }
    }

    public bool HasItem()
    {
        return hasItem;
    }

    public void ClearItem()
    {
        SetItemData(null);
    }
    public void SetItem(Sprite sprite)
    {
        itemIcon.sprite = sprite;

        if (sprite == null)
        {
            itemIcon.enabled = false;
        }
        else
        {
            itemIcon.enabled = true;
        }

    }

    ///<summary>
    ///左右クリック判定
    ///</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasItem) return;

        //左クリック:選択
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            InventryManager.instance.SelectSlot(this);
        }
        //右クリック:捨てる
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            InventryManager.instance.DiscardSlot(this);
        }
    }
}

