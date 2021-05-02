using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    public Button continueGame;
    public Button quitToMenu;
    public Text pausedText;
    public GameObject pausePanel;

    public void ContinuePlay()
    {
        pausePanel.gameObject.SetActive(false);
        pausedText.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        quitToMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
