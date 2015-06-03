using UnityEngine;
using System.Collections;

public class EnemyLocationSettingsContainer : MonoBehaviour
{


    public Transform[] waypoints;


    public void SetEnemyParameters(GameObject enemy)
    {
        enemy.GetComponent<Patrolling>().SetWaypoints(waypoints);
    }
}
