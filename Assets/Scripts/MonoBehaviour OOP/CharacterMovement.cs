using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector2 MoveDirection;

    [SerializeField]
    private float _moveSpeed;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        transform.position += new Vector3(MoveDirection.x, MoveDirection.y, 0f) * _moveSpeed * Time.deltaTime;
    }
}
