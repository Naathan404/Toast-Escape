using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [Header("Cutscene part")]

    [SerializeField] private List<GameObject> pics = new List<GameObject>();
    [SerializeField] private GameObject tapIcon;
    [SerializeField] private SceneController sceneController;
    private bool canInteract = false;
    private int picIdx = 0;

    private void Start()
    {
        UpdateCutscene();
        StartCoroutine(ResetInteract());
    }

    private void Update()
    {
        if (canInteract)
        {
            tapIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (picIdx != pics.Count - 1)
                {
                    AudioManager.instance.PlaySFX(AudioManager.instance.shopTabChange);
                    picIdx++;
                    tapIcon.SetActive(false);
                    UpdateCutscene();
                    StartCoroutine(ResetInteract());
                }
                else
                {
                    tapIcon.SetActive(false);
                    canInteract = false;
                    sceneController.LoadSceneWithAsync("1_InGame");
                }
            }
        }
    }

    IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }

    private void UpdateCutscene()
    {
        for (int i = 0; i < pics.Count; i++)
        {
            if (i != picIdx)
                pics[i].SetActive(false);
            else
                pics[i].SetActive(true);
        }
    }
}
