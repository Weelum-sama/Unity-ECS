using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(CharacterAuthoring))]
public class PlayerAuthoring : MonoBehaviour 
{
    private class Baker : Baker<PlayerAuthoring> {
        public override void Bake(PlayerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<InitializeCameraTargetTag>(entity);
            AddComponent<CameraTarget>(entity);
        }
    }

}