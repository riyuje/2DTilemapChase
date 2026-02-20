using UnityEngine;
using UnityEngine.Tilemaps;

public class FakeChaser : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private bool isPause;
    [SerializeField] private float pauseTime;

    void Update()
    {
        if (!target) return;

        if (isPause)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0)
                isPause = false;

            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= sightRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );

            // ‹ß‚Ã‚¢‚½‚çŒ¸‘¬i•|‚³‰‰oj
            if (distance < 1.5f)
                moveSpeed = 0.5f;
        }
    }

    public void Setup(Transform target, Tilemap tilemap)
    {
        this.target = target;
        this.tilemap = tilemap;
    }

    public void SetPause(float time)
    {
        pauseTime = time;
        isPause = true;
    }
}