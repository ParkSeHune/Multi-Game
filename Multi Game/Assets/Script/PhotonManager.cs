using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentRegion;
    public Text currentLobby;

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        currentRegion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch (Data.count)
        {
            case 0: currentLobby.text = "First Lobby";
                break;
            case 1: currentLobby.text = "Second Lobby";
                break;
            case 2: currentLobby.text = "Third Lobby";
                break;
        }
    }

    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    // �κ� �����ߴ��� Ȯ���ϴ� �Լ�
    public override void OnConnectedToMaster()
    {
        switch (Data.count)
        {
            case 0: PhotonNetwork.JoinLobby(new TypedLobby("Lobby 1", LobbyType.Default));
                break;
            case 1:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 2", LobbyType.Default));
                break;
            case 2:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 3", LobbyType.Default));
                break;
        }
    }

    //�κ� �������� ��
    public override void OnJoinedLobby()
    {
        // �� ����ȭ�� ���߱� ���ؼ�
        //�Ϲ����� LoadLevel�� ����ȭ�� ���� ����
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
