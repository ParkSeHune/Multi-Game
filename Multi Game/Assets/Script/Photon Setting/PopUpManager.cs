using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    //메세지를 표시하는 UI
    public Text errorMessage;

    private static GameObject prefab;

    //PupUpManager로 전역에서 접근할 수 있는 함수
    public static PopUpManager Show(string message)
    {
        if (prefab == null)
        {
            //prefab이 없다면 prefab을 불러온다
            prefab = (GameObject)Resources.Load("Error PopUp");
        }

        GameObject obj = Instantiate(prefab);
        PopUpManager errorUI = obj.GetComponent<PopUpManager>();
        errorUI.UpdateContent(message);

        return errorUI;
    }

    //PopUp의 내용을 갱신하는 함수
    public void UpdateContent(string message)
    {
        errorMessage.text = message;
    }

    //PopUp을 닫는 함수
    public void CanclePopUp()
    {
        Destroy(gameObject);
    }
}