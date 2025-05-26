using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUIPanel;
    public GameObject startText;

    public static bool isGameOver;
    public static bool isTapped = false;

    void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        isTapped = false;
    }

    void Update()
    {
        if(isGameOver)
        {
            Time.timeScale = 0;
            gameOverUIPanel.SetActive(true);
        }

        if (SwipeManager.tap)
        {
            isTapped = true;
            if (startText != null)
                Destroy(startText);
        }
    }
}
