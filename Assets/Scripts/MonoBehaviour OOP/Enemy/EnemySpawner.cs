using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public static event Action<EnemySpawner> OnEnemySpawnEvent;

    [HideInInspector]
    public List<GameObject> Enemies = new List<GameObject>();

    public int TotalEnemies { private set; get; }

    [SerializeField]
    private GameObject _enemyPrefab;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
    }

    private void Start() {
        SpawnEnemy(PlayerSingleton.Instance.transform.position);
    }

    private void Update() {
        
    }

    private void UpdateList(GameObject enemy) {
        Enemies.Add(enemy);
        TotalEnemies++;
        if (OnEnemySpawnEvent != null) {
            OnEnemySpawnEvent(this);
        }
    }

    public void SpawnEnemy(Vector3 position) {
        var newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity, transform);
        UpdateList(newEnemy);
    }
    public void SpawnEnemy(GameObject enemy) {
        var newEnemy = Instantiate(enemy, transform);
        UpdateList(newEnemy);
    }
}
