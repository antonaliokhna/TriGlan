using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPanelContent : MonoBehaviour
{
    public GameObject PanelContentPrefab;
    private List<GameObject> instantiateObjects;

    void Start()
    {
        instantiateObjects = new List<GameObject>();
    }

    public void CreateContentPrefab(Dictionary<string, int> NameAndCountValues)
    {
        foreach (var item in NameAndCountValues)
        {
            var ItenPanelContent = Instantiate(PanelContentPrefab, this.transform);
            ItenPanelContent.GetComponent<MatchPanelItem>().SetVisibleItemValue(item.Key, item.Value);
            instantiateObjects.Add(ItenPanelContent);
        }
    }

    public void ClearInstantiateObjects()
    {
        if (instantiateObjects.Count > 0)
        {
            foreach (var item in instantiateObjects)
                Destroy(item);
            instantiateObjects.Clear();
        }
    }


}
