using UnityEngine;
using System.Collections;

public class TransformSerializer : Photon.MonoBehaviour
{

	public Transform headBone; 

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
            Vector3 bodyAngles = nextRotation.eulerAngles;
			Vector3 headAngles = new Vector3(0, bodyAngles.z, -bodyAngles.x);
            bodyAngles.x = 0;
            bodyAngles.z = 0;

            localTransform.rotation = Quaternion.Euler(Vector3.Lerp(localTransform.rotation.eulerAngles, bodyAngles, 0.5f));
            localTransform.position = Vector3.Lerp(localTransform.position, nextPosition, 0.5f);

			if (headBone){
				//headBone.rotation = Quaternion.Euler(Vector3.Lerp(headBone.rotation.eulerAngles, headAngles, 0.5f));
				headBone.localRotation = Quaternion.Euler(headAngles);
			}
            
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
