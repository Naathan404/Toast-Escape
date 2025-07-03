using System.Collections.Generic;
using UnityEngine;

public class CoinPatternResetter : MonoBehaviour
{
    private struct CoinData
    {
        public Transform tf;
        public Vector3 localPos;
    }

    private List<CoinData> coins = new List<CoinData>();

    void Awake()
    {
        // Tìm tất cả các object có tag "Coin" trong cây con
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.CompareTag("Coin"))
            {
                coins.Add(new CoinData
                {
                    tf = child,
                    localPos = child.localPosition,
                });
            }
        }
    }

    public void ResetPattern()
    {
        foreach (var coin in coins)
        {
            coin.tf.localPosition = coin.localPos;
        }
    }
}