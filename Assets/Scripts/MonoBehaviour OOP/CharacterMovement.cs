using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public Vector2 MoveDirection { private get; set; }

    [SerializeField]
    private float _moveSpeed;

    private void Update() {
        transform.position += new Vector3(MoveDirection.x, MoveDirection.y) * _moveSpeed * Time.deltaTime;
    }
}
