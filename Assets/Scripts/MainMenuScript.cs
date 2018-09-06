using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;

public class MainMenuScript : MonoBehaviour {

    

    public void btnPlay()
    {
        Application.LoadLevel(1);
    }
    public void btnLeaderboad()
    {
        //GooglePlayService.GPS.showLeaderboardUI();
    }
    public void btnDanhGia()
    {
        Application.OpenURL("market://details?id=com.shark.milionaire.berich");
    }
    public void btnQuit()
    {
        Application.Quit();
    }
}
