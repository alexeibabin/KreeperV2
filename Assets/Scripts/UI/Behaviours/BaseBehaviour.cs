using UnityEngine;
using System.Collections;

public abstract class BaseBehaviour : MonoBehaviour
{

    public abstract void Action(GameObject target);
    public GameObject GetParentWaypointObject()
    {
        return transform.parent.parent.gameObject;
    }
}
