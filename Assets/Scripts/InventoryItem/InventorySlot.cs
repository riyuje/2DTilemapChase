using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] 
    private Image itemIcon;

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

