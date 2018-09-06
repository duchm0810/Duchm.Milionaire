using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdmobIntersititials : MonoBehaviour {

    int adsN = 0;
    public static AdmobIntersititials admob;
    InterstitialAd interstitial;
    private void Awake()
    {
        if(admob == null)
        {
            admob = this;
        }
    }

    private void Start()
    {
        this.RequestInterstitial();
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "";
#elif UNITY_IPHONE
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void HandleOnAdLeavingApplication(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    this.RequestInterstitial();
    }

    public void HandleOnAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    public void showAds()
    {
        if(adsN >= 1)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                adsN = 0;
            }
            else
            {
                adsN++;
            }
        }
        else
        {
            adsN++;
        }
    }
}

