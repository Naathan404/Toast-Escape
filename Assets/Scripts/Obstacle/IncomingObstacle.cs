using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class IncomingObstacle : MonoBehaviour
{
    [SerializeField] private GameObject warningSignPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float warningDuration = 2f;

    private void Start()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.alert);
        StartCoroutine(StartWithWarning(warningDuration));
    }

    IEnumerator StartWithWarning(float duration)
    {
        warningSignPrefab.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningSignPrefab.SetActive(false);
        obstaclePrefab.SetActive(true);
    }
}
