using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    //�޼����� ǥ���ϴ� UI
    public Text errorMessage;

    private static GameObject prefab;

    //PupUpManager�� �������� ������ �� �ִ� �Լ�
    public static PopUpManager Show(string message)
    {
        if (prefab == null)
        {
            //prefab�� ���ٸ� prefab�� �ҷ��´�
            prefab = (GameObject)Resources.Load("Error PopUp");
        }

        GameObject obj = Instantiate(prefab);
        PopUpManager errorUI = obj.GetComponent<PopUpManager>();
        errorUI.UpdateContent(message);

        return errorUI;
    }

    //PopUp�� ������ �����ϴ� �Լ�
    public void UpdateContent(string message)
    {
        errorMessage.text = message;
    }

    //PopUp�� �ݴ� �Լ�
    public void CanclePopUp()
    {
        Destroy(gameObject);
    }
}