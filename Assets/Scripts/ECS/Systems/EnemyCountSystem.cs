using Unity.Burst;
using Unity.Entities;

public partial struct EnemyCountSystem : ISystem 
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        foreach (var (proxy, enemyCount) in SystemAPI.Query<EnemyCountProxy, EnemyCount>()) {
            var totalEnemies = enemyCount.Value;
            var tracker = EnemyCountTrackerSingleton.Instance;
            tracker.totalEnemies = totalEnemies;
        }
    }
}