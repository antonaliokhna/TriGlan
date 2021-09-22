using System;
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
   

    public void OnClickLogInButton()
    {
        StartCoroutine(serverMenu.CheckInternetConnection(this.TextLog, (isConnected) => {
            if (isConnected)
                StartCoroutine(serverMenu.Login(this.inputFields, this.TextLog, this.LoadingImage));
        }));
    }

    public void OnClickRegisterButton()
    {
        StartCoroutine(serverMenu.CheckInternetConnection(this.TextLog, (isConnected) => {
            if (isConnected)
                StartCoroutine(serverMenu.Register(this.inputFields, this.TextLog, this.LoadingImage));
        }));
    }
    public void OnClickChangePassworddButton()
    {
        StartCoroutine(serverMenu.CheckInternetConnection(this.TextLog, (isConnected) => {
            if (isConnected)
                StartCoroutine(serverMenu.ChangePassword(this.inputFields, this.TextLog, this.LoadingImage));
        }));
    }

    public void OnClickFindQuestionButton()
    {
        StartCoroutine(serverMenu.CheckInternetConnection(this.TextLog, (isConnected) => {
            if (isConnected)
                StartCoroutine(serverMenu.FindCodeQuestion(this.inputFields, this.TextLog, this.LoadingImage));
        }));
    }

    public void SpawPanelMeneger(int panelShow)
    {
        serverMenu.SwapPannel(panelShow, this.numberPanel);
    }

}
