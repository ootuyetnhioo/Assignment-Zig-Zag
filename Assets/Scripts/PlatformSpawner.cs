using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Biến để lưu trữ prefab của nền đất (platform)
    public GameObject platform;

    // Biến để lưu trữ vị trí của platform cuối cùng đã được tạo ra
    public Transform lastPlatform;

    // Biến để lưu trữ đường dẫn của các platform đã được tạo ra
    public Transform PlatformPath;

    // Biến để lưu trữ vị trí cuối cùng của platform đã được tạo ra
    Vector3 lasPos;

    // Biến để lưu trữ vị trí mới sẽ tạo ra platform
    Vector3 newPos;

    // Biến kiểm tra xem có nên dừng việc tạo platform hay không
    public bool stop;

    // Hàm được gọi khi script bắt đầu chạy
    void Start()
    {
        // Gán giá trị vị trí của lastPlatform cho biến lasPos
        lasPos = lastPlatform.position;

        // Bắt đầu coroutine để tạo ra các platform
        StartCoroutine(SpawnPlatforms());
    }

    // Hàm để tạo ra vị trí mới cho platform dựa trên vị trí cuối cùng của platform trước đó
    void GenratePos()
    {
        // Gán giá trị vị trí cuối cùng cho vị trí mới
        newPos = lasPos;

        // Random một số nguyên từ 0 đến 1
        int rand = Random.Range(0, 2);

        // Nếu rand > 0, di chuyển platform theo trục x
        if (rand > 0)
        {
            newPos.x += 2f;
        }
        // Ngược lại, di chuyển platform theo trục z
        else
        {
            newPos.z += 2f;
        }
    }

    // Coroutine để liên tục tạo ra các platform
    IEnumerator SpawnPlatforms()
    {
        // Vòng lặp sẽ tiếp tục cho đến khi biến stop trở thành true
        while (!stop)
        {
            // Gọi hàm để tạo ra vị trí mới cho platform
            GenratePos();

            // Tạo một instance mới của prefab platform tại vị trí mới
            // Quaternion.identity đại diện cho không gian quay không thay đổi
            // PlatformPath là cha của platform mới được tạo ra
            Instantiate(platform, newPos, Quaternion.identity, PlatformPath);

            // Gán giá trị vị trí mới cho lasPos để sử dụng trong lần tạo platform tiếp theo
            lasPos = newPos;

            // Đợi 0.2 giây trước khi tạo platform tiếp theo
            yield return new WaitForSeconds(0.2f);
        }
    }
}
