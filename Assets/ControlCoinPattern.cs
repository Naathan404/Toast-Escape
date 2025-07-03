using UnityEngine;

public class ControlCoinPattern : MonoBehaviour
{
    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Coin"))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
