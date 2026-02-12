using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cấu hình Màn chơi")]
    public List<GameObject> levelPrefabs; 
    public static int currentLevelIndex = 0; // Chỉ số màn chơi hiện tại

    private GameObject spawnedLevel;

    [Header("Tiến độ")]
    public int totalGroups = 3; 
    private int completedGroups = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Đảm bảo không nạp quá số màn trong danh sách
        if (currentLevelIndex >= levelPrefabs.Count) currentLevelIndex = 0;
        
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (spawnedLevel != null) Destroy(spawnedLevel);

        if (index < levelPrefabs.Count)
        {
            spawnedLevel = Instantiate(levelPrefabs[index], Vector3.zero, Quaternion.identity);
            completedGroups = 0;
            
            if (TimeManager.instance != null)
            {
                TimeManager.instance.timeRemaining = 30f;
            }
        }
    }

public void AddCompletedGroup()
{
    completedGroups++;
    if (completedGroups >= totalGroups)
    {
        if (TimeManager.instance != null) TimeManager.instance.StopTimer();
        // KHÔNG tăng currentLevelIndex ở đây
        Invoke("GoToWinScene", 1.0f);
    }
}

    private void GoToWinScene()
    {
        loadingController.LoadScene("Win");
    }
    
    // Hàm hỗ trợ để MenuController kiểm tra xem đã là màn cuối chưa
    public static bool IsLastLevel(int totalLevels)
    {
        return currentLevelIndex >= totalLevels - 1;
    }
}