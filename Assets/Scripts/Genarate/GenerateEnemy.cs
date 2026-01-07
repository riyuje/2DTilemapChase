using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateEnemy : MonoBehaviour
{
    [SerializeField]
    private Chaser enemyPrefab;

    [SerializeField]
    private Transform generateEnemyTran;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Tilemap tilemap;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Chaser enemy = Instantiate(enemyPrefab, generateEnemyTran);

        enemy.Setup(target, tilemap);
    }

    
    void Update()
    {
        
    }
}
