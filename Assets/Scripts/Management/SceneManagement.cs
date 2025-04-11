using System.Collections;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    // เก็บชื่อของ Scene Transition ที่จะใช้
    public string SceneTransitionName { get; private set; }

    // ตั้งค่าชื่อ Scene Transition
    public void SetTransitionName(string sceneTransitionName)
    {
        // ตรวจสอบว่า sceneTransitionName ไม่เป็นค่าว่าง
        if (string.IsNullOrEmpty(sceneTransitionName))
        {
            Debug.LogError("Scene Transition Name cannot be null or empty!");
            return;
        }

        // กำหนดค่าชื่อ Scene Transition
        this.SceneTransitionName = sceneTransitionName;
    }

    // ตัวอย่างการใช้ Transition Name
    public void TransitionToScene()
    {
        if (!string.IsNullOrEmpty(SceneTransitionName))
        {
            // ใช้ชื่อ SceneTransitionName เพื่อเปลี่ยน Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneTransitionName);
        }
        else
        {
            Debug.LogError("Scene Transition Name is not set!");
        }
    }

    // ฟังก์ชันสำหรับให้การคลิกปุ่ม UI เป็นการเปลี่ยนฉาก
    public void LoadSceneFromButton(string GamePlay)
    {
        SetTransitionName(GamePlay);  // ตั้งชื่อ Scene ที่จะโหลด
        TransitionToScene();           // เปลี่ยนฉาก
    }
}
