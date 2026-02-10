using Unity.Entities;
using UnityEngine;

public class CharacterAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private class Baker : Baker<CharacterAuthoring> {
        public override void Bake(CharacterAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<InitializeCharacterFlag>(entity);
            AddComponent<MovingDirection>(entity);
            AddComponent(entity, new MoveSpeed { Value = authoring._speed });
        }
    }
}
