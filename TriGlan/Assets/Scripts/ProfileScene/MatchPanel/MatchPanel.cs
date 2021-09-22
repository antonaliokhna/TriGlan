using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : MonoBehaviour
{
    public GameObject backroundMatchPanel;

    public GameObject killsContent;
    private MatchPanelContent killsPanelContent;

    public GameObject weaponsContent;
    private MatchPanelContent weaponsPanelContent;

    public GameObject boostsContent;
    private MatchPanelContent boostsPanelContent;


    public Text MatchIdText;
    public Text FloorText;
    public Text DateText;
    public Text LengthText;
    public Text CoinsText;

    public GameObject[] VisibleAnimationLines;
    private float DeltaTimeAnimation = 0f;

    private Animator myAnimator;


    void Start()
    {
        killsPanelContent = killsContent.GetComponent<MatchPanelContent>();
        weaponsPanelContent = weaponsContent.GetComponent<MatchPanelContent>();
        boostsPanelContent = boostsContent.GetComponent<MatchPanelContent>();
        myAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VisibleLineAnimation();
    }

    private void VisibleLineAnimation()
    {
        if (DeltaTimeAnimation >= 0.5f)
        {
            if (VisibleAnimationLines[0].gameObject.activeSelf)
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
        foreach (var line in VisibleAnimationLines)
            line.gameObject.SetActive(val);
    }

    public void OpenMatchPanelAndSetValues(ItemInfoMatch match)
    {
        SetValueActivePanel(true);
        MatchIdText.text = match.VisibleMatchID.ToString();
        FloorText.text = match.Floor.ToString();
        LengthText.text = match.Length;
        DateText.text = match.Date.ToString();
        CoinsText.text = match.Coins.ToString();
    }

    public void SetContentValues(Dictionary<string, int> kills, Dictionary<string, int> weapons, Dictionary<string, int> boosts)
    {
        killsPanelContent.CreateContentPrefab(kills);
        weaponsPanelContent.CreateContentPrefab(weapons);
        boostsPanelContent.CreateContentPrefab(boosts);
    }

    public void SetValueActivePanel(bool value)
    {
        backroundMatchPanel.SetActive(value);
        if (!value)
        {
            myAnimator.SetTrigger("HidePanel");
            killsPanelContent.ClearInstantiateObjects();
            weaponsPanelContent.ClearInstantiateObjects();
            boostsPanelContent.ClearInstantiateObjects();
        }
        else 
            myAnimator.SetTrigger("OpenPanel");
    }

}
