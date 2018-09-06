using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButtonScript : MonoBehaviour {

    private string[] tAnh = { "PLAY","RANK","RATE","EXIT" };
    private string[] tArap = { "لعب", "مرتبة", "معدل", "خروج" };
    private string[] tDuc = { "SPIELEN", "RANG", "BEWERTUNG", "AUSFAHRT" };
    private string[] tNga = { "ИГРАТЬ", "РАНГ", "СТАВКА", "ВЫХОД" };
    private string[] tPhap = { "JOUER", "RANG", "TAUX", "SORTIE" };
    private string[] tTayBanNha = { "JUGAR", "RANGO", "TARIFA", "SALIDA" };
    private string[] tItalia = { "GIOCARE", "RANGO", "VOTA", "PARTENZA" };
    private string[] tViet = { "CHƠI NGAY", "BẢNG XẾP HẠNG", "ĐÁNH GIÁ", "THOÁT" };

    public GameObject LanguagePanel;
    public Text PlayButtonText;
    public Text RankButtonText;
    public Text OptionButtonText;
    public Text ExitButtonText;



    private void Awake()
    {
        //print(PlayerPrefs.GetInt("DMMMM"));
        //print(tAnh[0]);
        InitGameObject();
        LanguagePanel.SetActive(false);
        if(PlayerPrefs.GetInt("FirstOpen")==0 || PlayerPrefs.GetInt("FirstOpen") == null)
        {
            LanguagePanel.SetActive(true);

        }
        else
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0: setButtonText(tAnh); break;
                case 1: setButtonText(tArap); break;
                case 2: setButtonText(tDuc); break;
                case 3: setButtonText(tNga); break;
                case 4: setButtonText(tPhap); break;
                case 5: setButtonText(tTayBanNha); break;
                case 6: setButtonText(tItalia); break;
                case 7: setButtonText(tViet); break;
                default: setButtonText(tAnh);break;
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void InitGameObject()
    {
        if (LanguagePanel == null)
        {
            LanguagePanel = GameObject.Find("LanguagePanel");
        }
        if (PlayButtonText == null)
        {
            PlayButtonText = GameObject.Find("PlayButtonText").GetComponent<Text>();
        }
        if (RankButtonText == null)
        {
            RankButtonText = GameObject.Find("RankButtonText").GetComponent<Text>();
        }
        if (OptionButtonText == null)
        {
            OptionButtonText = GameObject.Find("RateButtonText").GetComponent<Text>();
        }
        if (ExitButtonText == null)
        {
            ExitButtonText = GameObject.Find("ExitButtonText").GetComponent<Text>();
        }
    }
    void setButtonText(string[] language)
    {
        PlayButtonText.text = language[0];
        RankButtonText.text = language[1];
        OptionButtonText.text = language[2];
        ExitButtonText.text = language[3];
    }
    public void setLanguage(int language)
    {
        PlayerPrefs.SetInt("Language", language);
        PlayerPrefs.SetInt("FirstOpen", 1);
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0: setButtonText(tAnh); break;
            case 1: setButtonText(tArap); break;
            case 2: setButtonText(tDuc); break;
            case 3: setButtonText(tNga); break;
            case 4: setButtonText(tPhap); break;
            case 5: setButtonText(tTayBanNha); break;
            case 6: setButtonText(tItalia); break;
            case 7: setButtonText(tViet); break;
            default: setButtonText(tAnh); break;
        }
        LanguagePanel.SetActive(false);
    }
    public void languageButton()
    {
        LanguagePanel.SetActive(true);
    }
}
