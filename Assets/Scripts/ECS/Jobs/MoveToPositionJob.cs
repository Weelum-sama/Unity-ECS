using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[WithAll(typeof(EnemyTag))]
public partial struct MoveToPositionJob : IJobEntity
{
    public float2 PlayerPosition;

    public void Execute(ref MovingDirection direction, in LocalTransform transform)
    {
        var vectorToPlayer = PlayerPosition - transform.Position.xy;
        direction.Value = math.normalize(vectorToPlayer);
    }
}
