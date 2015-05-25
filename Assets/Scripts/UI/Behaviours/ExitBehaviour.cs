using UnityEngine;
using System.Collections;

public class ExitBehaviour : BaseBehaviour
{

    public override void Action(GameObject target)
    {
        Debug.Log("Exiting..");
        Application.Quit();
    }
}
