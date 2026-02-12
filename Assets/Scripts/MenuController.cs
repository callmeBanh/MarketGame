using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        loadingController.LoadScene("GamePlay");
    }
    public void BackToMainMenu()
    {
        
        Time.timeScale = 1; 
        
        loadingController.LoadScene("StartGame");
    }
    public void TryAgain()
    {
        Time.timeScale = 1;
        loadingController.LoadScene("GamePlay");
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.SetInt("Level", currentLevel + 1);
        loadingController.LoadScene("GamePlay");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
