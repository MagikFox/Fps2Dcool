using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ennemy : MonoBehaviour
{
    [SerializeField] Transform Waypoint1;
    [SerializeField] Transform Waypoint2;

    public Vector3 position1;
    public Vector3 position2;
    private PhotonView PV;

    Vector3 currentTargetDestination;

    public float distanceTolerance = 0.5f; //you can change the tolerance to whatever you need it to be

    

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        position1 = Waypoint1.position;
        position2 = Waypoint2.position;
        transform.position = position1; //set the initial position
        currentTargetDestination = position2;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false)
            return;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetDestination, 1 * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTargetDestination) <= distanceTolerance)
        {
            //once we reach the current destination, set the other location as our new destination
            if (currentTargetDestination == position1)
            {
                currentTargetDestination = position2;
            }
            else
            {
                currentTargetDestination = position1;
            }
        }
    }
}
