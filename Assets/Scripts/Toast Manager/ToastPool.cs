using UnityEngine;
using System.Collections.Generic;

public class ToastPool : MonoBehaviour
{
    public static ToastPool instance;

    [SerializeField] private GameObject toastPrefab;
    [SerializeField] private Transform toastPoolParent;
    private List<GameObject> toastPool = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        // Toa pool
        for (int i = 0; i < 20; i++)
        {
            GameObject toast = Instantiate(toastPrefab, toastPoolParent);
            if (toast.GetComponent<ToastCollisionHandler>() == null)
                toast.AddComponent<ToastCollisionHandler>();
            toast.SetActive(false);
            toastPool.Add(toast);
        }
    }

    public GameObject GetToast()
    {
        foreach (GameObject toast in toastPool)
        {
            if (!toast.activeInHierarchy)
                return toast;
        }
        GameObject newToast = Instantiate(toastPrefab, toastPoolParent);
        newToast.SetActive(false);
        toastPool.Add(toastPrefab);
        return newToast;
    }
    
}