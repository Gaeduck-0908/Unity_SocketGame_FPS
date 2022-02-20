using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ī�޶� ȸ���ӵ�
    private float SpinSpeed = 2000.0f;

    float MouseX;
    float MouseY;

    private void Update()
    {
        //X��
        float horizontal = Input.GetAxis("Mouse X");
        //Y��
        float Vertical = Input.GetAxis("Mouse Y");

        //���� ���� ���� ��ǥ = ȸ���ӵ� * ������
        MouseX += horizontal * SpinSpeed * Time.deltaTime;
        MouseY += Vertical * SpinSpeed * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -90, 90);

        //���� ���� eulerAngles = ���Ϸ���
        transform.eulerAngles = new Vector3(-MouseY, MouseX, 0);
    }
}
