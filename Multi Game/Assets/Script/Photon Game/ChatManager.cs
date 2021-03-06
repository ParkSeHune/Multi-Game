using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField input;
    public GameObject ChatPrefab;
    public Transform ChatContent;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            //InputField에 있는 텍스트를 가져옴
            string chat = PhotonNetwork.NickName + " : " + input.text;

            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    public void RpcAddChat(string msg)
    {
        //chatPrefab 하나 생성하여 text에 값을 설정
        GameObject chat = Instantiate(ChatPrefab);
        chat.GetComponent<Text>().text = msg;

        //스크롤 뷰 = content에 자식으로 등록
        chat.transform.SetParent(ChatContent);

        //채팅을 입력한 후에도 이어서 입력할 수 있도록 설정
        input.ActivateInputField();

        //input 텍스트를 초기화
        input.text = "";
    }
}