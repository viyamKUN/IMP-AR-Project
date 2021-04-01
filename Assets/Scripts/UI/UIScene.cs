using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UIScene : MonoBehaviour
{
    // Start is called before the first frame update

    public void ButtonGoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ButtonGoJoinScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ButtonGoBackScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ButtonExit()
    {
        //in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
        
        Application.Quit();
    }
}
