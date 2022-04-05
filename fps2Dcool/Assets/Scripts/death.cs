using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class death : MonoBehaviour
{
    public string Tag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if(collision.CompareTag(Tag))
        {
            Debug.Log("Die");
            Transform respawn= FindObjectOfType<spawnManager>().getSpawn(PhotonNetwork.LocalPlayer.ActorNumber);
            collision.transform.position = respawn.position;
        }
    }
}
