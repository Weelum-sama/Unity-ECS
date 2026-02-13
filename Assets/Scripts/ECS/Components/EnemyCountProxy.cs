using Unity.Entities;

public struct EnemyCountProxy : IComponentData {
    public UnityObjectRef<EnemyCountDisplayer> Displayer;
}