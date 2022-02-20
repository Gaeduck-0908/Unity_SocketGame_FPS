using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // �Ѿ�
    public GameObject bullet;
    // �ѱ�
    public GameObject muzzle;
    // �Ѿ� �߻� ����
    private float cooltime = 0.1f;
    // ���� �ð�
    private float time = 0;

    void Update()
    {
        if (time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet);
                bullet.transform.position = muzzle.transform.position;
                bullet.transform.rotation = muzzle.transform.rotation;
            }
            time = cooltime;
        }

        time -= Time.deltaTime;
    }
}
