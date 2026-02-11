using UnityEngine;

public class DragFruit : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPos;
    private FruitData data;

    void Start()
    {
        // Lưu vị trí cũ để trả về nếu bé kéo sai thúng
        startPos = transform.position;
        // Lấy giá trị số lượng quả từ FruitData
        data = GetComponent<FruitData>();
    }

    void OnMouseDown()
    {
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
            // Cập nhật vị trí nhóm quả theo chuột/tay chạm
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    void CheckMatch()
    {
        // Tạo vùng quét va chạm hình tròn tại vị trí hiện tại của quả (bán kính 0.8f)
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f);

        // Kiểm tra xem có chạm vào Thúng (Tag: Basket) không
        if (hit != null && hit.CompareTag("Basket"))
        {
            Basket basket = hit.GetComponent<Basket>();

            // So sánh giá trị nhóm quả và số mục tiêu trên thúng
            if (basket != null && data != null && data.fruitValue == basket.targetNumber)
            {
                // ĐÚNG:
                // 1. Phát âm thanh (Sử dụng sound.instance có sẵn của bạn)
                if (sound.instance != null && !sound.instance.isMuted())
                {
                    // Bạn có thể tạo thêm hàm PlayCorrectSound() trong sound.cs
                    // Ở đây tạm gọi Toggle để kiểm tra kết nối
                    sound.instance.ToggleSound(); 
                }

                // 2. Báo cho GameManager
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddCompletedGroup();
                }

                // 3. Ẩn nhóm quả
                gameObject.SetActive(false);
            }
            else
            {
                // SAI (Nhầm thúng): Bay về chỗ cũ
                transform.position = startPos;
            }
        }
        else
        {
            // Thả ngoài: Bay về chỗ cũ
            transform.position = startPos;
        }
    }

    // Vẽ vùng nhận diện va chạm trong Scene để bạn dễ quan sát
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.8f);
    }
}