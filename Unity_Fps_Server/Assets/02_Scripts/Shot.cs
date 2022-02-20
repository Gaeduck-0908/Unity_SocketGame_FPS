using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // ÃÑ¾Ë
    public GameObject bullet;
    // ÃÑ±¸
    public GameObject muzzle;
    // ÃÑ¾Ë ¹ß»ç °£°Ý
    private float cooltime = 0.1f;
    // ÇöÀç ½Ã°£
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
