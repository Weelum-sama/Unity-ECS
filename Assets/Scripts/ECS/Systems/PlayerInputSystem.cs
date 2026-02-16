using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class PlayerInputSystem : SystemBase 
{
    private Input _input;

    protected override void OnCreate() {
        _input = new Input();
        _input.Enable();
    }
    protected override void OnUpdate() {
        var currentInput = (float2)_input.Player.Move.ReadValue<Vector2>();
        
        currentInput = new Vector2(0.71f, -0.71f); // For testing purposes

        foreach (var direction in SystemAPI.Query<RefRW<MovingDirection>>().WithAll<PlayerTag>()) {
            direction.ValueRW.Value = currentInput;
        }
    }
}