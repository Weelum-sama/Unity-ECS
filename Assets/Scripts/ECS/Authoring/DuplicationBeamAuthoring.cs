using Unity.Entities;
using UnityEngine;

public class DuplicationBeamAuthoring : MonoBehaviour
{
    public float MovingSpeed;
    public int duplicationAmount;

    private class Baker : Baker<DuplicationBeamAuthoring> {
        public override void Bake(DuplicationBeamAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DuplicationBeamData {
                MovingSpeed = authoring.MovingSpeed,
                duplicationAmount = authoring.duplicationAmount,
            });
        }
    }
}
