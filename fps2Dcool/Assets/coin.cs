using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().OnCoin(gameObject.GetComponent<PhotonView>());
        }
    }
}
