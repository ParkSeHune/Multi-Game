using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomJoin, RoomCreate;

    public GameObject RoomPrefab;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if (RoomName.text.Length > 0)
        {
            RoomJoin.interactable = true;
        }
        else
        {
            RoomJoin.interactable= false;
        }

        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable = true;
        }
        else
        {
            RoomCreate.interactable = false;
        }
    }

    public void OnClickCreateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        room.MaxPlayers = byte.Parse(RoomPerson.text);

        //���� ���� ���θ� �����մϴ�.
        room.IsOpen = true;

        // �κ񿡼� �� ����� �����ų �� �����մϴ�.
        room.IsVisible = true;

        //���� �����ϴ� �Լ��Դϴ�.
        PhotonNetwork.CreateRoom(RoomName.text, room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    //�� ������ �Ϸ�� �� ȣ��Ǵ� �ݹ� �Լ��Դϴ�.
    public override void OnCreatedRoom()
    {
        Debug.Log("���� �����Ǿ����ϴ�.");
    }

    public void AllDeleteRoom()
    {
        //Transform ������Ʈ�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ������ �õ��մϴ�.
        foreach (Transform trans in RoomContent)
        {
            //Transform�� ������ �ִ� ���� ������Ʈ�� �����մϴ�.
            Destroy(trans.gameObject);
        }
    }

    //�뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ��Դϴ�.
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void RoomCreateObject()
    {
        //RoomCatalog�� ���� ���� Value���� �� �ִٸ� RoomInfo�� �־��ݴϴ�.
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            //���� �����մϴ�.
            GameObject room = Instantiate(RoomPrefab);

            //RoomContent�� ���� ������Ʈ�� �����մϴ�.
            room.transform.SetParent(RoomContent);

            //�� ������ �Է��մϴ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    void UpdateRoom(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            //�ش� �̸��� RoomCatalog�� Key ������ �����Ǿ� �ִٸ�
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                //RemovedFromList : �뿡�� ������ �Ǿ��� ��
                if (roomList[i].RemovedFromList)
                {
                    //��ųʸ��� �ִ� �����͸� �����մϴ�.
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            //�׷��� ������ roomInfo�� RoomCatalog�� �߰��մϴ�.
            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    //������ ������ �������� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //��Ʈ��ũ ������ �������� �� return �ڵ� ��ȣ�� �̿��ؼ� ������ �����մϴ�.
        //���� �������� �ʾ��� �� ȣ���մϴ�.
        Debug.Log($"JoinRoom Failed {returnCode} : {message}");
    }

    //�ش� �κ� �� ����� ���� ������ �ִٸ� ȣ��Ǵ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        RoomCreateObject();
    }
}
