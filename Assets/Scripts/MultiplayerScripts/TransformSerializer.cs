using UnityEngine;
using System.Collections;

public class TransformSerializer : Photon.MonoBehaviour {

    private Transform localTransform;
    private Vector3 nextPosition;
    private Quaternion nextRotation;


    void Start()
    {
        localTransform = transform;
    }


    void Update()
    {
        if (photonView.isMine)
        {
            localTransform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 5);
            localTransform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We are sending data to the stream
            stream.SendNext(localTransform.position);
            stream.SendNext(localTransform.rotation);
        }
        else
        {
            // we are receiving data from the stream
            nextPosition = (Vector3)stream.ReceiveNext();
            nextRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
