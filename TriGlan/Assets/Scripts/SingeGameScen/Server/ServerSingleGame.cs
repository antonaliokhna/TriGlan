using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerSingleGame : MonoBehaviour
{
    public Dictionary<int, string[]> BotsDictionaty = new Dictionary<int, string[]>();
    public Dictionary<int, string[]> WeaponsDictionaty = new Dictionary<int, string[]>();
    public Dictionary<int, string[]> BoostsDictionaty = new Dictionary<int, string[]>();

    public InterfaceManager InterfaceManager;

    void Start()
    {
        InterfaceManager = GameObject.FindGameObjectWithTag("InterfaceManager").GetComponent<InterfaceManager>();
        StartCoroutine(GetBotsDictionary());
        StartCoroutine(GetWeapons());
        StartCoroutine(GetBoosts());
        StartCoroutine(CteateMatch());
    }

    public IEnumerator CteateMatch()
    {
        string url = "https://superscript-record.000webhostapp.com/Server/CreateMatch.php";

        WWWForm form = new WWWForm();
        form.AddField("UserID", ServerMenu.UserId.ToString());
        WWW w = new WWW(url, form);
        yield return w;

        ServerMenu.MatchID = Convert.ToInt32(w.text);
    }

    public IEnumerator UpdateMatchInfo(int MatchID, int LvlNow, int CountGetAllCoins, string time)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/UpdateMatch.php";
        WWWForm form = new WWWForm();

        form.AddField("MatchID", MatchID.ToString());
        form.AddField("MaxLevel", LvlNow.ToString());   
        form.AddField("CountGetCoins", CountGetAllCoins.ToString());
        form.AddField("MathTime", time);

        WWW w = new WWW(url, form);
        yield return w;
    }


    public IEnumerator CreateMatchWeapon(int WeaponId)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/CreateMatchWeapon.php";
        WWWForm form = new WWWForm();

        form.AddField("MatchID", ServerMenu.MatchID);
        form.AddField("WeaponID", WeaponId);

        WWW w = new WWW(url, form);

        yield return w;
    }

    public IEnumerator CreateMatchBoost(int BoostID)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/CreateMatchBoost.php";
        WWWForm form = new WWWForm();

        form.AddField("MatchID", ServerMenu.MatchID);
        form.AddField("BoostID", BoostID);

        WWW w = new WWW(url, form);

        yield return w;
    }

    public IEnumerator CreateMatchKill(int BotID)
    {
        string url = "https://superscript-record.000webhostapp.com/Server/CreateMatchKill.php";
        WWWForm form = new WWWForm();

        form.AddField("MatchID", ServerMenu.MatchID);
        form.AddField("BotID", BotID);

        WWW w = new WWW(url, form);
        yield return w;
    }

    public IEnumerator GetBoosts()
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetBoosts.php";
        WWWForm form = new WWWForm();

        WWW w = new WWW(url, form);

        yield return w;

        string[] str = w.text.Split('/');

        for (int i = 0; i < str.GetLength(0); i++)
            BoostsDictionaty.Add(i, str[i].Split(' '));
    }

    public IEnumerator GetWeapons()
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetWeapons.php";
        WWWForm form = new WWWForm();

        WWW w = new WWW(url, form);

        yield return w;

        string[] str = w.text.Split('/');

        for (int i = 0; i < str.GetLength(0); i++) 
            WeaponsDictionaty.Add(i, str[i].Split(' '));
    }


    public IEnumerator GetBotsDictionary()
    {
        string url = "https://superscript-record.000webhostapp.com/Server/GetBots.php";
        WWWForm form = new WWWForm();
        WWW w = new WWW(url, form);

        while (!w.isDone)
            yield return new WaitForSeconds(0.01f);

        yield return w;

        string[] strBot = w.text.Split('/');

        BotsDictionaty = new Dictionary<int, string[]>();

        for (int i = 0; i < strBot.GetLength(0); i++)
            BotsDictionaty.Add(i, strBot[i].Split(' '));
    }
}
