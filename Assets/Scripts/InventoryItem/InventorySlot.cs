using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    public bool hasItem; //trueなら持っている,falseなら持っていない(名前によってtrueとfalseの処理を変える)

    public ItemData itemData;

    public void SetItemData(ItemData newItemData)
    {
        Debug.Log("newItemData取得" + newItemData.id);

        itemData = newItemData;

        if (itemData != null)
        {
            hasItem = true;

            //画像登録
            itemIcon.sprite = itemData.iconSprite;
        }
        else
        {
            hasItem = false;
        }
    }

    public bool HasItem()
    {
        return hasItem;
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
}

