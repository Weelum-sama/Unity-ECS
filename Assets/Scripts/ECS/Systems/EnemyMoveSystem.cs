using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

partial struct EnemyDirectionSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy;

        var moveToPlayerJob = new MoveToPositionJob {
            PlayerPosition = playerPosition,
        };

        state.Dependency = moveToPlayerJob.ScheduleParallel(state.Dependency);
    }
}
