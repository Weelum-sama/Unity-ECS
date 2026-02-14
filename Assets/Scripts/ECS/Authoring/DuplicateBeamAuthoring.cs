using Unity.Entities;
using UnityEngine;

public class DuplicateBeamAuthoring : MonoBehaviour
{
    public float MovingSpeed;
    public int duplicationAmount;

    private class Baker : Baker<DuplicateBeamAuthoring> {
        public override void Bake(DuplicateBeamAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DuplicateBeamData {
                MovingSpeed = authoring.MovingSpeed,
            });
            AddComponent<MarkedForDestructionTag>(entity);
            SetComponentEnabled<MarkedForDestructionTag>(entity, false);
        }
    }
}
