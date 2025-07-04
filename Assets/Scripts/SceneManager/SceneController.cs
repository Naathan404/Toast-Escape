using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Slider loadingSlider;

    public void OnLoadSceneButtonClick(string sceneName)
    {
        if (sceneName == "1_InGame")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.accept);
            if (PlayerPrefs.GetInt("HadPlayed", 0) == 0)
            {
                PlayerPrefs.SetInt("HadPlayed", 1);
                sceneName = "0_1_Cutscene";
            }
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.clickButton);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadGameAsync(sceneName));
    }

    public void LoadSceneWithAsync(string sceneName)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.clickButton);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadGameAsync(sceneName));
    }

    public void OnQuitButtonClick()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.clickButton);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    // Load async
    IEnumerator LoadGameAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float currentProgressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = currentProgressValue;
            yield return null;
        }
    }
}
