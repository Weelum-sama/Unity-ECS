using UnityEngine;

public class DuplicateBeam : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private void Update() {
        transform.position += transform.right * _moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        EnemySpawner.Instance.SpawnEnemy(other.gameObject);
        Destroy(gameObject);
    }
}
