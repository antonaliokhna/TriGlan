using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ProfileInfoScenMeneger : MonoBehaviour
{
    private ServerProfileInfo serverProfileInfo;
    private ContentProfileMeneger contentProfileMeneger;
    private ProfileTotalInfo profileTotalInfo;

    public MatchPanel profileMatchPanel;

    void Start()
    {
        serverProfileInfo = GameObject.FindGameObjectWithTag("Server").GetComponent<ServerProfileInfo>();
        contentProfileMeneger = GameObject.FindGameObjectWithTag("Content").GetComponent<ContentProfileMeneger>();
        profileTotalInfo = GameObject.FindGameObjectWithTag("ProfileTotalContent").GetComponent<ProfileTotalInfo>();
        GetMatchesAndProfileInfo();
    }
    private void GetMatchesAndProfileInfo()
    {
        StartCoroutine(serverProfileInfo.GetLastMatchesInfo((callbackMatches) =>
        {
            contentProfileMeneger.CreateContent(this, callbackMatches);
            StartCoroutine(serverProfileInfo.GetUserInfoByUserID((callbackUserInfo) =>
            {
                profileTotalInfo.SetTotalProfileValues(contentProfileMeneger.ItemProfileContent, callbackUserInfo[0], callbackUserInfo[1]);
            }));

        }));
    }

    public void OpenMoreMatchInfo(ItemInfoMatch match)
    {
        profileMatchPanel.OpenMatchPanelAndSetValues(match);

        StartCoroutine(serverProfileInfo.GetMoreInfoByMatchID(match.MatchID, (callbackUserInfo) =>
        {
            profileMatchPanel.SetContentValues(IDontKnowVoid(callbackUserInfo[0].Split('/')),
                IDontKnowVoid(callbackUserInfo[1].Split('/')), IDontKnowVoid(callbackUserInfo[2].Split('/')));
        }));
    }

    private Dictionary<string, int> IDontKnowVoid(string[] CallbackTypeValue)
    {
        Dictionary<string, int> valuesDictionary = new Dictionary<string, int>();

        for (int i = 0; i < CallbackTypeValue.GetLength(0) - 1; i++)
        {
            string[] bot = CallbackTypeValue[i].Split(' ');
            valuesDictionary.Add(bot[0], Convert.ToInt32(bot[1]));
        }
        return valuesDictionary;
    }
}
