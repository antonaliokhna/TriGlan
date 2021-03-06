using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoMatch : MonoBehaviour
{
    public int MatchID;
    public int VisibleMatchID;
    public int Floor;
    public DateTime Date;
    public string Length;
    public int Coins;
    public int Kills;
    public int Weapons;
    public int Boosts;

    public ItemInfoMatch(int matchID, int visibleMatchID, int floor, DateTime date, string length, int coins, int kills, int weapons, int boosts)
    {
        MatchID = matchID;
        VisibleMatchID = visibleMatchID;
        Floor = floor;
        Date = date;
        Length = length;
        Coins = coins;
        Kills = kills;
        Weapons = weapons;
        Boosts = boosts;
    }
}
