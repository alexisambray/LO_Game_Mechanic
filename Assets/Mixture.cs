using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : MonoBehaviour
{
    private MixtureStat mixtureStat;
    // Start is called before the first frame update
    void Start()
    {
        mixtureStat = new MixtureStat();


        // Log initial stats
        Debug.Log($"{gameObject.name} - ItemFound: {mixtureStat.ItemFound}");
        Debug.Log($"{gameObject.name} - AppearanceFound: {mixtureStat.AppearanceFound}");
        Debug.Log($"{gameObject.name} - UseFound: {mixtureStat.UseFound}");
    }

    public MixtureStat GetMixtureStat()
    {
        return mixtureStat;
    }
}
