using UnityEditor.Rendering;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConectToServer : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Menu");
    }

}
