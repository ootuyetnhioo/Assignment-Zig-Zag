using UnityEngine;

public class Platform : MonoBehaviour
{
    // Biến để lưu trữ prefab của hiệu ứng nổ khi platform rơi
    public GameObject platformBlast;

    // Biến để lưu trữ prefab của vật phẩm sao
    public GameObject star;

    // Hàm được gọi khi platform được khởi tạo
    void Start()
    {
        // Sinh một số nguyên ngẫu nhiên từ 1 đến 20
        int randNumber = UnityEngine.Random.Range(1, 21);

        // Lấy vị trí hiện tại của platform
        Vector3 tempPos = transform.position;

        // Tăng giá trị y của vị trí lên để đặt vật phẩm trên platform
        tempPos.y += 1.2f;

        // Nếu số ngẫu nhiên nhỏ hơn 4, tạo một prefab sao tại vị trí mới
        if (randNumber < 4)
        {
            Instantiate(star, tempPos, star.transform.rotation);
        }
    }

    // Hàm được gọi khi có sự va chạm với player và player rời khỏi platform
    private void OnCollisionExit(Collision collision)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player" hay không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Gọi hàm FallDown sau 0.2 giây sử dụng hàm Invoke
            Invoke("FallDown", 0.2f);
        }
    }

    // Hàm để thực hiện platform rơi xuống sau khi player rời khỏi
    void FallDown()
    {
        // Tạo một hiệu ứng nổ tại vị trí của platform
        Instantiate(platformBlast, transform.position, Quaternion.identity);

        // Kích hoạt physics cho platform (kinematic = false) để nó rơi tự do
        GetComponent<Rigidbody>().isKinematic = false;

        // Hủy bỏ đối tượng platform sau 0.5 giây
        Destroy(gameObject, 0.5f);
    }
}
