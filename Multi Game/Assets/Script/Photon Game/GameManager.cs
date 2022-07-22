using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    void Start()
    {
        PhotonNetwork.Instantiate("Character", new Vector3(0, 1, 0), Quaternion.identity);
    }
}
