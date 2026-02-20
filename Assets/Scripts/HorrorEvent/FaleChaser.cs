using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class FakeChaser : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private bool isPause;
    [SerializeField] private float pauseTime;

    void Update()
    {
        if (!agent || !target || !tilemap) return;

        if (isPause)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0)
                isPause = false;

            return;
        }

        Vector3Int cellPos = tilemap.WorldToCell(target.position);
        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(cellPos);

        float distance = Vector3.Distance(transform.position, targetWorldPos);

        if (distance <= sightRange)
        {
            agent.SetDestination(targetWorldPos);

            // ‹ß‚Ã‚¢‚½‚çŒ¸‘¬i•|‚³‰‰oj
            if (distance < 1.5f)
                agent.speed = moveSpeed * 0.3f;
            else
                agent.speed = moveSpeed;
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void Setup(Transform target, Tilemap tilemap)
    {
        this.target = target;
        this.tilemap = tilemap;

        if (TryGetComponent(out agent))
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.destination = transform.position;
            agent.speed = moveSpeed;
        }
        else
        {
            Debug.LogWarning("NavMeshAgent‚ªŽæ“¾‚Å‚«‚Ü‚¹‚ñ");
        }
    }
}
