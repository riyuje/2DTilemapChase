using UnityEngine;

public class itemPickup : MonoBehaviour
{
    [SerializeField]
    private int itemId;

    public ItemData itemData;

    private void Start()
    {
        itemId = Random.Range(0, TableManager.instance.GetItemDataCount());

        itemData = TableManager.instance.GetItemData(itemId);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("アイテムを取得した");
            InventryManager.instance.GetItem(itemData);

            //取得後、アイテム削除
            Destroy(gameObject);
        }
    }

}
