
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;

public class RoomItembutton : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    public RoomInfo roomInfo;

    public void SetupRoomButton(RoomInfo _info)
    {
        roomName.text = _info.Name;
        roomInfo = _info;
    }

    public void JoinRoom()
    {
        MenuManager.Instance.openMenu("LoadingMenu");
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }
}
