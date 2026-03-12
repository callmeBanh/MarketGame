using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Cấu hình UI")]
    public TMP_Text timerText; 

    [Header("Cấu hình thời gian")]
    public float timeRemaining = 30f; // Bắt đầu từ 30 giây
    private bool isRunning = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Chờ GameManager báo bắt đầu (sau khi nhấn nút Đồng Ý) mới chạy
        // Hoặc cho chạy ngay nếu bạn muốn
        isRunning = true; 
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // HẾT GIỜ
                timeRemaining = 0;
                isRunning = false;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        // Làm tròn lên để bé không thấy số 0 trước khi thực sự hết giờ
        int seconds = Mathf.CeilToInt(timeToDisplay);
        timerText.text = string.Format("00:{0:00}", seconds);

        // Đổi màu đỏ khi còn dưới 5 giây để cảnh báo
        if (timeToDisplay <= 5f)
        {
            timerText.color = Color.red;
        }
    }

    void GameOver()
    {
        Debug.Log("Hết giờ! Chuyển sang màn hình Lose.");
        // Gọi loadingController để chuyển sang Scene Lose
        // Đảm bảo bạn đã thêm Scene "Lose" vào Build Settings
        loadingController.LoadScene("Lose");
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer(float newTime)
    {
        timeRemaining = newTime;
        isRunning = true;
    }
}