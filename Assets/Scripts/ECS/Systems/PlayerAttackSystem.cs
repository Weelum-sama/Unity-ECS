using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public partial struct PlayerAttackSystem : ISystem 
{
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<PhysicsWorldSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        var elapsedTime = SystemAPI.Time.ElapsedTime;

        var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
        var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

        foreach(var (expirationTimestamp, attackData, transform) in SystemAPI.Query<RefRW<PlayerCooldownExpirationTimestamp>, PlayerAttackData, LocalTransform>()) {
            if (expirationTimestamp.ValueRO.Value > elapsedTime) continue;

            var spawnPoint = transform.Position;
            var minPerceptionPosition = spawnPoint - attackData.PerceptionRadius;
            var maxPerceptionPosition = spawnPoint + attackData.PerceptionRadius;

            var AABB = new OverlapAabbInput {
                Aabb = new Aabb {
                    Min = minPerceptionPosition,
                    Max = maxPerceptionPosition,
                },
                Filter = attackData.CollisionFilter,
            };
            var hits = new NativeList<int>(state.WorldUpdateAllocator);
            if (!physicsWorldSingleton.OverlapAabb(AABB, ref hits)) continue;

            var maxDistanceSq = float.MaxValue;
            var closestEnemyPosition = float3.zero;
            foreach (var hit in hits) {
                var currentEnemyPosition = physicsWorldSingleton.Bodies[hit].WorldFromBody.pos;
                var distanceToPlayerSq = math.distancesq(spawnPoint.xy, currentEnemyPosition.xy);

                if(distanceToPlayerSq < maxDistanceSq) {
                    maxDistanceSq = distanceToPlayerSq;
                    closestEnemyPosition = currentEnemyPosition;
                }
            }

            var vectorToClosestEnemy = closestEnemyPosition - spawnPoint;
            var angleToClosestEnemy = math.atan2(vectorToClosestEnemy.y, vectorToClosestEnemy.x);
            var spawnOrientation = quaternion.Euler(0f, 0f, angleToClosestEnemy);

            var attack = ecb.Instantiate(attackData.AttackPrefab);
            var attackSize = attackData.PrefabSize;
            ecb.SetComponent(attack, LocalTransform.FromPositionRotationScale(spawnPoint, spawnOrientation, attackSize));

            expirationTimestamp.ValueRW.Value = elapsedTime + attackData.CooldownTime;
        }
    }
}