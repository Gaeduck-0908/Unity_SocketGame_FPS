using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //카메라 회전속도
    private float SpinSpeed = 2000.0f;

    float MouseX;
    float MouseY;

    private void Update()
    {
        //X값
        float horizontal = Input.GetAxis("Mouse X");
        //Y값
        float Vertical = Input.GetAxis("Mouse Y");

        //현재 각도 저장 좌표 = 회전속도 * 프레임
        MouseX += horizontal * SpinSpeed * Time.deltaTime;
        MouseY += Vertical * SpinSpeed * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -90, 90);

        //각도 갱신 eulerAngles = 오일러각
        transform.eulerAngles = new Vector3(-MouseY, MouseX, 0);
    }
}
