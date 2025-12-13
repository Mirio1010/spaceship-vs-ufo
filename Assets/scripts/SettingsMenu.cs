using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void SetDifficulty(float value)
    {
        GameSettings.Difficulty = value;
    }
}
