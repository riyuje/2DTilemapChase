using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Button actionButton; //ボタン参照を追加する

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
            itemIcon.sprite = itemData.iconSprite; //画像登録
            itemIcon.enabled = true;
        }
        else
        {
            hasItem = false;
            itemIcon.sprite = null; //アイテム欄、空のままにする
            itemIcon.enabled = false;
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

    /// <summary>
    /// 通常時:捨てる
    /// </summary>
    public void SetDiscardButton()
    {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(DiscardItem);
    }
    /// <summary>
    /// 通常時:使う
    /// </summary>
    public void SetUseButton()
    {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(UseItem);
    }
    /// <summary>
    /// QTE時
    /// </summary>
    public void SetQTEButton()
    {
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(PushQTEButton);
    }

    private void DiscardItem()
    {
        Debug.Log("アイテムを捨てる");
    }

    private void UseItem()
    {
        Debug.Log("アイテムを使う");
    }

    private void PushQTEButton()
    {
        Debug.Log("QTEボタン押下");
    }
}

