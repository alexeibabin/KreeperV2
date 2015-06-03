using UnityEngine;
using System.Collections;

public class EnemyLocationSettingsContainer : MonoBehaviour
{


    public Transform[] waypoints;


    public void SetEnemyParameters(GameObject enemy)
    {
        Debug.Log("Setting enemy waypoints");
        enemy.GetComponent<Patrolling>().SetWaypoints(waypoints);
    }
}
