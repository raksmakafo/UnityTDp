using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        string roomName = joinInput.text;
        Debug.Log("Trying to join room: '" + roomName + "'");

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogError("Room name is empty. Cannot join room.");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
    }



    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
