using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

partial struct CharacterMoveSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach(var (velocity, direction, speed) in SystemAPI.Query<RefRW<PhysicsVelocity>, MovingDirection, MoveSpeed>()) {
            var moveStep = direction.Value * speed.Value;
            velocity.ValueRW.Linear = new float3(moveStep, 0f);
        }
        
    }
}
