using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject lerpPrefab;
    [SerializeField] Vector2 P1StartPos;
    [SerializeField] Vector2 P2StartPos;

    public PhotonPlayer localPlayer;
    public GameObject localPlayerObject;

    void Start()
    {

        PhotonPlayer[] allPlayers = PhotonNetwork.playerList;

        foreach(PhotonPlayer p in allPlayers)
        {
            if (p.IsLocal)
            {
                localPlayer = p;
                
            }
        }


        if (PhotonNetwork.isMasterClient)
        {
            PhotonView pv = PhotonNetwork.Instantiate(playerPrefab.name, P1StartPos, Quaternion.identity, 0).GetPhotonView();
            localPlayerObject = pv.gameObject;
            pv.TransferOwnership(localPlayer);

            pv = PhotonNetwork.Instantiate(lerpPrefab.name, P1StartPos, Quaternion.identity, 0).GetPhotonView();
            pv.TransferOwnership(localPlayer);
        }
        

    }

 
}
