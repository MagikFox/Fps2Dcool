using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Finish : MonoBehaviourPunCallbacks
{
    public List<int> playerlist = new List<int>();
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int myid = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
            bool found = false;
            for (int i = 0; i < playerlist.Count; i++)
            {
                if (playerlist[i] == myid)
                {
                    found = true;
                }
            }
            if (!found)
            {
                PV.RPC("addList", RpcTarget.All, myid);
                ;
            }
        }

        if (playerlist.Count == 2)
        {
            PV.RPC("leaveRoomMine", RpcTarget.All);
        }
    }

    [PunRPC]

    public void addList(int id)
    {
        playerlist.Add(id);
    }

    [PunRPC]

    public void removeList(int id)
    {
        playerlist.Remove(id);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null || collision.gameObject.GetComponentInParent<PhotonView>() == null)
        {
            return;
        }
        int myid = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
        bool found = false;
        for (int i = 0; i < playerlist.Count; i++)
        {
            if (playerlist[i] == myid)
            {
                found = true;
            }
        }
        if (found)
        {
            PV.RPC("removeList", RpcTarget.All, myid);
        }
    }

    [PunRPC]
    public void leaveRoomMine()
    {
        if (!(PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.Leaving))
            PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if (FindObjectOfType<RoomManager>())
        {
            PhotonNetwork.Destroy(FindObjectOfType<RoomManager>().gameObject);
        }
        PhotonNetwork.LoadLevel(0);
        Cursor.lockState = CursorLockMode.None;
    }
}
