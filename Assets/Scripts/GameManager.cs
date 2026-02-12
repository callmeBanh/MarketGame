using UnityEngine;
using UnityEngine.UI; // Cần thiết để điều khiển Button

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cấu hình UI")]
    public GameObject startPopup; // Kéo StartPopup vào đây
    public Button agreeButton;    // Kéo Button Đồng Ý vào đây

    [Header("Cấu hình trò chơi")]
    public GameObject fruitGroups; // Kéo Object chứa tất cả các Group quả vào đây
    public int totalGroups = 3; 
    private int completedGroups = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Bước 1: Khi vào Scene, hiện Popup và ẩn các nhóm quả
        if (startPopup != null) startPopup.SetActive(true);
        if (fruitGroups != null) fruitGroups.SetActive(false);

        // Bước 2: Lắng nghe sự kiện nhấn nút
        if (agreeButton != null)
        {
            agreeButton.onClick.AddListener(StartGameplay);
        }
    }

    // Hàm này chạy khi nhấn nút Đồng Ý
    private void StartGameplay()
    {
        // Bước 3: Tắt Popup và hiện các nhóm quả để bắt đầu chơi
        if (startPopup != null) startPopup.SetActive(false);
        if (fruitGroups != null) fruitGroups.SetActive(true);
        
        Debug.Log("Trò chơi chính thức bắt đầu!");
    }

    public void AddCompletedGroup()
    {
        completedGroups++;
        Debug.Log("Đã hoàn thành: " + completedGroups + "/" + totalGroups);

        if (completedGroups >= totalGroups)
        {
            Invoke("GoToWinScene", 1.0f);
        }
    }

    private void GoToWinScene()
    {
        Debug.Log("Chuyển sang màn hình chiến thắng!");
        // Đảm bảo bạn đã có class loadingController trong project
        loadingController.LoadScene("Win");
    }
}