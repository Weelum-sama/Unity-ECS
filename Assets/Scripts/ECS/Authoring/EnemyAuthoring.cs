using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(CharacterAuthoring))]
public class EnemyAuthoring : MonoBehaviour
{
    private class Baker : Baker<EnemyAuthoring> {
        public override void Bake(EnemyAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<EnemyTag>(entity);
        }
    }
}
