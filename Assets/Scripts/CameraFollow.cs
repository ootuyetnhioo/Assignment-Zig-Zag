using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Transform của đối tượng mà camera sẽ theo dõi
    public Transform target;

    // Tốc độ theo dõi của camera
    public float followSpeed = 5f;

    // Vector để lưu trữ khoảng cách ban đầu giữa camera và đối tượng theo dõi
    private Vector3 distance;

    // Hàm được gọi khi script được khởi tạo
    void Start()
    {
        // Tính và lưu trữ khoảng cách ban đầu giữa camera và đối tượng theo dõi
        distance = target.position - transform.position;
    }

    // Hàm được gọi mỗi frame
    void Update()
    {
        // Kiểm tra xem đối tượng theo dõi có nằm trên màn hình hay không (để tránh camera đi xuống khi đối tượng rơi xuống khỏi màn hình)
        if (target.position.y >= 0)
        {
            // Gọi hàm Follow để camera theo dõi đối tượng
            Follow();
        }
    }

    // Hàm để camera theo dõi đối tượng
    void Follow()
    {
        // Lấy vị trí hiện tại của camera
        Vector3 currentPosition = transform.position;

        // Lấy vị trí mục tiêu mà camera cần đến, là vị trí của đối tượng theo dõi trừ đi khoảng cách ban đầu
        Vector3 targetPosition = target.position - distance;

        // Sử dụng Lerp để di chuyển camera từ vị trí hiện tại đến vị trí mục tiêu với tốc độ được xác định bởi followSpeed
        transform.position = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
    }
}
