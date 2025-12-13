using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject settingsPanel;

    public Slider volumeSlider;
    public Toggle muteToggle;

    float lastVolume = 1f; // remembers volume when you mute

    void Start()
    {
        // Initialize UI from current system volume
        float v = AudioListener.volume;

        // If volume is 0, treat as muted but keep slider at lastVolume
        if (v <= 0.0001f)
        {
            muteToggle.SetIsOnWithoutNotify(true);
            volumeSlider.SetValueWithoutNotify(lastVolume);
        }
        else
        {
            lastVolume = v;
            muteToggle.SetIsOnWithoutNotify(false);
            volumeSlider.SetValueWithoutNotify(v);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowInstructions() => instructionsPanel.SetActive(true);
    public void HideInstructions() => instructionsPanel.SetActive(false);

    public void ShowSettings() => settingsPanel.SetActive(true);
    public void HideSettings() => settingsPanel.SetActive(false);

    // Hook this to: VolumeSlider -> On Value Changed (float)
    public void SetVolume(float value)
    {
        // If muted, ignore slider changes (or optionally unmute)
        if (muteToggle.isOn)
            return;

        lastVolume = Mathf.Max(value, 0.0001f);
        AudioListener.volume = lastVolume;
    }

    // Hook this to: MuteToggle -> On Value Changed (bool)
     public void ToggleMute(bool isMuted)
    {
        AudioListener.pause = isMuted;
    }


    
public GameObject highScoresPanel;

public void OpenHighScores()
{
    
    highScoresPanel.SetActive(true);
}

public void CloseHighScores()
{
    highScoresPanel.SetActive(false);
 
}

}
