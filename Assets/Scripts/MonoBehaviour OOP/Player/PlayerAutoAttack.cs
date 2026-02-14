using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackPrefab;
    [SerializeField]
    private float _cooldownTime, _perceptionRadius;

    private List<GameObject> _enemies;
    private float _expirationTimeStamp;

    private void OnEnable() {
        EnemySpawner.OnEnemySpawnEvent += UpdateEnemyList;
    }

    private void OnDisable() {
        EnemySpawner.OnEnemySpawnEvent -= UpdateEnemyList;
    }
    private void UpdateEnemyList(EnemySpawner enemySpawner) {
        _enemies = enemySpawner.Enemies;
    }

    private void Update() {
        var elapsedTime = Time.time;

        if (_expirationTimeStamp > elapsedTime) return;

        var enemyPosition = NearestEnemyPosition();
        if (enemyPosition == Vector3.zero) return;

        Attack(enemyPosition);
        _expirationTimeStamp = elapsedTime + _cooldownTime;
    }

    private void Attack(Vector3 targetPosition) {
        if (_attackPrefab == null) return;
        var vectorToTarget = targetPosition - transform.position;
        var angleToClosestEnemy = math.atan2(vectorToTarget.y, vectorToTarget.x);
        var attackOrientation = quaternion.Euler(0f, 0f, angleToClosestEnemy);
        Instantiate(_attackPrefab, transform.position, attackOrientation);
    }

    private Vector3 NearestEnemyPosition() {
        var maxDistanceSq = float.MaxValue;
        var closestEnemyPosition = Vector3.zero;
        foreach (var enemy in _enemies) {
            var enemyPosition = enemy.transform.position;
            var distanceToPlayerSq = math.distancesq(transform.position, enemyPosition);

            if (distanceToPlayerSq > math.square(_perceptionRadius)) continue;

            if (distanceToPlayerSq < maxDistanceSq) {
                maxDistanceSq = distanceToPlayerSq;
                closestEnemyPosition = enemyPosition;
            }
        }

        return closestEnemyPosition;
    }
}
