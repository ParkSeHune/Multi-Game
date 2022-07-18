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

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    // 로비에 접속했는지 확인하는 함수
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

    //로비에 접속했을 때
    public override void OnJoinedLobby()
    {
        // 씬 동기화를 맞추기 위해서
        //일반적인 LoadLevel은 동기화가 되지 않음
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
