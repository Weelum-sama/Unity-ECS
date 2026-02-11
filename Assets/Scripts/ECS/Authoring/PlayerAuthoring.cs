using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[RequireComponent(typeof(CharacterAuthoring))]
public class PlayerAuthoring : MonoBehaviour 
{
    public GameObject AttackPrefab;
    public float PrefabSize;
    public float CooldownTime;
    public float PercepcionRadius;

    private class Baker : Baker<PlayerAuthoring> {
        public override void Bake(PlayerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<InitializeCameraTargetTag>(entity);
            AddComponent<CameraTarget>(entity);

            var enemyLayer = LayerMask.NameToLayer("Enemy");
            var enemyLayerMask = (uint)math.pow(2, enemyLayer);

            var attackCollisionFilter = new CollisionFilter {
                BelongsTo = uint.MaxValue,
                CollidesWith = enemyLayerMask,
            };

            AddComponent(entity, new PlayerAttackData {
                AttackPrefab = GetEntity(authoring.AttackPrefab, TransformUsageFlags.Dynamic),
                PrefabSize = authoring.PrefabSize,
                CooldownTime = authoring.CooldownTime,
                PerceptionRadius = new float3(authoring.PercepcionRadius),
                CollisionFilter = attackCollisionFilter,
            });
            AddComponent<PlayerCooldownExpirationTimestamp>(entity);
        }
    }
}