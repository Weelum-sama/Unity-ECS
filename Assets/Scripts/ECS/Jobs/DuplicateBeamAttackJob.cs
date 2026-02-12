using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public struct DuplicateBeamAttackJob : ITriggerEventsJob 
{
    [ReadOnly] public ComponentLookup<DuplicateBeamData> DuplicationBeamLookup;
    [ReadOnly] public ComponentLookup<EnemyTag> EnemyLookup;
    public ComponentLookup<MarkedForDestructionTag> EntitiesToDestroyLookup;
    
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

        // TODO : Duplicate enemy entity
        Debug.Log("Duplicating");

        EntitiesToDestroyLookup.SetComponentEnabled(duplicateBeamEntity, true);
    }
}