using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
[UpdateBefore(typeof(AfterPhysicsSystemGroup))]
public partial struct DuplicateBeamAttackSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        NativeList<Entity> _entitiesToDuplicate = new NativeList<Entity>(Allocator.Persistent);

        var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

        var attackJob = new DuplicateBeamAttackJob {
            DuplicationBeamLookup = SystemAPI.GetComponentLookup<DuplicateBeamData>(true),
            EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(true),
            EntitiesToDestroyLookup = SystemAPI.GetComponentLookup<MarkedForDestructionTag>(),
            EntitiesToDuplicate = _entitiesToDuplicate,
        };

        var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
        attackJob.Schedule(simulationSingleton, state.Dependency).Complete();

        if (_entitiesToDuplicate.Length < 1) return;

        var enemyCountProxy = SystemAPI.GetSingletonEntity<EnemyCountProxy>();
        var enemyCount = SystemAPI.GetComponent<EnemyCount>(enemyCountProxy);
        var currentEnemiesCount = enemyCount.Value;

        var newEnemiesCount = currentEnemiesCount;

        for (int i = 0; i < _entitiesToDuplicate.Length; i++) {
            var newEntity = ecb.Instantiate(_entitiesToDuplicate[i]);
            newEnemiesCount += 1;
        }
        var newEnemyCount = new EnemyCount { Value = newEnemiesCount };
        ecb.SetComponent(enemyCountProxy, newEnemyCount);
    }
}