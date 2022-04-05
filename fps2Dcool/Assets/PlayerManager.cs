using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    
    private PhotonView PV;
    private GameObject Controller;
    private int Coin = 0;
    

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public PhotonView getPV()
    {
        return PV;
    }
    public void GotNewCoin()
    {
        Coin++;
        Debug.LogError(Coin);
    }
    private void Start()
    {
        if (PV.IsMine)
            createController();
    }

    public void createController()
    {
        Transform mySpawn = FindObjectOfType<spawnManager>().getSpawn(PhotonNetwork.LocalPlayer.ActorNumber);
        Controller = PhotonNetwork.Instantiate(Path.Combine("prefabs", "PlayerController"), mySpawn.position, Quaternion.identity);
    }

    public void Die()
    {
        return;
    }
}
