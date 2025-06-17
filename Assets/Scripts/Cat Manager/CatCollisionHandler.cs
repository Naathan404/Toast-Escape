using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class CatCollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Bait"))
        // {
        //     AudioManager.instance.PlaySFX(AudioManager.instance.hitScoreSFX);
        //     EffectManager.instance.CallHitSlimeEffect(other.transform.position);
        //     Destroy(other.gameObject);
        //     CatGroupManager.instance.AddNewCat();
        // }

        // if (other.gameObject.CompareTag("Bomb") && other.gameObject.GetComponent<Bomb>())
        // {
        //     AudioManager.instance.PlaySFX(AudioManager.instance.hitBombSFX);
        //     CatGroupManager.instance.RemoveCat(this.GetComponent<Cat>());

        //     ///// Xu li animation no khi va cham voi bomb
        //     EffectManager.instance.CallExplosionEffect(other.transform.position);
        //     // Xoa bomb
        //     Destroy(other.gameObject);
        // }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Thu thap tien
        if (collision.CompareTag("Coin"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.hitCoinSFX);
            GameManager.instance.IncreaseCoin();
            EffectManager.instance.CallHitCoinEffect(collision.transform.position);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bait"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.hitScoreSFX);
            EffectManager.instance.CallHitSlimeEffect(collision.transform.position);
            Destroy(collision.gameObject);
            CatGroupManager.instance.AddNewCat();
        }


        // Nhat duoc power up nam cham
        if (collision.CompareTag("PowerUp/Magnet"))
        {
            CoinMagnet magnet = GameObject.FindFirstObjectByType<CoinMagnet>();
            magnet.Activate();
            // Goi am thanh va hieu ung
            EffectManager.instance.CallHitPowerUpEffect(collision.transform.position);
            AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);

            Destroy(collision.gameObject);
        }

        // Nhat duoc power x2 coin thu thap
        if (collision.CompareTag("PowerUp/DoubleCoin"))
        {
            DoubleCoin doubleCoin = GameObject.FindFirstObjectByType<DoubleCoin>();
            doubleCoin.Activate();
            // Goi am thanh va hieu ung
            EffectManager.instance.CallHitPowerUpEffect(collision.transform.position);
            AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bomb") && collision.gameObject.GetComponent<Bomb>())
        {
            Vector3 pos = collision.transform.position;
            AudioManager.instance.PlaySFX(AudioManager.instance.hitBombSFX);
            CatGroupManager.instance.RemoveCat(this.GetComponent<Cat>());
            // Xoa bomb
            Destroy(collision.gameObject);
            ///// Xu li animation no khi va cham voi bomb
            EffectManager.instance.CallExplosionEffect(pos);
        }

        if (collision.gameObject.CompareTag("Obstacle/Bat"))
        {
            Vector3 pos = collision.transform.position;
            CatGroupManager.instance.RemoveCat(this.GetComponent<Cat>());
            Destroy(collision.gameObject);
            Destroy(gameObject);
            EffectManager.instance.CallExplosionEffect(pos);
            AudioManager.instance.PlaySFX(AudioManager.instance.hitBombSFX);
        }
    }
    
}

