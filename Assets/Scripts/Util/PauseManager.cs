using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _fpsCounter, _pauseScreen;

    private Input _input;
    private bool _paused = true;

    private void Awake() {
        _input = new Input();
        _input.Enable();
    }

    private void OnEnable() {
        _input.Player.Pause.started += Pause;
    }

    private void Pause(InputAction.CallbackContext context) {
        _paused = !_paused;
        _fpsCounter.SetActive(_paused);
        _pauseScreen.SetActive(!_paused);
        Time.timeScale =
        _paused ? 1f : 0f;
    }
}
