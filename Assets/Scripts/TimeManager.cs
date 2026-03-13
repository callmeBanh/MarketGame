using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Cấu hình UI")]
    public TMP_Text timerText; 

    [Header("Cấu hình thời gian")]
    public float timeRemaining = 30f; 
    private bool isRunning = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Mặc định không chạy ngay, chờ MenuController kích hoạt
        isRunning = false; 
        UpdateTimerDisplay(timeRemaining);
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
                timeRemaining = 0;
                isRunning = false;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int seconds = Mathf.CeilToInt(timeToDisplay);
        timerText.text = string.Format("00:{0:00}", seconds);

        if (timeToDisplay <= 5f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.black;
        }
    }

    void GameOver()
    {
        Debug.Log("Hết giờ!");
        loadingController.LoadScene("Lose");
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer(float newTime)
    {
        timeRemaining = newTime;
        isRunning = true; // Bắt đầu đếm ngược thực sự
    }
}