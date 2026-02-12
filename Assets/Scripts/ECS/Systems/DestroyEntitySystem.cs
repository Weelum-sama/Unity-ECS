using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct DestroyEntitySystem : ISystem {
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<MarkedForDestructionTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        var endECBSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var endECB = endECBSystem.CreateCommandBuffer(state.WorldUnmanaged);

        foreach(var (tag, entity) in SystemAPI.Query<MarkedForDestructionTag>().WithEntityAccess()) {
            endECB.DestroyEntity(entity);
        }
    }
}