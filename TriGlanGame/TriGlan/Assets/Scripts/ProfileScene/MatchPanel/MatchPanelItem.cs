using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanelItem : MonoBehaviour
{
    public Text TextValue;
    
    public void SetVisibleItemValue(string name, int count)
    {
        if (count > 1)
            this.TextValue.text = name + " x" + count.ToString();
        else
            this.TextValue.text = name;
    }
}
