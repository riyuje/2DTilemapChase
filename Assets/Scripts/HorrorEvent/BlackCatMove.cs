using UnityEngine;

public class BlackCatMove : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

