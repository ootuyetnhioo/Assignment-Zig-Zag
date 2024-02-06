using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Biến để lưu trữ prefab của hiệu ứng nổ khi vật phẩm được lấy
    public GameObject starBlast;

    // Hàm được gọi khi đối tượng được khởi tạo
    void Start()
    {
        // Gọi hàm để thiết lập màu sắc ngẫu nhiên cho ngôi sao
        SetRandomStarColor();
    }

    // Hàm được gọi mỗi frame
    void Update()
    {
        // Quay vật phẩm xung quanh trục Z với một góc cố định mỗi giây
        transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
    }

    // Hàm được gọi khi có sự va chạm với các đối tượng khác
    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player" hay không
        if (collision.gameObject.tag == "Player")
        {
            // Kiểm tra xem đối tượng Item hiện tại có tag là "Star" hay không
            if (gameObject.tag == "Star")
            {
                // Gọi hàm GetStar từ GameManager để xử lý việc lấy vật phẩm sao
                GameManager.instance.GetStar();

                // Tạo một hiệu ứng nổ tại vị trí của vật phẩm
                Instantiate(starBlast, transform.position, Quaternion.identity);
            }

            // Hủy bỏ đối tượng Item sau khi nó được lấy
            Destroy(gameObject);
        }
    }

    // Hàm để thiết lập màu sắc ngẫu nhiên cho ngôi sao
    private void SetRandomStarColor()
    {
        // Lấy component Renderer từ đối tượng ngôi sao
        Renderer starRenderer = GetComponent<Renderer>();

        // Kiểm tra xem Renderer có tồn tại không
        if (starRenderer != null)
        {
            // Thiết lập màu sắc ngẫu nhiên
            starRenderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
