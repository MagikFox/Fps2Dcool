using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class exitGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(FindObjectOfType<RoomManager>().gameObject);
            PhotonNetwork.LeaveRoom();
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
