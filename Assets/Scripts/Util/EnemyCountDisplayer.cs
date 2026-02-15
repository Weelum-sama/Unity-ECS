using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountDisplayer : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [HideInInspector]
    public int CurrentEnemyCount = 0;

    private void Update() {
        _text.text = CurrentEnemyCount.ToString();
    }
}
