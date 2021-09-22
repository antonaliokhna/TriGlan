using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProfileTotalInfo : MonoBehaviour
{
    public Text LoginText;
    public Text DateRegText;
    public Text CountMatchText;
    public Text MaxFloorText;
    public Text LenghtText;
    public Text CoinsText;
    public Text KillsText;
    public Text WeaponsText;
    public Text BoostsText;

    public GameObject UserHasNoMatches;

    public GameObject[] VisibleLine;

    private float DeltaTimeAnimation = 0f;

    public void SetTotalProfileValues(Dictionary<int, ItemInfoMatch> DictionaryValues, string login, string dateReg)
    {
        LoginText.text = login;
        DateRegText.text = dateReg;

        if (DictionaryValues.Count > 0)
        {
            MaxFloorText.text = DictionaryValues.Values.Max(x => x.Floor).ToString();
            CoinsText.text = DictionaryValues.Values.Sum(x => x.Coins).ToString();
            KillsText.text = DictionaryValues.Values.Sum(x => x.Kills).ToString();
            WeaponsText.text = DictionaryValues.Values.Sum(x => x.Weapons).ToString();
            BoostsText.text = DictionaryValues.Values.Sum(x => x.Boosts).ToString();
            CoinsText.text = DictionaryValues.Values.Sum(x => x.Coins).ToString();
            CountMatchText.text = DictionaryValues.Values.Count().ToString();
            SumAllTime(DictionaryValues.Values.Select(x => x.Length).ToArray());
        }
        else
            InfoNotAFound();
    }

    private void InfoNotAFound()
    {
        UserHasNoMatches.SetActive(true);
        MaxFloorText.text = "0";
        CoinsText.text = "0";
        KillsText.text = "0";
        WeaponsText.text = "0";
        BoostsText.text = "0";
        CoinsText.text = "0";
        CountMatchText.text = "0";
        LenghtText.text = "00:00:00";
    }



    private void SumAllTime(string[] AllValuesTime)
    {
        LenghtText.text = (TimeSpan.FromMinutes(AllValuesTime.Sum(x => Convert.ToInt32(x.Substring(0, 2))))
            + TimeSpan.FromSeconds(AllValuesTime.Sum(x => Convert.ToInt32(x.Substring(x.Length - 2))))).ToString();
    }

    void Update()
    {
        VisibleLineAnimation();
    }

    private void VisibleLineAnimation()
    {
        if (DeltaTimeAnimation >= 0.5f)
        {
            if (VisibleLine[0].gameObject.activeSelf)
                SetBoolValueLines(false);
            else
                SetBoolValueLines(true);

            DeltaTimeAnimation = 0f;
        }
        else
            DeltaTimeAnimation += Time.deltaTime;
    }

    private void SetBoolValueLines(bool val)
    {
        foreach (var line in VisibleLine)
            line.gameObject.SetActive(val);
    }

}
