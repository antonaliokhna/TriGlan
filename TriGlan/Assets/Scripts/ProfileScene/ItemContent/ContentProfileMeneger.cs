using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContentProfileMeneger : MonoBehaviour
{
    private ProfileInfoScenMeneger profileInfoScenMeneger;

    public Dictionary<int, ItemInfoMatch> ItemProfileContent = new Dictionary<int, ItemInfoMatch>();
    private List<GameObject> InstansiteGameObjects = new List<GameObject>();

    public GameObject ItemContentPrefub;
    public GameObject[] VisibleLineSort;

    public GameObject[] someButtonGameobject;


    //private void buttonCallBack(Text text, string value)
    //{
    //    foreach (var item in someButtonGameobject)
    //        item.GetComponentInChildren<Text>().color = new Color(0f, 1f, 1f, 0.5f);
        
    //    text.color = new Color(0f, 1f, 1f, 1f);
    //}

    public void CreateContent(ProfileInfoScenMeneger meneger, string ValuesString)
    {
        profileInfoScenMeneger = meneger;
        CreateMatchesTable(ValuesString);

        //foreach (var item in someButtonGameobject)
          //  item.GetComponent<Button>().onClick.AddListener(() => buttonCallBack(item.GetComponentInChildren<Text>(), item.name.ToString()));
    }

    private void CreateMatchesTable(string valuesString)
    {
        string[] StrMasValues = valuesString.Split('/');

        for (int i = 0; i < StrMasValues.GetLength(0) - 1; i++)
        {
            var values = StrMasValues[i].Split(' ');

            ItemProfileContent.Add(i, new ItemInfoMatch
                (
                    Convert.ToInt32(values[0].ToString()),
                    i + 1,
                    Convert.ToInt32(values[1].ToString()),
                    Convert.ToDateTime(values[2].ToString()),
                    values[3].Remove(values[3].Length - 3, 3).ToString(),
                    Convert.ToInt32(values[4].ToString()),
                    Convert.ToInt32(values[5].ToString()),
                    Convert.ToInt32(values[6].ToString()),
                    Convert.ToInt32(values[7].ToString())
                ));
        }
        InstansiteObjects();

        //OrderByDescending();
    }

    public void OrderByDescending(string value)
    {
        foreach (var item in InstansiteGameObjects)
            Destroy(item);

        InstansiteGameObjects.Clear();

        //VisibleLineSort[0].transform.GetComponent<Animator>().SetTrigger("12");

        switch (value)
        {
            case "MatchID":
                {
                    ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.VisibleMatchID).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Floor":
                {
                    ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Floor).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Date":
                {
                    ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Date).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Length":
                {
                    //ItemProfileContent = ItemProfileContent.OrderByDescending(x => TimeSpan.FromSeconds(x.Value.Length)).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Coins":
                {
                    ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Coins).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Kills":
                {
                    ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Date).ToDictionary(k => k.Key, v => v.Value);
                    break;
                }
            case "Weapons":
                {
                    break;
                }
            case "Boosts":
                {
                    break;
                }
        }

        ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Floor).ToDictionary(k => k.Key, v => v.Value);


        //ItemProfileContent = ItemProfileContent.OrderByDescending(x => x.Value.Floor).ToDictionary(k => k.Key, v => v.Value);
        InstansiteObjects();
    }

    private void InstansiteObjects()
    {
        foreach (var item in ItemProfileContent)
        {
            var ItemContent = Instantiate(ItemContentPrefub, this.transform);
            ItemContent.GetComponent<ItemContent>().SetVisibleValuesItem(item.Value, this);
            InstansiteGameObjects.Add(ItemContent);
        }
    }

    public void SentMatchByID(int MatchID)
    {
        profileInfoScenMeneger.OpenMoreMatchInfo(ItemProfileContent.Values.Where(x => x.VisibleMatchID == MatchID).First());
    }
}
