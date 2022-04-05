using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("activeBridge", RpcTarget.All);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("disableBridge", RpcTarget.All);
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("disableBridge", RpcTarget.All);
        }
    }*/

    [PunRPC]
    public void activeBridge()
    {
        bridge.SetActive(true);
    }

    [PunRPC]
    public void disableBridge()
    {
        bridge.SetActive(false);
    }
}
