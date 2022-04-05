using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject door;
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("openDoor", RpcTarget.All);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("closeDoor", RpcTarget.All);
        }
    }

    [PunRPC]
    public void openDoor()
    {
        door.SetActive(false);
    }

    [PunRPC]
    public void closeDoor()
    {
        door.SetActive(true);
    }
}
