using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class HorrorEventSpawner : MonoBehaviour
{
    [SerializeField] private HorrorEventBase[] eventPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private Tilemap tilemap;

    public void SpawnRandomEvent(float exposure)
    {
        if (eventPrefabs.Length == 0) return;

        int index = Random.Range(0, eventPrefabs.Length);

        Vector3 spawnPos = GetSpawnPosition();

        HorrorEventBase horrorEvent = Instantiate(eventPrefabs[index], spawnPos, Quaternion.identity);

        // FakeChaserなら初期化
        //FakeChaserHorrorEvent fakechaser = obj.GetComponent<FakeChaserHorrorEvent>();
        if (horrorEvent is ISetUp) //horrorEvent変数にISetUPがついていますか(nullチェック込み)
        {
            horrorEvent.SetUp(player, tilemap);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float y = Random.Range(-height / 2f, height / 2f);
        Vector3 camPos = cam.transform.position;

        Vector3 rawPos = new Vector3(camPos.x - width / 2f - 2f, camPos.y + y, 0);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(rawPos, out hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return rawPos; // fallback
    }
}
