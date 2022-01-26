using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsLoader : MonoBehaviour
{
    public static bool IsLoaded = false;
    void Start()
    {
        if (!IsLoaded)
        {
            Debug.Log("init Ad");
            InterstitialAd.S.LoadAd();
            RewardedAds.S.LoadAd();
            IsLoaded = true;
        }
        
    }

 
}
