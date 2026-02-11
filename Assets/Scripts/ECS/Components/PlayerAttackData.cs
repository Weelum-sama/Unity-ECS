using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

public struct PlayerAttackData : IComponentData
{
    public Entity AttackPrefab;
    public float PrefabSize;
    public float CooldownTime;
    public float3 PerceptionRadius;
    public CollisionFilter CollisionFilter;
}
