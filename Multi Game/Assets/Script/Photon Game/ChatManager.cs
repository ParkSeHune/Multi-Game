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

            //InputField�� �ִ� �ؽ�Ʈ�� ������
            string chat = PhotonNetwork.NickName + " : " + input.text;

            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    public void RpcAddChat(string msg)
    {
        //chatPrefab �ϳ� �����Ͽ� text�� ���� ����
        GameObject chat = Instantiate(ChatPrefab);
        chat.GetComponent<Text>().text = msg;

        //��ũ�� �� = content�� �ڽ����� ���
        chat.transform.SetParent(ChatContent);

        //ä���� �Է��� �Ŀ��� �̾ �Է��� �� �ֵ��� ����
        input.ActivateInputField();

        //input �ؽ�Ʈ�� �ʱ�ȭ
        input.text = "";
    }
}