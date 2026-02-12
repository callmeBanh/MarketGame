using UnityEngine;

public class DragFruit : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPos;
    private FruitData data;

    void Start()
    {
        // Lưu vị trí ban đầu để trả về nếu kéo sai thúng
        startPos = transform.position;
        // Lấy thông tin giá trị quả (1, 2, hoặc 3) từ script FruitData
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
            // Cập nhật vị trí nhóm quả theo chuột hoặc tay chạm
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Giữ nguyên tọa độ Z để không bị khuất sau Background
            transform.position = mousePos;
        }
    }

void CheckMatch() {
    // Quét tất cả các vật thể chạm vào vùng tròn
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.0f);
    bool foundCorrectBasket = false;

    foreach (var hit in hits) {
        if (hit.CompareTag("Basket")) {
            Basket basket = hit.GetComponent<Basket>();
            if (basket != null && data != null && data.fruitValue == basket.targetNumber) {
                foundCorrectBasket = true;
                break;
            }
        }
    }

    if (foundCorrectBasket) {
        gameObject.SetActive(false); // Ẩn ngay
        if (GameManager.instance != null) GameManager.instance.AddCompletedGroup();
    } else {
        transform.position = startPos; // Luôn bay về nếu không khớp
    }
}

    // Vẽ vùng nhận diện trong Scene để bạn dễ căn chỉnh
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.2f);
    }
}