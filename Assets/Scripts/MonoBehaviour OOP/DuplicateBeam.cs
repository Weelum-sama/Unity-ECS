using UnityEngine;

public class DuplicateBeam : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed;

    private void Update() {
        transform.position += transform.right * _MoveSpeed * Time.deltaTime;
    }
}
