using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class IncomingObstacle : MonoBehaviour
{
    [SerializeField] private GameObject warningSignPrefab;
    [SerializeField] private float warningDuration = 2f;
    [SerializeField] private GameObject batSpawnPoint;

    private void OnEnable()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.alert);
        warningSignPrefab.SetActive(true);
        StartCoroutine(StartWithWarning(warningDuration));
    }

    IEnumerator StartWithWarning(float duration)
    {
        yield return new WaitForSeconds(duration);
        warningSignPrefab.SetActive(false);
        GameObject bat = IncomingObstaclePool.instance.GetBat();
        bat.transform.position = batSpawnPoint.transform.position;
        bat.SetActive(true);
    }
}
