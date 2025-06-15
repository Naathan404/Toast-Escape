using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager instance;
    public HashSet<string> activeBoosts = new HashSet<string>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Kich hoat boost
    public void ActivateBoost(string boostName)
    {
        activeBoosts.Add(boostName);
    }

    // Kiem tra xem boost co duoc kich hoac hay chua
    public bool isBoostActivate(string boostName)
    {
        return activeBoosts.Contains(boostName);
    }

    // Reset tat cac cac boost
    public void ClearBoosts()
    {
        Debug.Log("Da reset tat ca cac boost");
        activeBoosts.Clear();
    }
}
