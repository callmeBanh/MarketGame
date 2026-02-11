using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        loadingController.LoadScene("GamePlay");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
