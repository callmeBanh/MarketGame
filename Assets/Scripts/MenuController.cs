using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton; 

    void Start()
    {
        // Kiểm tra nút Chơi tiếp ở màn Win: Nếu là màn 3 (Index 2) thì khóa nút
        if (nextLevelButton != null && GameManager.currentLevelIndex >= 2)
        {
            nextLevelButton.interactable = false;
        }
    }

    // --- HÀM MỚI CHO SCENE STARTGAME ---

    // Gán hàm này vào nút "CHƠI" ở màn hình bắt đầu
    public void StartGame()
    {
        Time.timeScale = 1;
        GameManager.currentLevelIndex = 0; // Luôn bắt đầu từ màn 1 khi nhấn Chơi mới
        // Gọi màn hình Loading trước khi vào GamePlay
        loadingController.LoadScene("GamePlay"); 
    }

    // Gán hàm này vào nút "THOÁT"
    public void QuitGame()
    {
        Debug.Log("Đang thoát game...");
        #if UNITY_EDITOR
            // Nếu đang chạy trong Unity Editor thì dừng Play mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Nếu là bản build thực tế thì thoát ứng dụng
            Application.Quit();
        #endif
    }

    // --- CÁC HÀM ĐIỀU KHIỂN CHUYỂN CẢNH KHÁC ---

    public void NextLevel()
    {
        Time.timeScale = 1;
        // Chỉ tăng Index khi người dùng nhấn "Chơi tiếp"
        GameManager.currentLevelIndex++; 
        if (GameManager.currentLevelIndex > 2) GameManager.currentLevelIndex = 0;
        loadingController.LoadScene("GamePlay");
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        // Load lại đúng màn hiện tại vì currentLevelIndex không đổi
        loadingController.LoadScene("GamePlay");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.currentLevelIndex = 0; 
        loadingController.LoadScene("StartGame");
    }
}