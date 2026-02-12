using Unity.Entities;
using Unity.Mathematics;

public struct EnemySpawnState : IComponentData
{
    public float SpawnTimer;
    public Random Random;
}