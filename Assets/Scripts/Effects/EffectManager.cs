using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [SerializeField] private int poolSize = 5;

    [Header("Effect Prefabs")]
    [SerializeField] private GameObject hitSlimeEffectPrefab;
    [SerializeField] private GameObject hitCoinEffectPrefab;
    [SerializeField] private GameObject hitPowerUpEffectPrefab;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private GameObject destroyObstacleEffectPrefab;
    [SerializeField] private GameObject powerUpDeactivateEffectPrefab;

    [Header("Parent Effects")]
    [SerializeField] private Transform slimeParent;
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform powerUpParent;
    [SerializeField] private Transform explosionParent;
    [SerializeField] private Transform obstacleDesroyedParent;

    /// Effects Pool
    private List<GameObject> slimeEffectPool = new List<GameObject>();
    private List<GameObject> coinEffectPool = new List<GameObject>();
    private List<GameObject> powerUpActivateEffectPool = new List<GameObject>();
    private List<GameObject> powerUpDeactivateEffectPool = new List<GameObject>();
    private List<GameObject> explosionEffectPool = new List<GameObject>();
    private List<GameObject> obstacleDestroyedEffectPool = new List<GameObject>();

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        /// Create pools
        for (int i = 0; i < poolSize; i++)
        {
            // Slime
            GameObject slimeEffect = Instantiate(hitSlimeEffectPrefab, slimeParent);
            slimeEffect.SetActive(false);
            slimeEffectPool.Add(slimeEffect);

            // Coin
            GameObject coinEffect = Instantiate(hitCoinEffectPrefab, coinParent);
            coinEffect.SetActive(false);
            coinEffectPool.Add(coinEffect);

            // Powerup activate
            GameObject powerUpActivateEffect = Instantiate(hitPowerUpEffectPrefab, powerUpParent);
            powerUpActivateEffect.SetActive(false);
            powerUpActivateEffectPool.Add(powerUpActivateEffect);

            // Power up deactivate
            GameObject powerUpDeactivateEffect = Instantiate(powerUpDeactivateEffectPrefab, powerUpParent);
            powerUpDeactivateEffect.SetActive(false);
            powerUpDeactivateEffectPool.Add(powerUpDeactivateEffect);

            // Explosion
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, explosionParent);
            explosionEffect.SetActive(false);
            explosionEffectPool.Add(explosionEffect);

            // Obstacle destroyed
            GameObject obstacleDestroyedEffect = Instantiate(destroyObstacleEffectPrefab, obstacleDesroyedParent);
            obstacleDestroyedEffect.SetActive(false);
            obstacleDestroyedEffectPool.Add(obstacleDestroyedEffect);
        }
    }

    public GameObject GetCoinEffect()
    {
        foreach (GameObject effect in coinEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(hitCoinEffectPrefab);
        newEffect.SetActive(false);
        coinEffectPool.Add(newEffect);
        return newEffect;
    }

    public GameObject GetSlimeEffect()
    {
        foreach (GameObject effect in slimeEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(hitSlimeEffectPrefab);
        newEffect.SetActive(false);
        slimeEffectPool.Add(newEffect);
        return newEffect;
    }

    public GameObject GetPowerUpActivateEffect()
    {
        foreach (GameObject effect in powerUpActivateEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(hitPowerUpEffectPrefab);
        newEffect.SetActive(false);
        powerUpActivateEffectPool.Add(newEffect);
        return newEffect;
    }

    public GameObject GetPowerUpDeactivateEffect()
    {
        foreach (GameObject effect in powerUpDeactivateEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(powerUpDeactivateEffectPrefab);
        newEffect.SetActive(false);
        powerUpDeactivateEffectPool.Add(newEffect);
        return newEffect;
    }

    public GameObject GetExplosionEffect()
    {
        foreach (GameObject effect in explosionEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(explosionEffectPrefab);
        newEffect.SetActive(false);
        explosionEffectPool.Add(newEffect);
        return newEffect;
    }

    public GameObject GetObstacleDestroyedEffect()
    {
        foreach (GameObject effect in obstacleDestroyedEffectPool)
        {
            if (!effect.activeInHierarchy)
                return effect;
        }
        GameObject newEffect = Instantiate(destroyObstacleEffectPrefab);
        newEffect.SetActive(false);
        obstacleDestroyedEffectPool.Add(newEffect);
        return newEffect;
    }

    public void ReturnPool(GameObject effect, float time)
    {
        StartCoroutine(DeactivateEffectAfter(effect, time));
    }

    IEnumerator DeactivateEffectAfter(GameObject effect, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        effect.SetActive(false);
    }
}
