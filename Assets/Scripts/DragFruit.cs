using UnityEngine;

public class DragFruit : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPos;
    private FruitData data;

    [Header("Cấu hình va chạm")]
    [SerializeField] private float checkRadius = 0.7f; // Độ lớn vòng tròn quét va chạm (đã chỉnh bé lại)

    void Start()
    {
        // Lưu vị trí ban đầu để trả về nếu kéo sai thúng
        startPos = transform.position;
        // Lấy thông tin giá trị quả từ script FruitData
        data = GetComponent<FruitData>();
    }

    void OnMouseDown()
    {
        // THÊM DÒNG NÀY: Nếu game đang dừng (Panel đang hiện) thì không cho kéo
    if (Time.timeScale == 0) return; 

    isDragging = true;
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckMatch();
    }

    void Update()
    {
        if (isDragging)
        {
            // Cập nhật vị trí nhóm quả theo chuột hoặc tay chạm
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; 
            transform.position = mousePos;
        }
    }

    void CheckMatch() 
    {
        // Sử dụng bán kính checkRadius đã cấu hình để quét thúng
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius); 
        bool foundCorrectBasket = false;

        foreach (var hit in hits) 
        {
            // Kiểm tra vật thể chạm phải có Tag là Basket không
            if (hit.CompareTag("Basket")) 
            {
                Basket basket = hit.GetComponent<Basket>();
                
                // Kiểm tra script Basket và so khớp giá trị quả với thúng
                if (basket != null && data != null && data.fruitValue == basket.targetNumber) 
                {
                    foundCorrectBasket = true;
                    
                    // Đổi hình ảnh thúng thành thúng có quả
                    basket.UpdateToFullSprite(); 
                    
                    break;
                }
            }
        }

        if (foundCorrectBasket) 
        {
            gameObject.SetActive(false); // Ẩn quả sau khi đã vào thúng
            
            // Thông báo cho GameManager đã hoàn thành thêm 1 nhóm
            if (GameManager.instance != null) 
            {
                GameManager.instance.AddCompletedGroup();
            }
        } 
        else 
        {
            // Trả về vị trí cũ nếu thả sai chỗ hoặc sai thúng
            transform.position = startPos; 
        }
    }

    // Vẽ vùng nhận diện trong Scene bằng vòng tròn màu vàng
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Hiển thị vòng tròn với bán kính khớp với code xử lý
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}