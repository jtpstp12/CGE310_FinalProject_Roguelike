using System.Collections;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    // �纪��ͧ͢ Scene Transition ������
    public string SceneTransitionName { get; private set; }

    // ��駤�Ҫ��� Scene Transition
    public void SetTransitionName(string sceneTransitionName)
    {
        // ��Ǩ�ͺ��� sceneTransitionName ����繤����ҧ
        if (string.IsNullOrEmpty(sceneTransitionName))
        {
            Debug.LogError("Scene Transition Name cannot be null or empty!");
            return;
        }

        // ��˹���Ҫ��� Scene Transition
        this.SceneTransitionName = sceneTransitionName;
    }

    // ������ҧ����� Transition Name
    public void TransitionToScene()
    {
        if (!string.IsNullOrEmpty(SceneTransitionName))
        {
            // ����� SceneTransitionName ��������¹ Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneTransitionName);
        }
        else
        {
            Debug.LogError("Scene Transition Name is not set!");
        }
    }

    // �ѧ��ѹ����Ѻ����ä�ԡ���� UI �繡������¹�ҡ
    public void LoadSceneFromButton(string GamePlay)
    {
        SetTransitionName(GamePlay);  // ��駪��� Scene ������Ŵ
        TransitionToScene();           // ����¹�ҡ
    }
}
