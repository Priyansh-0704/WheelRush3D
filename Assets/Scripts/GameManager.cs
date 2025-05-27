using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUIPanel;
    public GameObject startText;
    public TextMeshProUGUI coinNumber;
    public Image coinIcon;

    public static bool isGameOver;
    public static bool isTapped = false;

    public static int noOfCoins;

    void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        isTapped = false;
        noOfCoins = 0;

        coinNumber.gameObject.SetActive(true);
        coinIcon.gameObject.SetActive(true);
    }

    void Update()
    {
        if(isGameOver)
        {
            Time.timeScale = 0;
            gameOverUIPanel.SetActive(true);
            coinNumber.gameObject.SetActive(false);
            coinIcon.gameObject.SetActive(false);
        }

        if (SwipeManager.tap)
        {
            isTapped = true;
            if (startText != null)
                Destroy(startText);
        }

        coinNumber.text = noOfCoins.ToString();
    }
}
