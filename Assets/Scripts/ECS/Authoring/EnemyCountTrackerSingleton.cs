using UnityEngine;

public class EnemyCountTrackerSingleton : MonoBehaviour
{
    public static EnemyCountTrackerSingleton Instance;
    public int totalEnemies;

    private EnemyCountDisplayer displayer;

    public void Awake() {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;

        displayer = GetComponent<EnemyCountDisplayer>();
    }

    public void Update() {
        displayer.CurrentEnemyCount = totalEnemies;
    }
}
