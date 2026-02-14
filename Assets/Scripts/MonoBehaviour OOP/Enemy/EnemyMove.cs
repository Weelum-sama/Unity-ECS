using UnityEngine;

[RequireComponent (typeof(CharacterMovement))]
public class EnemyMove : MonoBehaviour {
    private CharacterMovement _characterMovement;

    private void Awake() {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update() {
        if(PlayerSingleton.Instance == null) return;

        Vector3 directionToPlayer = PlayerSingleton.Instance.gameObject.transform.position - transform.position;
        _characterMovement.MoveDirection = directionToPlayer.normalized;
    }
}
