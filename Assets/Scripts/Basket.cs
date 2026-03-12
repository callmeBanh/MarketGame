using UnityEngine;

public class Basket : MonoBehaviour
{
    public int targetNumber; 
    
    [Header("Cấu hình hình ảnh")]
    public Sprite fullBasketSprite; // Kéo hình rổ có quả vào đây trong Inspector

    // Hàm này sẽ được gọi từ DragFruit khi kéo đúng quả
    public void UpdateToFullSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && fullBasketSprite != null)
        {
            sr.sprite = fullBasketSprite;
        }
    }
}