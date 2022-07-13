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
        //같은 버전의 유저들끼리의 접속을 허용
        //같은 버전끼리의 접속만 허용
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원 가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원 가입 실패");
    }

    public void SignUp()
    {
        //PlayFab에 User를 등록하기 위한 클레스
        var request = new RegisterPlayFabUserRequest {Email = email.text, Password = password.text, Username = username.text};

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request, // 회원가입에 대한 정보
            SignUpSuccess, //회원 가입이 성공했을  떄
            SignUpFailure // 회원가입이 실패했을 떄
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest {Email = email.text, Password = password.text};

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }
}
