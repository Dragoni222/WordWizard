using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using TMPro;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
public class CreateAndJoinRooms : Photon.PunBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField createInput;
    public TMP_InputField joinInput;


   

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.room != null)
        {
            if (PhotonNetwork.room.PlayerCount > 1)
            {
                PhotonNetwork.LoadLevel("MainGame");
            }
        }
      
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
        
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    private void OnDisconnectedFromMasterServer()
    {
        SceneManager.LoadScene("Loading");
    }

}
