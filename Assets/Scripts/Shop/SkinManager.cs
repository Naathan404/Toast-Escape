using UnityEngine;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;
    private const string SKIN_ID = "SelectedSkinID";
    [SerializeField] private List<RuntimeAnimatorController> animatorControllers;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Da xoa mot object SkinManager");
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public RuntimeAnimatorController GetSkinByID(int skinID)
    {
        if (skinID >= animatorControllers.Count)
        {
            Debug.Log("Skin ID vuot qua pham vi cua List animatorControllers");
            return null;
        }
        return animatorControllers[skinID];
    }
}
