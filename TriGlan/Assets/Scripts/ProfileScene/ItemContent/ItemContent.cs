using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContent : MonoBehaviour
{
    private ContentProfileMeneger contentProfileMeneger;

    public Text MatchIDText;
    public Text FloorText;
    public Text DateText;
    public Text LengthText;
    public Text CoinsText;
    public Text KillsText;
    public Text WeaponsText;
    public Text BoostsText;
    public int localID;

    public void SetVisibleValuesItem(ItemInfoMatch itemProfileContent, ContentProfileMeneger profileMeneger)
    {
        localID = itemProfileContent.VisibleMatchID;
        MatchIDText.text = itemProfileContent.VisibleMatchID.ToString();
        FloorText.text = itemProfileContent.Floor.ToString();
        DateText.text = itemProfileContent.Date.ToString();
        LengthText.text = itemProfileContent.Length.ToString();
        CoinsText.text = itemProfileContent.Coins.ToString();
        KillsText.text = itemProfileContent.Kills.ToString();
        WeaponsText.text = itemProfileContent.Weapons.ToString();
        BoostsText.text = itemProfileContent.Boosts.ToString();
        contentProfileMeneger = profileMeneger;
    }
    
    public void SentMatchID()
    {
        contentProfileMeneger.SentMatchByID(this.localID);
    }
}
