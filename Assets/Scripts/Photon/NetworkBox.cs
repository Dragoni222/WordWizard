using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBox : Photon.MonoBehaviour, IPunObservable
{
    Vector3 newPosition;
    Quaternion newRotation;
    [SerializeField] Rigidbody2D rigidbody2d;
    const float estimatedSpeed = 10;

    void Start()
    {
        rigidbody2d.simulated = gameObject.GetPhotonView().isMine;

        newPosition = transform.position;
        newRotation = transform.rotation;

        rigidbody2d = GetComponent<Rigidbody2D>();

        rigidbody2d.simulated = false;
    }

    void Update()
    {

        rigidbody2d.simulated = PhotonNetwork.isMasterClient;

        if (!PhotonNetwork.isMasterClient)
        {
            transform.position = Vector3.Lerp(transform.position,
                newPosition,
                Time.deltaTime * estimatedSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                newRotation,
                Time.deltaTime * estimatedSpeed);
        }
    }

    #region IPunObservable implementation

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else
        {
            newPosition = (Vector3)stream.ReceiveNext();
            newRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    #endregion
}
