using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float duration = 3f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 60 * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerUpManager manager = other.GetComponent<PowerUpManager>();
            if (manager != null)
            {
                manager.ActivateShield(duration);
            }
            Destroy(gameObject);
        }
    }
}
