using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour 
{ 
    private CharacterMovement _characterMovement;
    private Input _input;

    private void Awake() {
        _characterMovement = GetComponent<CharacterMovement>();
        _input = new Input();
        _input.Enable();
    }

    private void Update() {
        var currentInput = _input.Player.Move.ReadValue<Vector2>();

        currentInput = new Vector2(0.71f, -0.71f); // For testing purposes

        _characterMovement.MoveDirection = currentInput;
    }
}