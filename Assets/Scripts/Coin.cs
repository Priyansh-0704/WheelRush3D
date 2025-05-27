using System.Net.Sockets;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 60 * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioManager audioManager = Object.FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlaySound("Coin Pickup");
            }
            GameManager.noOfCoins++;
            Destroy(gameObject);
        }
    }
}
