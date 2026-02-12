using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cấu hình trò chơi")]
    public GameObject fruitGroups; // Object chứa tất cả các Group quả
    public int totalGroups = 3;    // Táo, Lê, Cam
    private int completedGroups = 0;

    private void Awake()
    {
        // Thiết lập Singleton
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
        // 1. Hiện trái cây ngay lập tức khi vào game
        if (fruitGroups != null) 
        {
            fruitGroups.SetActive(true);
        }

        // 2. Thông báo bắt đầu game
        Debug.Log("Trò chơi đã bắt đầu!");
    }

    public void AddCompletedGroup()
    {
        completedGroups++;
        Debug.Log($"Đã hoàn thành: {completedGroups}/{totalGroups}");

        if (completedGroups >= totalGroups)
        {
            // THẮNG CUỘC:
            // Dừng đồng hồ đếm ngược
            if (TimeManager.instance != null) 
            {
                TimeManager.instance.StopTimer();
            }

            // Chờ 1 giây rồi chuyển sang màn hình Win
            Invoke("GoToWinScene", 1.0f);
        }
    }

    private void GoToWinScene()
    {
        Debug.Log("Chuyển sang màn hình chiến thắng!");
        loadingController.LoadScene("Win");
    }
}