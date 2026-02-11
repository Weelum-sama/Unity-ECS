using Unity.Entities;
using Unity.Transforms;

public partial struct MoveDuplicationBeamSystem : ISystem 
{
    public void OnUpdate(ref SystemState state) {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (transform, data) in SystemAPI.Query<RefRW<LocalTransform>, DuplicationBeamData>()) {
            transform.ValueRW.Position += transform.ValueRO.Right() * data.MovingSpeed * deltaTime;
        }
    }
}