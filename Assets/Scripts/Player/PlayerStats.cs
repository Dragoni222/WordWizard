using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Photon;

public class PlayerStats : Photon.MonoBehaviour
{
    //basic stats
    public float health = 50;
    public float mana = 50;

    //Photon stuff
    public int playernum;
    PhotonView view;

    //UI
    [SerializeField] TextMeshProUGUI hpText;

    
    private void Start()
    {
        
        view = gameObject.GetPhotonView();
        
    }
    void Update()
    {
        hpText.text = $"{health}";
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
            stream.SendNext(mana);
        }
        else
        {
            health = (float)stream.ReceiveNext();
            mana = (float)stream.ReceiveNext();
        }
    }

}
