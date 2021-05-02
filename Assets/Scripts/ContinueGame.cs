using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    public Button continueGame;
    public Button quitToMenu;
    public Text pausedText;

    public void ContinuePlay()
    {
        pausedText.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(false);
        quitToMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
