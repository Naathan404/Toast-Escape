using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    [Header("Chunk Lists")]
    [SerializeField] private List<GameObject> EasyPlatformList;
    [SerializeField] private List<GameObject> MediumPlatformList;
    [SerializeField] private List<GameObject> HardPlatformList;
    [SerializeField] private List<GameObject> ExtremePlatformList;
    [SerializeField] private List<GameObject> HellPlatformList;

    [Header("Pool's parent")]
    [SerializeField] private Transform EasyPlatformPoolParent;
    [SerializeField] private Transform MediumPlatformPoolParent;
    [SerializeField] private Transform HardPlatformPoolParent;
    [SerializeField] private Transform ExtremePlatformPoolParent;
    [SerializeField] private Transform HellPlatformPoolParent;

    private List<GameObject> EasyPlatformPool = new List<GameObject>();
    private List<GameObject> MediumPlatformPool = new List<GameObject>();
    private List<GameObject> HardPlatformPool = new List<GameObject>();
    private List<GameObject> ExtremePlatformPool = new List<GameObject>();
    private List<GameObject> HellPlatformPool = new List<GameObject>();

    public static PlatformPool instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
        }

        // Tao pool
        for (int i = 0; i < EasyPlatformList.Count; i++)
        {
            GameObject lv = Instantiate(EasyPlatformList[i], EasyPlatformPoolParent);
            lv.SetActive(false);
            EasyPlatformPool.Add(lv);
        }

        for (int i = 0; i < MediumPlatformList.Count; i++)
        {
            GameObject lv = Instantiate(MediumPlatformList[i], MediumPlatformPoolParent);
            lv.SetActive(false);
            MediumPlatformPool.Add(lv);
        }

        for (int i = 0; i < HardPlatformList.Count; i++)
        {
            GameObject lv = Instantiate(HardPlatformList[i], HardPlatformPoolParent);
            lv.SetActive(false);
            HardPlatformPool.Add(lv);
        }

        for (int i = 0; i < ExtremePlatformList.Count; i++)
        {
            GameObject lv = Instantiate(ExtremePlatformList[i], ExtremePlatformPoolParent);
            lv.SetActive(false);
            ExtremePlatformPool.Add(lv);
        }

        for (int i = 0; i < HellPlatformList.Count; i++)
        {
            GameObject lv = Instantiate(HellPlatformList[i], HellPlatformPoolParent);
            lv.SetActive(false);
            HellPlatformPool.Add(lv);
        }
    }

    // Lay platform
    public GameObject GetEasyPlatform()
    {
        GameObject plf;
        int i = 0;
        do
        {
            plf = EasyPlatformPool[Random.Range(0, EasyPlatformPool.Count)];
            if (!plf.activeInHierarchy) return plf;
            i++;
        } while (i < EasyPlatformPool.Count);
        GameObject newPlf = Instantiate(EasyPlatformList[Random.Range(0, EasyPlatformList.Count)], EasyPlatformPoolParent);
        newPlf.SetActive(false);
        EasyPlatformPool.Add(newPlf);
        return newPlf;
    }

    public GameObject GetMediumPlatform()
    {
        GameObject plf;
        int i = 0;
        do
        {
            plf = MediumPlatformPool[Random.Range(0, MediumPlatformPool.Count)];
            if (!plf.activeInHierarchy) return plf;
            i++;
        } while (i < MediumPlatformPool.Count);
        GameObject newPlf = Instantiate(MediumPlatformList[Random.Range(0, MediumPlatformList.Count)], MediumPlatformPoolParent);
        newPlf.SetActive(false);
        MediumPlatformPool.Add(newPlf);
        return newPlf;
    }

    public GameObject GetHardPlatform()
    {
        GameObject plf;
        int i = 0;
        do
        {
            plf = HardPlatformPool[Random.Range(0, HardPlatformPool.Count)];
            if (!plf.activeInHierarchy) return plf;
            i++;
        } while (i < HardPlatformPool.Count);
        GameObject newPlf = Instantiate(HardPlatformList[Random.Range(0, HardPlatformList.Count)], HardPlatformPoolParent);
        newPlf.SetActive(false);
        HardPlatformPool.Add(newPlf);
        return newPlf;
    }

    public GameObject GetExtremePlatform()
    {
        GameObject plf;
        int i = 0;
        do
        {
            plf = ExtremePlatformPool[Random.Range(0, ExtremePlatformPool.Count)];
            if (!plf.activeInHierarchy) return plf;
            i++;
        } while (i < ExtremePlatformPool.Count);
        GameObject newPlf = Instantiate(ExtremePlatformList[Random.Range(0, ExtremePlatformList.Count)], ExtremePlatformPoolParent);
        newPlf.SetActive(false);
        ExtremePlatformPool.Add(newPlf);
        return newPlf;
    }

    public GameObject GetHellPlatform()
    {
        GameObject plf;
        int i = 0;
        do
        {
            plf = HellPlatformPool[Random.Range(0, HellPlatformPool.Count)];
            if (!plf.activeInHierarchy) return plf;
            i++;
        } while (i < HellPlatformPool.Count);
        GameObject newPlf = Instantiate(HellPlatformList[Random.Range(0, HellPlatformList.Count)], HellPlatformPoolParent);
        newPlf.SetActive(false);
        HellPlatformPool.Add(newPlf);
        return newPlf;
    }
}
