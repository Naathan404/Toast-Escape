using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class ToastCollisionHandler : MonoBehaviour
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
            // Deactive collider
            collision.gameObject.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.hitCoinSFX);
            GameManager.instance.IncreaseCoin();

            // Call effect
            GameObject effect = EffectManager.instance.GetCoinEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.2f);   
        }

        if (collision.gameObject.CompareTag("Bait"))
        {
            // Deactive collider
            collision.gameObject.SetActive(false);

            CatGroupManager.instance.AddNewCat();
            AudioManager.instance.PlaySFX(AudioManager.instance.hitScoreSFX);
            
            // Call effect
            GameObject effect = EffectManager.instance.GetSlimeEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.3f);
        }


        // Nhat duoc power up nam cham
        if (collision.CompareTag("PowerUp/Magnet"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);
            // Deactive collider
            collision.gameObject.SetActive(false);

            CoinMagnet magnet = GameObject.FindFirstObjectByType<CoinMagnet>();
            magnet.Activate();

            // Call effect
            GameObject effect = EffectManager.instance.GetPowerUpActivateEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.3f);
        }

        // Nhat duoc power x2 coin thu thap
        if (collision.CompareTag("PowerUp/DoubleCoin"))
        {
            // Deactive collider
            collision.gameObject.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.powerUp);
            DoubleCoin doubleCoin = GameObject.FindFirstObjectByType<DoubleCoin>();
            doubleCoin.Activate();

            // Call effect
            GameObject effect = EffectManager.instance.GetPowerUpActivateEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.3f);
        }

        if (collision.gameObject.CompareTag("Bomb") && collision.gameObject.GetComponent<Bomb>())
        {
            // Deactive collider
            collision.gameObject.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.hitBombSFX);
            CatGroupManager.instance.RemoveCat(this.GetComponent<Toast>());

            // Call effect
            GameObject effect = EffectManager.instance.GetExplosionEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.3f);
        }

        if (collision.gameObject.CompareTag("Obstacle/Bat"))
        {
            // Deactive collider
            collision.gameObject.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.hitBombSFX);
            CatGroupManager.instance.RemoveCat(this.GetComponent<Toast>());

            // Call effect
            GameObject effect = EffectManager.instance.GetExplosionEffect();
            effect.transform.position = collision.transform.position;
            effect.SetActive(true);
            EffectManager.instance.ReturnPool(effect, 0.3f);
        }
    }
}