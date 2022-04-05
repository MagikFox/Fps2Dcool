using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.5f;
    private Rigidbody2D _playerRigidbody;
    private PhotonView PV;
    public int JumpForce = 3;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        if(PV.IsMine == false)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

    }

    public void OnCoin(PhotonView coinPV)
    {
        PV.RPC("tookCoin", RpcTarget.All, coinPV.ViewID);
    }

    [PunRPC]
    public void tookCoin(int viewID)
    {
        if (PhotonNetwork.IsMasterClient) //si master client on détruit la pièce 
            PhotonNetwork.Destroy(FindObjectsOfType<coin>().Where(x => x.GetComponent<PhotonView>().ViewID == viewID).ToArray()[0].gameObject);
        FindObjectsOfType<PlayerManager>().Where(x => x.getPV().IsMine).ToArray()[0].GotNewCoin();
    }

    private void Start()
    {
        if(PV.IsMine == false)
        {
            Destroy(_playerRigidbody);
        }
    }

    private void Update()
    {
        if (PV.IsMine == false)
            return;

        //Dans le cas ou c'est égal a true on fait toutes nos actions en local 
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void MovePlayer()
    {
        var horizontalinput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector3(horizontalinput * playerSpeed, _playerRigidbody.velocity.y);
    }

    public void Jump()
    {
        _playerRigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}
