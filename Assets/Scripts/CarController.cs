using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Tốc độ di chuyển của ô tô
    public float moveSpeed;

    // Biến kiểm tra hướng di chuyển của ô tô (true: trái, false: phải)
    bool faceLeft;

    // Biến kiểm tra việc nhấn tab lần đầu tiên
    bool firstTab;

    // Hàm được gọi mỗi frame
    void Update()
    {
        // Kiểm tra xem trò chơi đã bắt đầu chưa
        if (GameManager.instance.isGameStarted)
        {
            // Nếu trò chơi đã bắt đầu, thực hiện di chuyển và kiểm tra input
            Move();
            CheckInput();
        }

        // Kiểm tra xem ô tô đã rơi xuống khỏi màn hình chưa
        if (transform.position.y <= -2)
        {
            // Nếu đã rơi xuống, kết thúc trò chơi
            GameManager.instance.GameOver();
        }
    }

    // Hàm thực hiện di chuyển ô tô theo hướng hiện tại
    void Move()
    {
        // Di chuyển ô tô theo hướng hiện tại với tốc độ đã được xác định
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Hàm kiểm tra input từ người chơi (click chuột)
    void CheckInput()
    {
        // Nếu người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Thay đổi hướng di chuyển của ô tô
            ChangeDir();
        }
    }

    // Hàm thay đổi hướng di chuyển của ô tô
    void ChangeDir()
    {
        // Nếu ô tô đang đi về bên trái
        if (faceLeft)
        {
            // Đặt biến faceLeft về false và quay ô tô 90 độ theo trục y để đổi hướng sang bên phải
            faceLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            // Nếu ô tô đang đi về bên phải, đặt biến faceLeft về true và quay ô tô 90 độ theo trục y để đổi hướng sang bên trái
            faceLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
