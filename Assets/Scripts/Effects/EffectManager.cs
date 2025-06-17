using Unity.Mathematics;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [Header("Effects")]
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private GameObject hitSlimeEffectPrefab;
    [SerializeField] private GameObject hitCoinEffectPrefab;
    [SerializeField] private GameObject hitPowerUpEffectPrefab;
    [SerializeField] private GameObject destroyObstacleEffectPrefab;
    [SerializeField] private GameObject magnetDeactivateEffectPrefab;

    public void CallExplosionEffect(Vector3 pos)
    {
        if (explosionEffectPrefab)
        {
            GameObject exlosionEffect = Instantiate(explosionEffectPrefab, pos + new Vector3(0, 1f), Quaternion.identity);
            Destroy(exlosionEffect, 0.3f);
        }
    }
    public void CallHitSlimeEffect(Vector3 pos)
    {
        if (hitSlimeEffectPrefab)
        {
            GameObject hitSlimeEffect = Instantiate(hitSlimeEffectPrefab, pos, Quaternion.identity);
            Destroy(hitSlimeEffect, 0.3f);
        }
    }

    public void CallHitCoinEffect(Vector3 pos)
    {
        if (hitCoinEffectPrefab)
        {
            GameObject hitCoinEffect = Instantiate(hitCoinEffectPrefab, pos, Quaternion.identity);
            Destroy(hitCoinEffect, 0.2f);
        }
    }

    public void CallHitPowerUpEffect(Vector3 pos)
    {
        if (hitPowerUpEffectPrefab)
        {
            GameObject hitPowerUpEffect = Instantiate(hitPowerUpEffectPrefab, pos, Quaternion.identity);
            Destroy(hitPowerUpEffect, 0.3f);
        }
    }

    public void CallDestroyObstacleEffect(Vector3 pos)
    {
        if (destroyObstacleEffectPrefab)
        {
            GameObject destroyObstacleEffect = Instantiate(destroyObstacleEffectPrefab, pos, Quaternion.identity);
            Destroy(destroyObstacleEffect, 0.2f);
        }
    }

    public void CallMagnetDeactivateEffect(Vector3 pos)
    {
        if (magnetDeactivateEffectPrefab)
        {
            GameObject magnetEffect = Instantiate(magnetDeactivateEffectPrefab, pos, Quaternion.identity);
            Destroy(magnetEffect, 0.35f);
        }
    }
}
