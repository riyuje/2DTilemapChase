using UnityEngine;
using UnityEngine.Tilemaps;

public class HorrorEventSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] eventPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private Tilemap tilemap;

    public void SpawnRandomEvent(float exposure)
    {
        if (eventPrefabs.Length == 0) return;

        int index = Random.Range(0, eventPrefabs.Length);

        Vector3 spawnPos = GetSpawnPosition();

        GameObject obj = Instantiate(eventPrefabs[index], spawnPos, Quaternion.identity);

        // FakeChaserなら初期化
        FakeChaser fakechaser = obj.GetComponent<FakeChaser>();
        if (fakechaser != null)
        {
            fakechaser.Setup(player, tilemap);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float y = Random.Range(-height / 2f, height / 2f);
        Vector3 camPos = cam.transform.position;

        return new Vector3(camPos.x - width / 2f - 2f, camPos.y + y, 0);
    }
}
