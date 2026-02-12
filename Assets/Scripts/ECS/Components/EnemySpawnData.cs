using Unity.Entities;

public struct EnemySpawnData : IComponentData 
{
    public Entity EnemyPrefab;
    public float SpawnInterval;
    public float SpawnDistance;
}