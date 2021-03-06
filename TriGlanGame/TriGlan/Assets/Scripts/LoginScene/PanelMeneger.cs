using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMeneger : MonoBehaviour
{
    public int numberPanel;
    private ServerMenu serverMenu;

    public InputField[] inputFields;
    public Text TextLog;
    public GameObject LoadingImage;

    // Start is called before the first frame update
    void Start()
    {
        serverMenu = GameObject.FindGameObjectWithTag("ServerMenu").GetComponent<ServerMenu>();
    }

    public bool CheckInternetConnection()
    {
        bool internet = (!(Application.internetReachability == NetworkReachability.NotReachable) ||
              (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork) ||
              (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork));

        if (!internet)
            TextLog.text = "Error, check internet connection!";

        return internet;
    }

    public void OnClickLogInButton()
    {
        if (CheckInternetConnection())
            StartCoroutine(serverMenu.Login(this.inputFields, this.TextLog, this.LoadingImage));
    }

    public void OnClickRegisterButton()
    {
        if (CheckInternetConnection())
            StartCoroutine(serverMenu.Register(this.inputFields, this.TextLog, this.LoadingImage));
    }
    public void OnClickChangePassworddButton()
    {
        if (CheckInternetConnection())
            StartCoroutine(serverMenu.ChangePassword(this.inputFields, this.TextLog, this.LoadingImage));
    }

    public void OnClickFindQuestionButton()
    {
        if (CheckInternetConnection())
            StartCoroutine(serverMenu.FindCodeQuestion(this.inputFields, this.TextLog, this.LoadingImage));
    }

    public void SpawPanelMeneger(int panelShow)
    {
        serverMenu.SwapPannel(panelShow, this.numberPanel);
    }
      
}
