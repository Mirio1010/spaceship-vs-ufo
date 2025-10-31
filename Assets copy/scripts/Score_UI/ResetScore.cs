using UnityEngine;
public class GameStart : MonoBehaviour
{
    void Start() { if (ScoreManager.Instance) ScoreManager.Instance.ResetScore(); }
}
