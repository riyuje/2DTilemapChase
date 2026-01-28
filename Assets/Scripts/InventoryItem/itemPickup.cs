using UnityEngine;

public class itemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("アイテムを取得した");

            //取得後、アイテム削除
            Destroy(gameObject);
        }
    }

}
