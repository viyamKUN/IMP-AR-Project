using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
public class SceneChange : MonoBehaviour
{
    public string SceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    
    public void ARtoLobby()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        LoaderUtility.Deinitialize();
    }

    public void LobbytoAR()
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }
}
