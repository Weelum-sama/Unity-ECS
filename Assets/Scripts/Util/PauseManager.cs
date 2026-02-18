using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public bool TestMode = false;

    [SerializeField]
    private GameObject _fpsCounter, _pauseScreen;

    private Input _input;
    private bool _paused = true;

    private void Awake() {
        _input = new Input();
        _input.Enable();

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable() {
        _input.Player.Pause.started += Pause;
    }

    private void Pause(InputAction.CallbackContext context) {
        PauseGame();
    }

    public void PauseGame() {
        _paused = !_paused;
        _fpsCounter.SetActive(_paused);
        _pauseScreen.SetActive(!_paused);
        Time.timeScale =
        _paused ? 1f : 0f;
    }
}
