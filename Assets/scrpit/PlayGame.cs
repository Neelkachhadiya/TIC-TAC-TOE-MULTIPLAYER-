
using System;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    string playername;
    Text playernametxt, p;
    GameObject btnHolder;
    PhotonView PV;
    int temp = 1;
    string str = "A";
    Button Reset;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        playername = PhotonNetwork.NickName;
        playernametxt = GameObject.Find("PlayerName").GetComponent<Text>();
        playernametxt.text = playername;
        btnHolder = GameObject.Find("BtnHolder");
        p = GameObject.Find("Print").GetComponent<Text>();
        Reset = GameObject.Find("ResetBtn").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnclick(int index)
    {

        if (playername == "Player A")
        {
            if (temp == 1)
            {
                temp = 0;
                str = "A";
            }
        }
        else if (playername == "Player B")
        {
            if (temp == 0)
            {
                temp = 1;
                str = "B";
            }
        }

        PV.RPC("syncData", RpcTarget.All, index, temp);
    }

    [PunRPC]
    void syncData(int index, int temp)
    {
        if ((temp == 0 && btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text == "") && str == "A")
        {
            p.text = "Player B's Turn: O";
            str = "B";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text = "X";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Button>().interactable = false;
            win(index);

        }

        if ((temp == 1 && btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text == "") && str == "B")
        {
            p.text = "Player A's Turn: X";
            str = "A";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text = "O";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Button>().interactable = false;
            win(index);
        }

    }


    void win(int n)
    {
        bool draw = true;

        for (int i = 0; i <8; i++)
        {
            if (btnHolder.transform.GetChild(i).GetComponentInChildren<Text>().text == "")
            {
                draw = false;
                break;
            }
        }

        if (draw)
        {
            p.text = " Drow ";
            Reset.GetComponent<Button>().interactable = true;
        }

        string OX = btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text;

        if (btnHolder.transform.GetChild(0).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(1).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(2).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();
        }else
        if (btnHolder.transform.GetChild(3).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(4).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(5).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();


        }else
        if (btnHolder.transform.GetChild(6).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(7).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(8).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }else
        if (btnHolder.transform.GetChild(0).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(3).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(6).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }else
        if (btnHolder.transform.GetChild(1).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(4).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(7).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }else
        if (btnHolder.transform.GetChild(2).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(5).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(8).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }else
        if (btnHolder.transform.GetChild(0).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(4).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(8).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }else
        if (btnHolder.transform.GetChild(2).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(4).GetComponentInChildren<Text>().text == OX &&
            btnHolder.transform.GetChild(6).GetComponentInChildren<Text>().text == OX)
        {
            p.text = OX + " wins ";
            OnDisablebtn();

        }

        
    }
    void OnDisablebtn()
    {
        for (int i = 0;i <= 8;i++)
        {
            btnHolder.transform.GetChild(i).GetComponentInChildren<Button>().interactable = false;
        }
        Reset.GetComponent<Button>().interactable = true;
    }



    public void ResetBtn()
    {
        p.text = "Resetting...";
        PV.RPC("ResetGame", RpcTarget.All); 

    }

    [PunRPC]
    void ResetGame()
    {
        for (int i = 0; i <= 8; i++)
        {
            btnHolder.transform.GetChild(i).GetComponentInChildren<Text>().text = "";
            btnHolder.transform.GetChild(i).GetComponentInChildren<Button>().interactable = true;
        }

        temp = 1;
        str = "A"; 
        p.text = "Player A's Turn: X"; 
        Reset.GetComponent<Button>().interactable = false; 
    }

}

