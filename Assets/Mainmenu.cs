using UnityEngine;
using UnityEngine.SceneManagement;

public class Maimenu : MonoBehaviour
{
    // �ѧ��ѹ����Ѻ��Ŵ Scene ��ѧ GamePlay
    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene("GamePlay");  // �кت��� Scene ����ͧ�����Ŵ
    }
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
