using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUIPanel;
    public GameObject startText;
    public TextMeshProUGUI coinNumber;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI currentScoreUI;
    public Image coinIcon;
    public Image scoreIcon;

    public static bool isGameOver;
    public static bool isTapped = false;
    public static float distTravelled;
    public static int score;
    public static int noOfCoins;

    void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        isTapped = false;
        noOfCoins = 0;
        distTravelled = 0;

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
            scoreUI.text = score.ToString() + " m";
            currentScoreUI.gameObject.SetActive(false);
            scoreIcon.gameObject.SetActive(false);
        }

        if (SwipeManager.tap)
        {
            isTapped = true;
            if (startText != null)
                Destroy(startText);
        }

        if(!isGameOver && isTapped)
        {
            coinNumber.text = noOfCoins.ToString();
            distTravelled += 10 * Time.deltaTime;
            score = Mathf.FloorToInt(distTravelled);

            currentScoreUI.text = score.ToString() + " m";
        }
    }
}
