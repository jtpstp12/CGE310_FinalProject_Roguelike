using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenuPanel; // 👉 ลาก UI Pause Panel มาใส่ใน Inspector

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // ถ้าต้องการให้ GameManager คงอยู่ข้าม Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // 📌 กด ESC เพื่อ pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // รีเซ็ตเวลาเผื่อมาจาก pause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // รีเซ็ตเวลาเผื่อมาจาก pause
        SceneManager.LoadScene("MainMenu");
    }

    // ✅ ฟังก์ชัน Pause
    public void PauseGame()
    {
        Time.timeScale = 0; // หยุดเวลา
        isPaused = true;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);
    }

    // ✅ ฟังก์ชัน Resume
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }
}
