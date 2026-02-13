using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountDisplayer : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [HideInInspector]
    public int _currentEnemyCount = 0;

    private void Update() {
        _text.text = _currentEnemyCount.ToString();
    }
}
