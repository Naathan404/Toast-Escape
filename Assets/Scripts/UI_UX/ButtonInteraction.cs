using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteraction : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.accept);
        GameManager.instance.Restart();
        SceneManager.LoadScene("1_InGame");
    }

    public void OnPauseButtonClick()
    {
        GameManager.instance.PauseGame();
        Debug.Log("An nut pause");
        AudioManager.instance.PlaySFX(AudioManager.instance.cancle);
        AudioManager.instance.PauseMusic();
        InGameUIManager.instance.SetPlayingPanel(false);
        InGameUIManager.instance.SetGameOverPanel(false);
        InGameUIManager.instance.SetPausePanel(true);
        Time.timeScale = 0f;
    }

    public void OnContinueButtonClick()
    {
        GameManager.instance.ResumeGame();
        Debug.Log("An nut continue");
        AudioManager.instance.PlaySFX(AudioManager.instance.accept);
        AudioManager.instance.UnPauseMusic();
        InGameUIManager.instance.SetPlayingPanel(true);
        InGameUIManager.instance.SetGameOverPanel(false);
        InGameUIManager.instance.SetPausePanel(false);
        Time.timeScale = 1f;
    }
}
