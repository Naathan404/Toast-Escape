using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class IncomingObstacle : MonoBehaviour
{
    [SerializeField] private GameObject warningSignPrefab;
    [SerializeField] private float warningDuration = 2f;
    [SerializeField] private GameObject batSpawnPoint;

    private void Start()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.alert);
        if (warningSignPrefab != null)
            warningSignPrefab.SetActive(true);

        StartCoroutine(StartWithWarning(warningDuration));
    }

    private void OnEnable()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.alert);
        if (warningSignPrefab != null)
            warningSignPrefab.SetActive(true);

        StartCoroutine(StartWithWarning(warningDuration));
    }

    IEnumerator StartWithWarning(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (warningSignPrefab != null)
            warningSignPrefab.SetActive(false);
        if (IncomingObstaclePool.instance != null)
        {
            GameObject bat = IncomingObstaclePool.instance.GetBat();
            if (bat != null && batSpawnPoint != null)
            {
                bat.transform.position = batSpawnPoint.transform.position;
                bat.SetActive(true);
            }
        }
    }
}
