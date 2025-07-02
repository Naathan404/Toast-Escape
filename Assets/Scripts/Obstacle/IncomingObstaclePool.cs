using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingObstaclePool : MonoBehaviour
{
    public static IncomingObstaclePool instance;

    [SerializeField] private GameObject incomingObstaclePrefab;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private Transform incomingObstaclePoolParent;
    private List<GameObject> incomingObstaclePool = new List<GameObject>();
    private List<GameObject> batPool = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        for (int i = 0; i < 5; i++)
        {
            GameObject obstacle = Instantiate(incomingObstaclePrefab, incomingObstaclePoolParent);
            obstacle.SetActive(false);
            incomingObstaclePool.Add(obstacle);

            GameObject bat = Instantiate(batPrefab, incomingObstaclePoolParent);
            bat.SetActive(false);
            batPool.Add(bat);
        }
    }

    public GameObject GetObstacle()
    {
        foreach (GameObject obs in incomingObstaclePool)
        {
            if (!obs.activeInHierarchy)
                return obs;
        }
        GameObject newObs = Instantiate(incomingObstaclePrefab, incomingObstaclePoolParent);
        newObs.SetActive(false);
        incomingObstaclePool.Add(newObs);
        return newObs;
    }

    public GameObject GetBat()
    {
        foreach (GameObject bat in batPool)
        {
            if (!bat.activeInHierarchy)
                return bat;
        }
        GameObject newBat = Instantiate(batPrefab, incomingObstaclePoolParent);
        newBat.SetActive(false);
        batPool.Add(newBat);
        return newBat;
    }

    public void ReturnPool(GameObject obs, float time)
    {
        StartCoroutine(DeactivateEffectAfter(obs, time));
    }

    IEnumerator DeactivateEffectAfter(GameObject obs, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        obs.SetActive(false);
    }
}
