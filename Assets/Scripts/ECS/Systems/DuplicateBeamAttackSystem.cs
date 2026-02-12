using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
[UpdateBefore(typeof(AfterPhysicsSystemGroup))]
public partial struct DuplicateBeamAttackSystem : ISystem {
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        var attackJob = new DuplicateBeamAttackJob {
            DuplicationBeamLookup = SystemAPI.GetComponentLookup<DuplicateBeamData>(true),
            EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(true),
            EntitiesToDestroyLookup = SystemAPI.GetComponentLookup<MarkedForDestructionTag>(),
        };

        var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
        state.Dependency = attackJob.Schedule(simulationSingleton, state.Dependency);
    }
}