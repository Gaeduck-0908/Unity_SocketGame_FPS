using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void SceneChanger()
    {
        if(SceneManager.GetActiveScene().name == "01_Start")
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("02_Main");
            }
        }
    }
}
