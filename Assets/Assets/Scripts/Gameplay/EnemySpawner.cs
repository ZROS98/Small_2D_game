using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField] private PlayerController CurrentCharacterController { get; set; }

    [field: SerializeField] private GameObject EnemyPrefab { get; set; }

    [field: SerializeField] private int EnemyCount = 1000;
    
    [field: SerializeField] private float SpawnXMin { get; set; } = -8f;
    [field: SerializeField] private float SpawnXMax { get; set; } = 8f;
    [field: SerializeField] private float SpawnYMin { get; set; } = -4f;
    [field: SerializeField] private float SpawnYMax { get; set; } = 4f;
    
    [HideInInspector] public EnemyController[] AllEnemies;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        AllEnemies = new EnemyController[EnemyCount];

        for (int i = 0; i < EnemyCount; i++)
        {
            Vector2 pos = GetRandomPosition();
            GameObject enemyObj = Instantiate(EnemyPrefab, pos, Quaternion.identity, transform);

            EnemyController controller = enemyObj.GetComponent<EnemyController>();
            controller.player = CurrentCharacterController.transform;

            AllEnemies[i] = controller;
        }

        CurrentCharacterController.EnemiesList = AllEnemies;
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(SpawnXMin, SpawnXMax);
        float y = Random.Range(SpawnYMin, SpawnYMax);
        return new Vector2(x, y);
    }
}