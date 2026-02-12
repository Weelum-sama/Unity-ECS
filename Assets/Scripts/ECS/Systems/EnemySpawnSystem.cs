using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct EnemySpawnSystem : ISystem 
{
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<PlayerTag>();
    }
    public void OnUpdate(ref SystemState state) {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;

        foreach (var (spawnState, spawnData) in SystemAPI.Query<RefRW<EnemySpawnState>, EnemySpawnData>()) {
            spawnState.ValueRW.SpawnTimer -= deltaTime;
            if (spawnState.ValueRW.SpawnTimer > 0f) continue;
            spawnState.ValueRW.SpawnTimer = spawnData.SpawnInterval;

            var newEnemy = ecb.Instantiate(spawnData.EnemyPrefab);
            var spawnAngle = spawnState.ValueRW.Random.NextFloat(0f, math.TAU);
            var spawnPoint = new float3 {
                x = math.sin(spawnAngle),
                y = math.cos(spawnAngle),
                z = 0f,
            };
            spawnPoint *= spawnData.SpawnDistance;
            spawnPoint += playerPosition;

            ecb.SetComponent(newEnemy, LocalTransform.FromPosition(spawnPoint));
        }
    }
}