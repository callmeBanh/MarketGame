using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cấu hình Màn chơi")]
    public List<GameObject> levelPrefabs; 
    public static int currentLevelIndex = 0; 
    private GameObject spawnedLevel;

    [Header("Cấu hình Panel hướng dẫn")]
    [SerializeField] private GameObject tutorialPanel; // Kéo TutorialPopup vào đây

    [Header("Tiến độ")]
    public int totalGroups = 3; 
    private int completedGroups = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Đóng băng game ngay khi Awake để hiện Panel
        if (tutorialPanel != null && tutorialPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        if (currentLevelIndex >= levelPrefabs.Count) currentLevelIndex = 0;
        LoadLevel(currentLevelIndex);
    }

    // CHUYỂN HÀM TỪ MENUCONTROLLER SANG ĐÂY
    public void StartGameAfterTutorial()
    {
        Time.timeScale = 1; // Kích hoạt lại vật lý
        
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false); // Ẩn Panel hướng dẫn
        }

        if (TimeManager.instance != null)
        {
            TimeManager.instance.StartTimer(30f); // Bắt đầu đếm ngược
        }
    }

    public void LoadLevel(int index)
    {
        if (spawnedLevel != null) Destroy(spawnedLevel);
        if (index < levelPrefabs.Count)
        {
            spawnedLevel = Instantiate(levelPrefabs[index], Vector3.zero, Quaternion.identity);
            completedGroups = 0;
        }
    }

    public void AddCompletedGroup()
    {
        completedGroups++;
        if (completedGroups >= totalGroups)
        {
            if (TimeManager.instance != null) TimeManager.instance.StopTimer();
            HandleLevelCompletion();
        }
    }

    private void HandleLevelCompletion()
    {
        currentLevelIndex++;
        if(currentLevelIndex < levelPrefabs.Count) LoadLevel(currentLevelIndex);
        else Invoke("GoToWinScene", 1.0f);
    }

    private void GoToWinScene()
    {
        loadingController.LoadScene("Win");
    }
}