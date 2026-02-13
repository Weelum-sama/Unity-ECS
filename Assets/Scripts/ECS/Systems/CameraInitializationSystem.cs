using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct CameraInitializationSystem : ISystem 
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<InitializeCameraTargetTag>();
    }

    public void OnUpdate(ref SystemState state) { 
        if (CameraTargetSingleton.Instance == null) return;

        var cameraTargetTransform = CameraTargetSingleton.Instance.transform;

        foreach (var (cameraTarget, initializationTag) in SystemAPI.Query<RefRW<CameraTarget>, EnabledRefRW<InitializeCameraTargetTag>>().WithAll<InitializeCameraTargetTag, PlayerTag>()) {
            cameraTarget.ValueRW.CameraTransform = cameraTargetTransform;
            initializationTag.ValueRW = false;
        }
    }
}