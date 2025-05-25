using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUIPanel;

    public static bool isGameOver = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(isGameOver)
        {
            Time.timeScale = 0;
            gameOverUIPanel.SetActive(true);
        }
    }
}
