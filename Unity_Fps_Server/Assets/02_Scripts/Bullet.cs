using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;
    private float speed = 80; //�Ѿ��� �ӵ�

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground") //���� ������
        {
            Destroy(gameObject); //������Ʈ ����
        }
    }
    private void Start()
    {
        Invoke("DestroyBullet", 2f); //2�ʵ� ����
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject); //������Ʈ ����
    }
}
