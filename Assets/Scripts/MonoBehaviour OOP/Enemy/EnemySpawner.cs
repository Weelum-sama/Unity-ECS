using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public static event Action<EnemySpawner> OnEnemySpawnEvent;

    [HideInInspector]
    public List<GameObject> Enemies = new List<GameObject>();

    public int TotalEnemies { private set; get; }

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private EnemyCountDisplayer _enemyCountDisplayer;

    [SerializeField]
    private float _spawnInterval, _spawnDistance;

    [SerializeField]
    private uint _randomSeed;

    private float _spawnTimer = 0f;
    private Random _random;

    private Vector3 _playerPosition;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
    }

    private void Start() {
        _random = Random.CreateFromIndex(_randomSeed);
    }

    private void Update() {
        _playerPosition = PlayerSingleton.Instance.transform.position;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer > 0f) return;
        _spawnTimer = _spawnInterval;

        var spawnAngle = _random.NextFloat(0f, math.TAU);
        var spawnPoint = new Vector3(
            math.sin(spawnAngle), math.cos(spawnAngle));

        spawnPoint *= _spawnDistance;
        spawnPoint += _playerPosition;
        SpawnEnemy(spawnPoint);
    }

    private void UpdateList(GameObject enemy) {
        Enemies.Add(enemy);
        TotalEnemies++;
        _enemyCountDisplayer._currentEnemyCount = TotalEnemies;
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
