using UnityEngine;
using UnityEngine.UI; // Cần thư viện này để điều khiển Button

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton; // Kéo nút "Chơi tiếp" vào đây trong Inspector

    void Start()
    {
        // Nếu đang ở màn hình Win, kiểm tra xem có phải màn cuối không
        if (nextLevelButton != null)
        {
            // Nếu GameManager đã hết màn chơi
            // Giả sử bạn có 3 màn, Index màn cuối là 2
            if (GameManager.currentLevelIndex >= 2) 
            {
                nextLevelButton.interactable = false; // Vô hiệu hóa nút
                // Hoặc ẩn luôn nút: nextLevelButton.gameObject.SetActive(false);
            }
        }
    }

public void NextLevel()
{
    Time.timeScale = 1;
    // CHỈ tăng Index khi thực sự bấm nút Chơi tiếp
    GameManager.currentLevelIndex++; 
    
    // Kiểm tra nếu vượt quá màn 3 (Index 2) thì quay về màn 1 (Index 0)
    if (GameManager.currentLevelIndex > 2) 
    {
        GameManager.currentLevelIndex = 0;
    }
    loadingController.LoadScene("GamePlay");
}

public void TryAgain()
{
    Time.timeScale = 1;
    // Giữ nguyên Index để nạp lại đúng màn vừa thua
    loadingController.LoadScene("GamePlay");
}

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.currentLevelIndex = 0; 
        loadingController.LoadScene("StartGame");
    }

    public void QuitGame()
    {
          Debug.Log("Quit Game Clicked");

    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }

    public void startGame()
    {
        Time.timeScale = 1;
        GameManager.currentLevelIndex = 0; 
        loadingController.LoadScene("GamePlay");
    }
}