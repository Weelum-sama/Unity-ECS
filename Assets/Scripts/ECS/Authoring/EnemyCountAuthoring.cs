using Unity.Entities;
using UnityEngine;

public class EnemyCountAuthoring : MonoBehaviour
{
    private class Baker : Baker<EnemyCountAuthoring> {
        public override void Bake(EnemyCountAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<EnemyCountProxy>(entity);
            AddComponent(entity, new EnemyCount { Value = 0 });
        }
    }
}
