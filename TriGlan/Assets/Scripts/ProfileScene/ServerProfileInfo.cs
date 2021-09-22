using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerProfileInfo : MonoBehaviour
{
    public IEnumerator GetLastMatchesInfo(Action<string> callback)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetLastMatchesInfo.php";
        WWWForm form = new WWWForm();

        form.AddField("UserID", ServerMenu.UserId);

        WWW w = new WWW(url, form);
        yield return w;

        callback(w.text);
    }

    public IEnumerator GetUserInfoByUserID(Action<string[]> callback)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetUserInfoByID.php";
        WWWForm form = new WWWForm();

        form.AddField("UserID", ServerMenu.UserId);

        WWW w = new WWW(url, form);
        yield return w;

        callback(w.text.Split(' '));
    }

    public IEnumerator GetMoreInfoByMatchID(int matchID, Action<string[]> callback)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetMoreInfoByMatchID.php";
        WWWForm form = new WWWForm();

        form.AddField("MatchID", matchID);

        WWW w = new WWW(url, form);
        yield return w;

        callback(w.text.Split('|'));
    }
}