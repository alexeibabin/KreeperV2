using UnityEngine;
using System.Collections;

public class TransformSerializer : Photon.MonoBehaviour
{

    private Transform localTransform;

    private Vector3 nextPosition;
    private Quaternion nextRotation;

    private Transform mainCameraTransform;

    void Start()
    {
        localTransform = transform;
        if (localTransform.Find("Head"))
        {
            mainCameraTransform = localTransform.FindChild("Head").GetChild(0);
        }
    }


    void Update()
    {
        if (!photonView.isMine)
        {
            Vector3 eulerAngles = nextRotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;

            localTransform.rotation = Quaternion.Euler(Vector3.Lerp(localTransform.rotation.eulerAngles, eulerAngles, 0.5f));
            localTransform.position = Vector3.Lerp(localTransform.position, nextPosition, 0.5f);

            //localTransform.rotation = Quaternion.Lerp(localTransform.rotation, nextRotation, 0.5f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We are sending data to the stream
            stream.SendNext(localTransform.position);

            if (mainCameraTransform)
            {
                stream.SendNext(mainCameraTransform.rotation);
            }
            else
            {
                stream.SendNext(localTransform.rotation);
            }

        }
        else
        {
            // we are receiving data from the stream
            nextPosition = (Vector3)stream.ReceiveNext();
            nextRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
