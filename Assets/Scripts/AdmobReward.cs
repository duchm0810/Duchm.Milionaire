using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class AdmobReward : MonoBehaviour
{
    public Image rewardAdsImg;
    public GameObject btnEmpty;
    public static AdmobReward admob;
    private RewardBasedVideoAd rewardBasedVideo;
    
    int adsN = 0;

    private void Awake()
    {
        if (admob == null)
        {
            admob = this;
        }
    }

    public void Start()
    {
        //InitGameObject();
        this.RequestRewardBasedVideo();
        StartCoroutine(checkLoadAdsReward());
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "";
        //string adUnitId = "";

#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4074527365839019/7990016338";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

        btnEmpty.gameObject.SetActive(false);
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    public void ShowRewardVideoX1()
    {
        btnEmpty = GameManager.gm.XButtonTuVan;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    public void ShowRewardVideoX2()
    {
        btnEmpty = GameManager.gm.XButtonKhanGia;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    public void ShowRewardVideoX3()
    {
        btnEmpty = GameManager.gm.XButton5050;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    public void ShowRewardVideoX4()
    {
        btnEmpty = GameManager.gm.XButtonGoiDien;
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    IEnumerator checkLoadAdsReward()
    {
        yield return new WaitForEndOfFrame();
        if (rewardBasedVideo.IsLoaded())
        {
            rewardAdsImg.color = Color.green;
        }
        else
        {
            rewardAdsImg.color = Color.red;
        }
        StartCoroutine(checkLoadAdsReward());
    }

}