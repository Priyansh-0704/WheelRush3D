using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public bool isInvincible { get; private set; } = false;
    public bool isMagnetActive { get; private set; } = false;

    private float invincibleTimer;
    private float magnetTimer;

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0) isInvincible = false;
        }

        if (isMagnetActive)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer <= 0) isMagnetActive = false;
            else AttractCoins();
        }
    }

    public void ActivateShield(float duration)
    {
        isInvincible = true;
        invincibleTimer = duration;
        // Optionally enable visual effect
    }

    public void ActivateMagnet(float duration)
    {
        isMagnetActive = true;
        magnetTimer = duration;
        // Optionally enable visual effect
    }

    private void AttractCoins()
    {
        Collider[] coins = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider col in coins)
        {
            if (col.CompareTag("Coin"))
            {
                col.transform.position = Vector3.MoveTowards(col.transform.position, transform.position, 10f * Time.deltaTime);
            }
        }
    }

}
