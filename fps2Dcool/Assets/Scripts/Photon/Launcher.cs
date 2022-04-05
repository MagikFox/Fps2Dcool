using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] Transform RoomImage;
    [SerializeField] GameObject ButtonRoomprefab;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform PseudoImage;
    [SerializeField] GameObject PseudoTextPrefab;
    [SerializeField] Button StartGameButton;

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartGameButton.interactable = true;
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (PhotonNetwork.IsConnected == false)
            PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.openMenu("MainMenu");
        Debug.Log("Joined lobby :)");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // si un player rentre dans la room on reçoit le Player 
    {
        //Instantiate(PseudoTextPrefab, PseudoImage).GetComponent<RoomPseudoItem>().Setup(newPlayer);

        GameObject myNewPseudo = Instantiate(PseudoTextPrefab, PseudoImage);
        myNewPseudo.GetComponent<RoomPseudoItem>().Setup(newPlayer);
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // même chose mais il part 
    {
        Player[] players = PhotonNetwork.PlayerList; // récupère tout les players de la liste 

        foreach (Transform transform in PseudoImage) // détruit tout les prefabs pseudos actuellement dans l'image 
        {
            Destroy(transform.gameObject);
        }

        for(int i = 0; i < players.Length; i++) // Boucle sur tout les players de notre tableau récupéré précedement 
        {
            GameObject myNewPseudo = Instantiate(PseudoTextPrefab, PseudoImage); // Instantie le player de [i] du tableau dans l'image 
            myNewPseudo.GetComponent<RoomPseudoItem>().Setup(players[i]);
        }
    }

    public void QuitRoom()
    {
        StartGameButton.interactable = false;
        PhotonNetwork.LeaveRoom();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in RoomImage)
        {
            Destroy(transform.gameObject);
        }
        for (int i = 0; i<roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList == false)
                Instantiate(ButtonRoomprefab, RoomImage).GetComponent<RoomItembutton>().SetupRoomButton(roomList[i]);
        }
    }

    public void createRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        MenuManager.Instance.openMenu("LoadingMenu");
        PhotonNetwork.CreateRoom(roomNameInputField.text);
    }

    public void LaunchGame()
    {
        PhotonNetwork.LoadLevel(1);
    }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartGameButton.interactable = true;
        }

        //changer le nom de la room dans l'ui 
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;


        Player[] players = PhotonNetwork.PlayerList; // récupère tout les players de la liste 

        foreach (Transform transform in PseudoImage) // détruit tout les prefabs pseudos actuellement dans l'image 
        {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < players.Length; i++) // Boucle sur tout les players de notre tableau récupéré précedement 
        {
            GameObject myNewPseudo = Instantiate(PseudoTextPrefab, PseudoImage); // Instantie le player de [i] du tableau dans l'image 
            myNewPseudo.GetComponent<RoomPseudoItem>().Setup(players[i]);
        }

        //
        MenuManager.Instance.openMenu("RoomMenu");
    }

    public void setUserName(string actualUsername)
    {
        Debug.Log(actualUsername);
        PhotonNetwork.NickName = actualUsername;
    }
}
