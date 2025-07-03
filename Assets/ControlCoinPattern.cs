using UnityEngine;

public class ControlCoinPattern : MonoBehaviour
{
    private void OnEnable()
    {
        CoinPatternResetter resetter = GetComponent<CoinPatternResetter>();
        resetter.ResetPattern();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Coin"))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
