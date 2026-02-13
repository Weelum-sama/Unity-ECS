using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

public struct DuplicateBeamAttackJob : ITriggerEventsJob 
{
    [ReadOnly] public ComponentLookup<DuplicateBeamData> DuplicationBeamLookup;
    [ReadOnly] public ComponentLookup<EnemyTag> EnemyLookup;
    public ComponentLookup<MarkedForDestructionTag> EntitiesToDestroyLookup;

    [WriteOnly]
    public NativeList<Entity> EntitiesToDuplicate;
    
    public void Execute(TriggerEvent triggerEvent) {
        Entity duplicateBeamEntity;
        Entity enemyEntity;

        if (DuplicationBeamLookup.HasComponent(triggerEvent.EntityA) && EnemyLookup.HasComponent(triggerEvent.EntityB)) {
            duplicateBeamEntity = triggerEvent.EntityA;
            enemyEntity = triggerEvent.EntityB;
        }
        else if (DuplicationBeamLookup.HasComponent(triggerEvent.EntityB) && EnemyLookup.HasComponent(triggerEvent.EntityA)) {
            duplicateBeamEntity = triggerEvent.EntityB;
            enemyEntity = triggerEvent.EntityA;
        } 
        else {
            return;
        }

        EntitiesToDuplicate.Add(enemyEntity);

        EntitiesToDestroyLookup.SetComponentEnabled(duplicateBeamEntity, true);
    }
}