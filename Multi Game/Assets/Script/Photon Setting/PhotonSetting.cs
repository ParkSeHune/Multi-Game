using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    public InputField email;
    public InputField password;
    public InputField username;
    public InputField region;

    public void LoginSuccess(LoginResult result)
    {
        //���� ������ �����鳢���� ������ ���
        //���� ���������� ���Ӹ� ���
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUpManager.Show("�α��ο� �����ϼ̽��ϴ�. ���̵�� ��й�ȣ�� ��Ȯ���� Ȯ���� �ֽʽÿ�.");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUpFailure(PlayFabError error)
    {

        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUp()
    {
        //PlayFab�� User�� ����ϱ� ���� Ŭ����
        var request = new RegisterPlayFabUserRequest {Email = email.text, Password = password.text, Username = username.text};

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request, // ȸ�����Կ� ���� ����
            SignUpSuccess, //ȸ�� ������ ��������  ��
            SignUpFailure // ȸ�������� �������� ��
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest {Email = email.text, Password = password.text};

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }
}