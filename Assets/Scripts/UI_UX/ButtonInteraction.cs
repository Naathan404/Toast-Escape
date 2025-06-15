using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteraction : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.clickButton);
        GameManager.instance.Restart();
        SceneManager.LoadScene("1_InGame");
    }

    public void OnMainMenuButtonClick()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.clickButton);
        GameManager.instance.Restart();
        SceneManager.LoadScene("0_MainMenu");
    }
}
