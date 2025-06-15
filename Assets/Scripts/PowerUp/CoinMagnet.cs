using UnityEngine;
using System.Collections.Generic;

public class CoinMagnet : BasePowerUp
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float magnetForce = 10f;

    private Transform targetPos;
    [SerializeField] private GameObject magnetSprite;

    private void Start()
    {
        targetPos = this.transform;
        magnetSprite.SetActive(false);
    }

    private void Update()
    {
        MoveCoin();
    }

    private void MoveCoin()
    {
        if (!isActive) return;

        Vector2 pos = targetPos.position;
        Collider2D[] coins = Physics2D.OverlapCircleAll(pos, radius);

        foreach (var coin in coins)
        {
            if (coin.CompareTag("Coin"))
            {
                coin.transform.position = Vector2.MoveTowards(
                    coin.transform.position,
                    pos,
                    magnetForce * Time.deltaTime);
            }
        }
    }

    protected override void OnActivate()
    {
        Debug.Log("Activate magnet");
        magnetSprite.SetActive(true);
    }

    protected override void OnDeactivate()
    {
        Debug.Log("Deactivate magnet");
        magnetSprite.SetActive(false);
    }
}
