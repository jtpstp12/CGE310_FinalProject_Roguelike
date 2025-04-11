using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject expObjectPrefab; // EXP Object Prefab
    [SerializeField] private GameObject healthGlobe, staminaGlobe;

    // �ѧ��ѹ���ж١���¡������ѵ�ٵ��
    public void DropItems()
    {
        // ��ͻ EXP Object �ء���駷���ѵ�ٵ��
        DropEXP();

        // ��ͻ��������
        int randomNum = Random.Range(1, 10);

        if (randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 0)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
    }

    // �ѧ��ѹ��ͻ EXP Object
    private void DropEXP()
    {
        Instantiate(expObjectPrefab, transform.position, Quaternion.identity); // ��ͻ EXP Object �����˹觢ͧ�ѵ��
    }
}
