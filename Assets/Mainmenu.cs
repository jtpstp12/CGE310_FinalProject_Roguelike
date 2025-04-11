using UnityEngine;
using UnityEngine.SceneManagement;

public class Maimenu : MonoBehaviour
{
    // ฟังก์ชันสำหรับโหลด Scene ไปยัง GamePlay
    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene("GamePlay");  // ระบุชื่อ Scene ที่ต้องการโหลด
    }
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
