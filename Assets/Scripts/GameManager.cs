using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cấu hình trò chơi")]
    public int totalGroups = 3; // Táo, Lê, Cam
    private int completedGroups = 0;

    private void Awake()
    {
        // Thiết lập Singleton để các script khác dễ dàng truy cập
        if (instance == null)
        {
            instance = this;
            // Nếu bạn muốn GameManager tồn tại xuyên suốt các Scene:
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCompletedGroup()
    {
        completedGroups++;
        Debug.Log("Đã hoàn thành: " + completedGroups + "/" + totalGroups);

        // Nếu bé đã kéo đúng đủ số nhóm quả
        if (completedGroups >= totalGroups)
        {
            // Đợi 1 giây để bé thấy quả cuối cùng biến mất rồi mới chuyển cảnh
            Invoke("GoToWinScene", 1.0f);
        }
    }

    private void GoToWinScene()
    {
        Debug.Log("Chuyển sang màn hình chiến thắng!");
        // Gọi loadingController để chuyển sang Scene Win
        loadingController.LoadScene("Win");
    }
}