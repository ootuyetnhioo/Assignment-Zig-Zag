using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Biến instance để tạo một singleton của GameManager
    public static GameManager instance;

    // Biến kiểm tra xem trò chơi đã bắt đầu hay chưa
    public bool isGameStarted;

    // GameObject chứa script PlatformSpawner để quản lý việc tạo ra các platform
    public GameObject platformSpawner;

    [Header("GameOver")]
    // GameObject chứa panel hiển thị khi trò chơi kết thúc
    public GameObject gameOverPanel;

    // Text hiển thị điểm số cuối cùng
    public TMP_Text lastScoreText;

    [Header("Score")]
    // Text hiển thị điểm số trong trò chơi
    public TMP_Text scoreText;

    // Text hiển thị điểm số tốt nhất
    public TMP_Text bestText;

    // Text hiển thị số sao đã thu thập
    public TMP_Text starText;

    [Header("Audio")]
    // Âm thanh khi bắt đầu trò chơi
    public AudioClip startGameAudioClip;

    // AudioSource để phát âm thanh
    private AudioSource audioSource;

    // Biến lưu trữ điểm số hiện tại trong trò chơi
    int score = 0;

    // Biến lưu trữ điểm số tốt nhất
    int bestScore;

    // Biến lưu trữ tổng số sao đã thu thập
    int totalStar;

    // Biến kiểm tra xem có đang cập nhật điểm số không
    bool countScore;

    // Hàm được gọi khi script được khởi tạo
    private void Awake()
    {
        // Tạo một singleton của GameManager
        if (instance == null)
        {
            instance = this;
        }

        // Thêm một AudioSource vào GameObject và cấu hình nó
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = startGameAudioClip;
        audioSource.loop = false;
    }

    // Hàm được gọi khi script bắt đầu
    void Start()
    {
        // Lấy dữ liệu về số sao và điểm số tốt nhất từ PlayerPrefs
        totalStar = PlayerPrefs.GetInt("totalStar");
        starText.text = totalStar.ToString();

        bestScore = PlayerPrefs.GetInt("bestScore");
        bestText.text = bestScore.ToString();
    }

    // Hàm được gọi mỗi frame
    void Update()
    {
        // Kiểm tra xem trò chơi đã bắt đầu hay chưa
        if (!isGameStarted)
        {
            // Nếu trò chơi chưa bắt đầu và người chơi nhấn chuột trái, thì bắt đầu trò chơi
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    // Hàm bắt đầu trò chơi
    public void GameStart()
    {
        // Đặt biến isGameStarted về true để bắt đầu trò chơi
        isGameStarted = true;

        // Đặt biến countScore về true để bắt đầu cập nhật điểm số
        countScore = true;

        // Bắt đầu coroutine để liên tục cập nhật điểm số
        StartCoroutine(UpdateScore());

        // Kích hoạt script PlatformSpawner để tạo ra các platform
        platformSpawner.SetActive(true);

        // Phát âm thanh khi bắt đầu trò chơi
        audioSource.Play();
    }

    // Hàm kết thúc trò chơi
    public void GameOver()
    {
        // Hiển thị panel GameOver
        gameOverPanel.SetActive(true);

        // Hiển thị điểm số cuối cùng trên panel
        lastScoreText.text = score.ToString();

        // Dừng cập nhật điểm số
        countScore = false;

        // Tắt script PlatformSpawner để ngừng tạo ra các platform
        platformSpawner.SetActive(false);

        // Lưu điểm số tốt nhất nếu nó lớn hơn điểm số tốt nhất hiện tại
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", score);
        }

        // Dừng phát âm thanh
        audioSource.Stop();
    }

    // Coroutine để liên tục cập nhật điểm số
    IEnumerator UpdateScore()
    {
        // Lặp vô hạn để cập nhật điểm số sau mỗi giây
        while (countScore)
        {
            // Đợi 1 giây
            yield return new WaitForSeconds(1f);

            // Tăng điểm số lên 1
            score++;

            // Hiển thị điểm số mới
            scoreText.text = score.ToString();

            // Nếu điểm số mới lớn hơn điểm số tốt nhất
            if (score > bestScore)
            {
                // Hiển thị điểm số mới làm điểm số tốt nhất
                scoreText.text = score.ToString();
                bestText.text = bestScore.ToString();
            }
            else
            {
                // Hiển thị điểm số mới
                scoreText.text = score.ToString();
            }
        }
    }

    // Hàm để chơi lại trò chơi
    public void ReplayGame()
    {
        // Load lại scene "Game" để chơi lại trò chơi
        SceneManager.LoadScene("Game");
    }

    // Hàm để cộng số sao khi người chơi thu thập được sao
    public void GetStar()
    {
        // Tăng số sao lên 1
        totalStar++;

        // Lưu số sao vào PlayerPrefs
        PlayerPrefs.SetInt("totalStar", totalStar);

        // Hiển thị số sao mới
        starText.text = totalStar.ToString();
    }
}
