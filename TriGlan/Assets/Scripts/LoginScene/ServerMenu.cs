using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerMenu : MonoBehaviour
{
    // 1 - log in, 2 - Register, 3 - Forgot Password.
    public GameObject[] Panels;

    public static int UserId;
    public static int MatchID;

    public IEnumerator Register(InputField[] inputFields, Text textLog, GameObject rotationImage)
    {
        rotationImage.SetActive(true);
        string url = "https://superscript-record.000webhostapp.com/Server/RegisterUsers.php";
        WWWForm form = new WWWForm();

        form.AddField("Login", inputFields[0].text);
        form.AddField("Password", inputFields[1].text);
        form.AddField("CodeQuestion", inputFields[2].text);
        form.AddField("CodeWord", inputFields[3].text);

        WWW w = new WWW(url, form);
        StartCoroutine(WaitForWWWReqiused(w, textLog, rotationImage));

        yield return w;
        textLog.text = w.text;
    }

    public IEnumerator FindCodeQuestion(InputField[] inputFields, Text textLog, GameObject rotationImage)
    {
        rotationImage.SetActive(true);
        string url = "https://superscript-record.000webhostapp.com/Server/FindCodeQuestion.php";
        WWWForm form = new WWWForm();

        form.AddField("Login", inputFields[0].text);

        WWW w = new WWW(url, form);
        StartCoroutine(WaitForWWWReqiused(w, textLog, rotationImage));

        yield return w;

        if (!w.text.Contains("Error"))
        {
            textLog.text = "Code question found successfully";
            inputFields[1].text = w.text;
        }
        else
            textLog.text = w.text;
    }

    public IEnumerator ChangePassword(InputField[] inputFields, Text textLog, GameObject rotationImage)
    {
        rotationImage.SetActive(true);
        string url = "https://superscript-record.000webhostapp.com/Server/ChangePassword.php";
        WWWForm form = new WWWForm();

        form.AddField("Login", inputFields[0].text);
        form.AddField("CodeWord", inputFields[2].text);
        form.AddField("Password", inputFields[3].text);

        WWW w = new WWW(url, form);
        StartCoroutine(WaitForWWWReqiused(w, textLog, rotationImage));

        yield return w;
        textLog.text = w.text;
    }


    public IEnumerator Login(InputField[] inputFields, Text textLog, GameObject rotationImage)
    {
        rotationImage.SetActive(true);
        string url = "https://superscript-record.000webhostapp.com/Server/LoginUser.php";
        WWWForm form = new WWWForm();

        form.AddField("Login", inputFields[0].text);
        form.AddField("Password", inputFields[1].text);
        WWW w = new WWW(url, form);

        StartCoroutine(WaitForWWWReqiused(w, textLog, rotationImage));
        yield return w;

        if (!w.text.Contains("Error"))
        {
            UserId = Convert.ToInt32(w.text);
            SceneManager.LoadScene(1);
        }
        else
            textLog.text = w.text;

        if (w.text.Length <= 0)
            textLog.text = "Check internet connection";
    }

    IEnumerator WaitForWWWReqiused(WWW w, Text textLog, GameObject obj)
    {
        float timeLastLoading = 0;

        textLog.text = "Loading";
        while (!w.isDone)
        {
            timeLastLoading += Time.deltaTime;
            if (timeLastLoading > 0.15f)
            {
                timeLastLoading = 0f;
                textLog.text += ".";
                if (textLog.text.Length >= 10)
                    textLog.text = "Loading";
            }
            obj.transform.Rotate(0, 0, 12.5f);
            yield return new WaitForSeconds(0.01f);
        }

        obj.SetActive(false);
        w.Dispose();
    }


    public IEnumerator CheckInternetConnection(Text textLog, Action<bool> action)
    {
        WWW w = new WWW("http://google.com");
        yield return w;
        if (w.error != null)
        {
            textLog.text = "Error: Check internet connection!";
            action(false);
        }
        else
            action(true);
    }


    void Start()
    {
        Panels[0].GetComponent<Animator>().SetTrigger("OpenPanel");
    }


    public void SwapPannel(int panelShow, int panelHide)
    {
        // 1 - log in, 2 - Register, 3 - Forgot Password.
        Panels[panelShow - 1].GetComponent<Animator>().SetTrigger("OpenPanel");
        Panels[panelHide - 1].GetComponent<Animator>().SetTrigger("HidePanel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
