using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetFloat("TeamA_TotalScore", 0f);
        PlayerPrefs.SetFloat("TeamB_TotalScore", 0f);
    }
}
