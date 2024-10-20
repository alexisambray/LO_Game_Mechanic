using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : MonoBehaviour
{
    public MixtureStat mixtureStat;
    private string mixtureKey;
    public int index = -1;
    // Start is called before the first frame update
    private void Start()
    {
        //if manual index, hardcode it as such
        if (index >= 0 && index <= 17)
        {
            mixtureStat = new MixtureStat(index);
            mixtureKey = mixtureStat.GenerateKey();
        }
        else
        {
            mixtureStat = new MixtureStat();
            mixtureKey = mixtureStat.GenerateKey();
        }
        

        if (MixtureStat.sharedStatsDict.ContainsKey(mixtureKey))
        {
            MixtureStat.SharedStats sharedStats = MixtureStat.sharedStatsDict[mixtureKey];
            Debug.Log($"{gameObject.name} - ItemFound: {sharedStats.itemFound}");
            Debug.Log($"{gameObject.name} - AppearanceFound: {sharedStats.appearanceFound}");
            Debug.Log($"{gameObject.name} - UseFound: {sharedStats.useFound}");
        }

        
    }

    public MixtureStat GetMixtureStat()
    {
        return mixtureStat;
    }
}
