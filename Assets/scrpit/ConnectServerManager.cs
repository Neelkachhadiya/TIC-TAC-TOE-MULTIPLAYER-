using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
public class Connectservermanager : MonoBehaviourPunCallbacks
{
    public GameObject Connectserver, DisConnectserver;
    public InputField CreatedRoomtext;
    public Text massagetxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConnectBtnClick() 
    { 
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnected()
    {
        massagetxt.text = "Connected To server";
        print("Connected To server");
        Connectserver.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        massagetxt.text = "Connected To Master Server";

        print("Connected To Master Server");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        massagetxt.text = "DisConnected To server :" + cause;

        print("DisConnected To server :"+cause);
        DisConnectserver.SetActive(true);

    }
    public void BackBtn()
    {
        DisConnectserver.SetActive(false);
    }
    public override void OnJoinedLobby()
    {
        massagetxt.text = "Player Join In Lobby";

        print("Player Join In Lobby");
    }
    public void CreateRoomBtnClick()
    {
        PhotonNetwork.CreateRoom(CreatedRoomtext.text,new RoomOptions { MaxPlayers=2,IsOpen=true,IsVisible=true});
    }
    public override void OnCreatedRoom()
    {
        massagetxt.text = "Room Created";

        print("Room Created");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        massagetxt.text = "Room Created Failed :" + message;
        print("Room Created Failed :"+message);

    }
    public void JoinRoomBtnClick()
    {
        PhotonNetwork.JoinRoom(CreatedRoomtext.text);
    }
    public override void OnJoinedRoom()
    {
        massagetxt.text = "Room Joined :"+CreatedRoomtext.text;
        print("Room Joined :"+CreatedRoomtext.text);
        if(PhotonNetwork.CountOfPlayersInRooms == 0)
        {
            PhotonNetwork.NickName = "Player A";
        }
        else
        {
            PhotonNetwork.NickName = "Player B";

        }
        PhotonNetwork.LoadLevel("Play");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        massagetxt.text = "Room Joined Falied :" + message;

        print("Room Joined Falied :" + message);
    }
}
