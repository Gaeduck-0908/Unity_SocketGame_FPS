using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;
    private float speed = 80; //총알의 속도

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground") //땅에 닿을시
        {
            Destroy(gameObject); //오브젝트 삭제
        }
    }
    private void Start()
    {
        Invoke("DestroyBullet", 2f); //2초뒤 삭제
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject); //오브젝트 삭제
    }
}
