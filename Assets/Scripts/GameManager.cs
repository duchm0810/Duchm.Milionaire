using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public bool isAwswer;
    public Quiz currentQuiz;
    public Text CauHoiText, AText, BText, CText, DText, QuizLevelText;
    public Text personA, personB, personC, goiDienText;
    public GameObject buttonA, buttonB, buttonC, buttonD;
    public GameObject PanelKhanGia, PanelTuVan, PanelGoiDien,GameOverPanel,QuizPanel,LosePanel,WinPanel;
    public GameObject XButtonKhanGia, XButtonTuVan, XButtonGoiDien, XButton5050;
    public Text quitText, yesText, noText;
    public Text playAgainText, yesText2, noText2;
    public Text winText,winContinue,winExit;
    public Text TimeText;
    DataService dataService;

    private string[] answerLanguage = { "My answer is ", " اجابتي هي", "Meine Antwort ist ", "Мой ответ ", "Ma réponse est ", "Mi respuesta es ", "La mia risposta è ", "Câu trả lời của tôi là " };
    private string[] quitLanguage = { "Do you wana quit with ", " هل أنت متأكد تريد إنهاء مع", "Möchtest du wirklich mit ", "Вы действительно хотите уйти со ", "Êtes-vous sûr de vouloir quitter avec ", "¿Estás seguro de querer dejarlo con ", "Sei sicuro di voler uscire con ", "Bạn muốn thoát ra với " };
    private string[] quitLanguage2 = { " points ?", "نقطة؟", " Punkten aufhören?", " очками?", " points?", " puntos?", " punti?", " điểm ?"};
    private string[] playAgainLanguage = { "Do you want to play again ?", "هل تريد اللعب مرة أخرى؟", "Willst du wieder spielen?", "Вы хотите снова сыграть?", "Voulez-vous jouer à nouveau?", "Quieres jugar de nuevo ?", "Vuoi giocare di nuovo ?","Bạn muốn chơi lại chứ ?" };
    private string[] yesString = {"YES", "نعم ", "JA", "ДА", "OUI", "SI", "SI","CÓ" };
    private string[] noString = { "NO", "لا", "NEIN", "НЕТ", "NON", "NO", "NO", "KHÔNG" };
    private string[] quizString = { "Quiz: ", "السؤال رقم1", "Frage: ", "Вопрос: ", "Question: ", "Pregunta: ", "Domanda: ", "Câu hỏi: " };

    private int[] reward = { 0, 200, 400, 600, 800, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };

    private int QuizLevel = 1;
    private int QuizId;
    private int answer;
    private int timeCounter;
    public bool gameStart;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
        print(PlayerPrefs.GetInt("Language"));
        currentQuiz = GameObject.Find("Quiz").GetComponent<Quiz>();
        InitGameObject();
        dataService = new DataService("Ai_la_trieu_phu.db");
        QuizLevel = 1;
    }
    // Use this for initialization
    void Start() {
        //GameOverPanel.GetComponent<Animator>().SetBool("On", false);
        //GameOverPanel.GetComponent<Animator>().SetBool("Off", true);
        QuizPanel.GetComponent<Animator>().SetBool("Off", false);
        QuizPanel.GetComponent<Animator>().SetBool("On", true);
        setQuitText();
        setPlayAgainText();
        XButton5050.SetActive(false);
        XButtonGoiDien.SetActive(false);
        XButtonKhanGia.SetActive(false);
        XButtonTuVan.SetActive(false);
        GetQuiz();
        Invoke("offPanel", 4);

    }

    // Update is called once per frame
    void Update() {
        
    }
    void GetQuiz()
    {
        this.isAwswer = false;
        if (QuizLevel < 6)
        {
            SoundManager.sm.audio.Stop();
            SoundManager.sm.audio.clip = SoundManager.sm.backgroundMusic1;
            SoundManager.sm.audio.Play();
        }
        else
        {
            SoundManager.sm.audio.Stop();
            SoundManager.sm.audio.clip = SoundManager.sm.backgroundMusic2;
            SoundManager.sm.audio.Play();
        }
        if (QuizLevel > 15)
        {
            WinPanel.GetComponent<Animator>().SetBool("Off", false);
            WinPanel.GetComponent<Animator>().SetBool("On", false);
        }
        else
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0: getTAnhQ(); break;
                case 1: getTArapQ(); break;
                case 2: getTDucQ(); break;
                case 3: getTNgaQ(); break;
                case 4: getTPhapQ(); break;
                case 5: getTTayBanNhaQ(); break;
                case 6: getTItaliaQ(); break;
                case 7: getTVietQ(); break;
            }
        }
        if (!gameStart)
        {
            gameStart = true;
            StartCoroutine(timerCounter());
        }
        Invoke("resetTime", 2);
    }
    public void SetAnswer(int Answer)
    {
        this.isAwswer = true;
        if (QuizLevel <= 5)
        {
            this.answer = Answer;
            buttonA.GetComponent<Button>().enabled = false;
            buttonB.GetComponent<Button>().enabled = false;
            buttonC.GetComponent<Button>().enabled = false;
            buttonD.GetComponent<Button>().enabled = false;
            StartCoroutine(CheckQuiz());
        }
        else
        {
            this.answer = Answer;
            buttonA.GetComponent<Button>().enabled = false;
            buttonB.GetComponent<Button>().enabled = false;
            buttonC.GetComponent<Button>().enabled = false;
            buttonD.GetComponent<Button>().enabled = false;
            StartCoroutine(CheckQuiz());
        }

    }
    public void SetColor(GameObject btn)
    {
        btn.GetComponent<Button>().interactable = false;
    }

    IEnumerator CheckQuiz()
    {
        yield return new WaitForSeconds(2);
        if (answer == currentQuiz.DapAn)
        {
            SoundManager.sm.audio.PlayOneShot(SoundManager.sm.musicCorrect);
            switch (answer)
            {
                case 1: buttonA.GetComponent<Animator>().SetBool("Dung", true); break;
                case 2: buttonB.GetComponent<Animator>().SetBool("Dung", true); break;
                case 3: buttonC.GetComponent<Animator>().SetBool("Dung", true); break;
                case 4: buttonD.GetComponent<Animator>().SetBool("Dung", true); break;
            }
            if (QuizLevel <= 14)
            {
                Invoke("onAnimPanel", 3);
                Invoke("offPanel", 8);
            }
            else
            {

            }
        }
        else
        {
            SoundManager.sm.audio.PlayOneShot(SoundManager.sm.musicIncorrect);
            switch (answer)
            {
                case 1: buttonA.GetComponent<Animator>().SetBool("Sai", true); break;
                case 2: buttonB.GetComponent<Animator>().SetBool("Sai", true); break;
                case 3: buttonC.GetComponent<Animator>().SetBool("Sai", true); break;
                case 4: buttonD.GetComponent<Animator>().SetBool("Sai", true); break;
            }
            switch (currentQuiz.DapAn)
            {
                case 1: buttonA.GetComponent<Animator>().SetBool("Dung", true); break;
                case 2: buttonB.GetComponent<Animator>().SetBool("Dung", true); break;
                case 3: buttonC.GetComponent<Animator>().SetBool("Dung", true); break;
                case 4: buttonD.GetComponent<Animator>().SetBool("Dung", true); break;
            }
            Invoke("showLosePanel", 3);
        }
    }
    
    // Gọi Trợ Giúp
    public void KhanGiaButton()
    {
        XButtonKhanGia.SetActive(true);
        PanelKhanGia.GetComponent<Animator>().SetBool("Off", false);
        PanelKhanGia.GetComponent<Animator>().SetBool("On", true);
        Invoke("VeBieuDo", 2f);
    }
    public void tatBieuDo()
    {
        PanelKhanGia.GetComponent<Animator>().SetBool("On", false);
        PanelKhanGia.GetComponent<Animator>().SetBool("Off", true);
        GameObject.Find("KhanGia").GetComponent<TroGiupKhanGia>().resetValue();
    }
    public void tatGoiDien()
    {
        PanelGoiDien.GetComponent<Animator>().SetBool("On", false);
        PanelGoiDien.GetComponent<Animator>().SetBool("Off", true);
    }
    public void btnBack()
    {
        setQuitText();
        GameOverPanel.GetComponent<Animator>().SetBool("Off", false);
        GameOverPanel.GetComponent<Animator>().SetBool("On", true);
    }
    public void btnThoat()
    {
        //GooglePlayService.GPS.addScore(GSResource.leaderboard_millionaire, reward[QuizLevel - 1]);
        Application.LoadLevel(0);
    }
    public void btnOlai()
    {
        Time.timeScale = 1;
        GameOverPanel.GetComponent<Animator>().SetBool("On", false);
        GameOverPanel.GetComponent<Animator>().SetBool("Off", true);
    }
    public void btnPlayAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
        AdmobIntersititials.admob.showAds();
    }
    public void TuVanButton()
    {
        XButtonTuVan.SetActive(true);
        PanelTuVan.GetComponent<Animator>().SetBool("Off", false);
        PanelTuVan.GetComponent<Animator>().SetBool("On", true);
        Invoke("TuVan", 2f);
    }
    public void tatTuVan()
    {
        PanelTuVan.GetComponent<Animator>().SetBool("On", false);
        PanelTuVan.GetComponent<Animator>().SetBool("Off", true);

    }
    public void TroGiup5050()
    {
        XButton5050.SetActive(true);
        Invoke("LoaiBoDapAn", 2f);
    }
    public void GoiDien()
    {
        XButtonGoiDien.SetActive(true);
        PanelGoiDien.GetComponent<Animator>().SetBool("Off", false);
        PanelGoiDien.GetComponent<Animator>().SetBool("On", true);
        Invoke("Goi", 2f);
    }
    public void thoatWinPanel()
    {
        //GooglePlayService.GPS.addScore(GSResource.leaderboard_millionaire, reward[15]);
        Application.LoadLevel(0);
    }
    public void choiTiep()
    {
        //GooglePlayService.GPS.addScore(GSResource.leaderboard_millionaire, reward[15]);
        Application.LoadLevel(Application.loadedLevel);
    }
    void VeBieuDo()
    {
        GameObject.Find("KhanGia").GetComponent<TroGiupKhanGia>().VeBieuDo();
    }
    void TuVan()
    {
        DuDoan(Random.Range(1, 4));
    }
    void DuDoan(int th)
    {
        if (th == 1)
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                personA.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personB.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personC.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
            }
            else
            {
                personA.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
                personB.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
                personC.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
            }
        }
        else if (th == 2)
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                personA.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personB.text = "C " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personC.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
            }
            else
            {
                personA.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
                personB.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " C";
                personC.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
            }
        } else if (th == 3)
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                personA.text = "B " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personB.text = "C " + answerLanguage[PlayerPrefs.GetInt("Language")];
                personC.text = TraDapAn(currentQuiz.DapAn) + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
            }
            else
            {
                personA.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " B";
                personB.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " C";
                personC.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
            }
        }
    }
    string TraDapAn(int answer)
    {
        string awn = "";
        switch (answer)
        {
            case 1: awn = " A"; return awn;
            case 2: awn = " B"; return awn;
            case 3: awn = " C"; return awn;
            case 4: awn = " D"; return awn;
        }
        return awn;

    }
    void LoaiBoDapAn()
    {
        SoundManager.sm.audio.PlayOneShot(SoundManager.sm.music5050);
        int choose1 = Random.Range(1, 5);
        int choose2 = Random.Range(1, 5);

        while (choose1 == currentQuiz.DapAn)
        {
            choose1 = Random.Range(1, 5);
        }
        while (choose2 == currentQuiz.DapAn)
        {
            choose2 = Random.Range(1, 5);
        } while (choose2 == choose1)
        {
            choose2 = Random.Range(1, 5);
        }
        print(choose1 + " / " + choose2);
        switch (choose1)
        {
            case 1: LoaiDapAn(buttonA, AText); break;
            case 2: LoaiDapAn(buttonB, BText); break;
            case 3: LoaiDapAn(buttonC, CText); break;
            case 4: LoaiDapAn(buttonD, DText); break;
        }
        switch (choose2)
        {
            case 1: LoaiDapAn(buttonA, AText); break;
            case 2: LoaiDapAn(buttonB, BText); break;
            case 3: LoaiDapAn(buttonC, CText); break;
            case 4: LoaiDapAn(buttonD, DText); break;
        }
    }
    void LoaiDapAn(GameObject Button, Text text)
    {
        Button.GetComponent<Button>().enabled = false;
        text.text = "";
    }
    void Goi()
    {
        if (PlayerPrefs.GetInt("Language") == 2)
        {
            goiDienText.text = currentQuiz.DapAn + " " + answerLanguage[PlayerPrefs.GetInt("Language")];
        }
        else
        {
            goiDienText.text = answerLanguage[PlayerPrefs.GetInt("Language")] + " " + TraDapAn(currentQuiz.DapAn);
        }
    }
    void offPanel()
    {
        QuizPanel.GetComponent<Animator>().SetBool("On", false);
        QuizPanel.GetComponent<Animator>().SetBool("Off", true);
        buttonA.GetComponent<Button>().enabled = true;
        buttonB.GetComponent<Button>().enabled = true;
        buttonC.GetComponent<Button>().enabled = true;
        buttonD.GetComponent<Button>().enabled = true;
        buttonA.GetComponent<Animator>().SetBool("Dung", false);
        buttonB.GetComponent<Animator>().SetBool("Dung", false);
        buttonC.GetComponent<Animator>().SetBool("Dung", false);
        buttonD.GetComponent<Animator>().SetBool("Dung", false);
        buttonA.GetComponent<Animator>().SetBool("Sai", false);
        buttonB.GetComponent<Animator>().SetBool("Sai", false);
        buttonC.GetComponent<Animator>().SetBool("Sai", false);
        buttonD.GetComponent<Animator>().SetBool("Sai", false);
        buttonA.GetComponent<Button>().interactable = true;
        buttonB.GetComponent<Button>().interactable = true;
        buttonC.GetComponent<Button>().interactable = true;
        buttonD.GetComponent<Button>().interactable = true;

    }
    void onAnimPanel()
    {
        QuizPanel.GetComponent<Animator>().SetBool("Off", false);
        QuizPanel.GetComponent<Animator>().SetBool("On", true);
        QuizLevel++;
        GetQuiz();
    }
    void showLosePanel()
    {
        LosePanel.GetComponent<Animator>().SetBool("Off", false);
        LosePanel.GetComponent<Animator>().SetBool("On", true);
        SoundManager.sm.audio.PlayOneShot(SoundManager.sm.musicDefeat);
        AdmobIntersititials.admob.showAds();
    }
    void showWinPanel()
    {
        WinPanel.GetComponent<Animator>().SetBool("Off", false);
        WinPanel.GetComponent<Animator>().SetBool("On", false);
        winText.text = playAgainLanguage[PlayerPrefs.GetInt("Language")];
        SoundManager.sm.audio.PlayOneShot(SoundManager.sm.musicVictories);
        AdmobIntersititials.admob.showAds();
    }
    void resetTime()
    {
        timeCounter = 32;
    }
    
    IEnumerator timerCounter()
    {
        yield return new WaitForSeconds(1);
        timeCounter--;
        TimeText.text = timeCounter.ToString();
        if (timeCounter == 0 && !this.isAwswer)
        {
            TimeText.text = timeCounter.ToString();
            buttonA.GetComponent<Button>().enabled = false;
            buttonB.GetComponent<Button>().enabled = false;
            buttonC.GetComponent<Button>().enabled = false;
            buttonD.GetComponent<Button>().enabled = false;
            showLosePanel();
        }else if(timeCounter > 0)
        {
            TimeText.text = timeCounter.ToString();
        }
        else
        {
            TimeText.text = "0";
        }
        StartCoroutine(timerCounter());
    }
    //Database câu hỏi
    void getTAnhQ()
    {
        

        switch (QuizLevel)
        {
            case 1:QuizId = Random.Range(1,255); break;
            case 2: QuizId = Random.Range(255,533); break;
            case 3: QuizId = Random.Range(533,773); break;
            case 4: QuizId = Random.Range(773,1238); break;
            case 5: QuizId = Random.Range(1238,1718); break;
            case 6: QuizId = Random.Range(1718,2238); break;
            case 7: QuizId = Random.Range(2238,2742); break;
            case 8: QuizId = Random.Range(2742,3231); break;
            case 9: QuizId = Random.Range(3231,3699); break;
            case 10: QuizId = Random.Range(3699,4085); break;
            case 11: QuizId = Random.Range(4085,4452); break;
            case 12: QuizId = Random.Range(4452,4802); break;
            case 13: QuizId = Random.Range(4802,5052); break;
            case 14: QuizId = Random.Range(5052,5298); break;
            case 15: QuizId = Random.Range(5298,5543); break;
        }

        English quiz = dataService.TiengAnh(QuizId);
        StartCoroutine(ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTArapQ()
    {

        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1,196); break;
            case 2: QuizId = Random.Range(196, 391); break;
            case 3: QuizId = Random.Range(391, 586); break;
            case 4: QuizId = Random.Range(586, 781); break;
            case 5: QuizId = Random.Range(781, 980); break;
            case 6: QuizId = Random.Range(980, 1257); break;
            case 7: QuizId = Random.Range(1257, 1534); break;
            case 8: QuizId = Random.Range(1534, 1811); break;
            case 9: QuizId = Random.Range(1811, 2088); break;
            case 10: QuizId = Random.Range(2088, 2368); break;
            case 11: QuizId = Random.Range(2368, 2590); break;
            case 12: QuizId = Random.Range(2590, 2812); break;
            case 13: QuizId = Random.Range(2812, 3034); break;
            case 14: QuizId = Random.Range(3034, 3256); break;
            case 15: QuizId = Random.Range(3256,3481); break;
        }

        Arap quiz = dataService.Arap(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTDucQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1, 254); break;
            case 2: QuizId = Random.Range(254, 507); break;
            case 3: QuizId = Random.Range(507, 760); break;
            case 4: QuizId = Random.Range(760, 1013); break;
            case 5: QuizId = Random.Range(1013, 1266); break;
            case 6: QuizId = Random.Range(1266, 1519); break;
            case 7: QuizId = Random.Range(1519, 1772); break;
            case 8: QuizId = Random.Range(1772, 2025); break;
            case 9: QuizId = Random.Range(2025, 2277); break;
            case 10: QuizId = Random.Range(2277, 2529); break;
            case 11: QuizId = Random.Range(2529, 2782); break;
            case 12: QuizId = Random.Range(2782, 3033); break;
            case 13: QuizId = Random.Range(3033, 3283); break;
            case 14: QuizId = Random.Range(3283, 3536); break;
            case 15: QuizId = Random.Range(3536,3788); break;
        }
        Duc quiz = dataService.TiengDuc(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTNgaQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1, 967); break;
            case 2: QuizId = Random.Range(967, 1914); break;
            case 3: QuizId = Random.Range(1914, 2820); break;
            case 4: QuizId = Random.Range(2820, 3738); break;
            case 5: QuizId = Random.Range(3738, 4790); break;
            case 6: QuizId = Random.Range(4790, 5841); break;
            case 7: QuizId = Random.Range(5841, 6837); break;
            case 8: QuizId = Random.Range(6837, 7777); break;
            case 9: QuizId = Random.Range(7777, 8669); break;
            case 10: QuizId = Random.Range(8669, 9472); break;
            case 11: QuizId = Random.Range(9472, 10092); break;
            case 12: QuizId = Random.Range(10092, 10618); break;
            case 13: QuizId = Random.Range(10618, 11018); break;
            case 14: QuizId = Random.Range(11018, 11282); break;
            case 15: QuizId = Random.Range(11282,11494); break;
        }
        Nga quiz = dataService.TiengNga(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTPhapQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1, 161); break;
            case 2: QuizId = Random.Range(161, 311); break;
            case 3: QuizId = Random.Range(311, 451); break;
            case 4: QuizId = Random.Range(451, 591); break;
            case 5: QuizId = Random.Range(591, 701); break;
            case 6: QuizId = Random.Range(701, 806); break;
            case 7: QuizId = Random.Range(806, 911); break;
            case 8: QuizId = Random.Range(911, 996); break;
            case 9: QuizId = Random.Range(996, 1081); break;
            case 10: QuizId = Random.Range(1081, 1151); break;
            case 11: QuizId = Random.Range(1151, 1211); break;
            case 12: QuizId = Random.Range(1211, 1261); break;
            case 13: QuizId = Random.Range(1261, 1291); break;
            case 14: QuizId = Random.Range(1291, 1321); break;
            case 15: QuizId = Random.Range(1321,1350); break;
        }
        Phap quiz = dataService.TiengPhap(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTTayBanNhaQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1, 148); break;
            case 2: QuizId = Random.Range(148, 281); break;
            case 3: QuizId = Random.Range(281, 414); break;
            case 4: QuizId = Random.Range(414, 547); break;
            case 5: QuizId = Random.Range(547, 682); break;
            case 6: QuizId = Random.Range(682, 815); break;
            case 7: QuizId = Random.Range(815, 948); break;
            case 8: QuizId = Random.Range(948, 1090); break;
            case 9: QuizId = Random.Range(1090, 1258); break;
            case 10: QuizId = Random.Range(1258, 1400); break;
            case 11: QuizId = Random.Range(1400, 1533); break;
            case 12: QuizId = Random.Range(1533, 1666); break;
            case 13: QuizId = Random.Range(1666, 1799); break;
            case 14: QuizId = Random.Range(1799,1932); break;
            case 15: QuizId = Random.Range(1932,2064); break;
        }
        TayBanNha quiz = dataService.TiengTBN(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTItaliaQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1, 679); break;
            case 2: QuizId = Random.Range(679, 1294); break;
            case 3: QuizId = Random.Range(1294, 1882); break;
            case 4: QuizId = Random.Range(1882, 2438); break;
            case 5: QuizId = Random.Range(2438, 2990); break;
            case 6: QuizId = Random.Range(2990, 3458); break;
            case 7: QuizId = Random.Range(3458, 3948); break;
            case 8: QuizId = Random.Range(3948, 4395); break;
            case 9: QuizId = Random.Range(4395,4821); break;
            case 10: QuizId = Random.Range(4821, 5224); break;
            case 11: QuizId = Random.Range(5224, 5613); break;
            case 12: QuizId = Random.Range(5613, 5942); break;
            case 13: QuizId = Random.Range(5942, 6087); break;
            case 14: QuizId = Random.Range(6087, 6184); break;
            case 15: QuizId = Random.Range(6184,6280); break;
        }
        italia quiz = dataService.TiengY(QuizId);
        StartCoroutine( ShowQuiz(quiz._id, quiz._cauHoi, quiz._cauA, quiz._cauB, quiz._cauC, quiz._cauD, quiz._dapAn));
    }
    void getTVietQ()
    {
        switch (QuizLevel)
        {
            case 1: QuizId = Random.Range(1,413); break;
            case 2: QuizId = Random.Range(413,716); break;
            case 3: QuizId = Random.Range(716,962); break;
            case 4: QuizId = Random.Range(962, 1270); break;
            case 5: QuizId = Random.Range(1270, 1587); break;
            case 6: QuizId = Random.Range(1587, 1969); break;
            case 7: QuizId = Random.Range(1969, 2352); break;
            case 8: QuizId = Random.Range(2352, 2753); break;
            case 9: QuizId = Random.Range(2753, 3135); break;
            case 10: QuizId = Random.Range(3135, 3389); break;
            case 11: QuizId = Random.Range(3389, 3601); break;
            case 12: QuizId = Random.Range(3601, 3787); break;
            case 13: QuizId = Random.Range(3787, 3959); break;
            case 14: QuizId = Random.Range(3959, 4117); break;
            case 15: QuizId = Random.Range(4117,4175); break;
        }
        Question quiz = dataService.TiengViet(QuizId);
        StartCoroutine( ShowQuiz(quiz.id, quiz.question, quiz.casea, quiz.caseb, quiz.casec, quiz.cased, int.Parse(quiz.truecase)));
    }
    void setQuitText()
    {
        if(PlayerPrefs.GetInt("Language") == 1)
        {
            setText(quitLanguage2[PlayerPrefs.GetInt("Language")],reward[QuizLevel-1],quitLanguage[PlayerPrefs.GetInt("Language")],yesString[PlayerPrefs.GetInt("Language")],noString[PlayerPrefs.GetInt("Language")]);
        }
        else
        {
            setText(quitLanguage[PlayerPrefs.GetInt("Language")], reward[QuizLevel-1], quitLanguage2[PlayerPrefs.GetInt("Language")], yesString[PlayerPrefs.GetInt("Language")], noString[PlayerPrefs.GetInt("Language")]);
        }
    }
    void setPlayAgainText()
    {
        setText2(playAgainLanguage[PlayerPrefs.GetInt("Language")], yesString[PlayerPrefs.GetInt("Language")], noString[PlayerPrefs.GetInt("Language")]);
    }
    void setText(string text1,int point,string text2,string textYes, string textNo)
    {
        quitText.text = text1 + point + text2;
        yesText.text = textYes;
        noText.text = textNo;
    }
    void setText2(string text1,string yesText,string noText)
    {
        playAgainText.text = text1;
        yesText2.text = yesText;
        noText2.text = noText;
    }
    IEnumerator ShowQuiz(int id, string cauhoi,string A,string B,string C,string D,int DapAn)
    {
        yield return new WaitForSeconds(3);
        currentQuiz.id = QuizLevel;
        currentQuiz.DapAn = DapAn;
        CauHoiText.text = currentQuiz.CauHoi = cauhoi;
        AText.text = currentQuiz.DapAnA = "A. "+A;
        BText.text = currentQuiz.DapAnB = "B. "+B;
        CText.text = currentQuiz.DapAnC = "C. "+C;
        DText.text = currentQuiz.DapAnD = "D. "+D;
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0: QuizLevelText.text = "Question: " + QuizLevel.ToString("00"); break;
            case 1: QuizLevelText.text = "سؤال: " + QuizLevel.ToString("00"); break;
            case 2: QuizLevelText.text = "Frage: " + QuizLevel.ToString("00"); break;
            case 3: QuizLevelText.text = "Bопрос: " + QuizLevel.ToString("00"); break;
            case 4: QuizLevelText.text = "Question: " + QuizLevel.ToString("00"); break;
            case 5: QuizLevelText.text = "Pregunta: " + QuizLevel.ToString("00"); break;
            case 6: QuizLevelText.text = "Domanda: " + QuizLevel.ToString("00"); break;
            case 7: QuizLevelText.text = "Câu hỏi: " + QuizLevel.ToString("00"); break;
        }
        
    }
    void InitGameObject()
    {
        if (GameOverPanel == null)
        {
            GameOverPanel = GameObject.Find("GameOverPanel");
        }
        if (LosePanel == null)
        {
            LosePanel = GameObject.Find("LosePanel");
        }
        if (WinPanel == null)
        {
            WinPanel = GameObject.Find("WinPanel");
        }
        if (QuizPanel == null)
        {
            QuizPanel = GameObject.Find("QuizPanel");
        }
        if (PanelKhanGia == null)
        {
            PanelKhanGia = GameObject.Find("KhanGia");
        }
        if (PanelTuVan == null)
        {
            PanelTuVan = GameObject.Find("TuVan");
        }
        if (PanelGoiDien == null)
        {
            PanelGoiDien = GameObject.Find("GoiDien");
        }
        if (CauHoiText == null)
        {
            CauHoiText = GameObject.Find("CauHoiText").GetComponent<Text>();
        }
        if (XButton5050 == null)
        {
            XButton5050 = GameObject.Find("XButton5050");
        }
        if (XButtonGoiDien == null)
        {
            XButtonGoiDien = GameObject.Find("XButtonGoiDien");
        }
        if (XButtonKhanGia == null)
        {
            XButtonKhanGia = GameObject.Find("XButtonKhanGia");
        }
        if (XButtonTuVan == null)
        {
            XButtonTuVan = GameObject.Find("XButtonTuVan");
        }
        if (AText == null)
        {
            AText = GameObject.Find("AText").GetComponent<Text>();
        }
        if (BText == null)
        {
            BText = GameObject.Find("BText").GetComponent<Text>();
        }
        if (CText == null)
        {
            CText = GameObject.Find("CText").GetComponent<Text>();
        }
        if (DText == null)
        {
            DText = GameObject.Find("DText").GetComponent<Text>();
        }
        if (buttonA == null)
        {
            buttonA = GameObject.Find("A");
        }
        if (buttonB == null)
        {
            buttonB = GameObject.Find("B");
        }
        if (buttonC == null)
        {
            buttonC = GameObject.Find("C");
        }
        if (buttonD == null)
        {
            buttonD = GameObject.Find("D");
        }
        if (QuizLevelText == null)
        {
            QuizLevelText = GameObject.Find("QuizLevel").GetComponent<Text>();
        }
        if (personA == null)
        {
            personA = GameObject.Find("PersonA").GetComponent<Text>();
        }
        if (personB == null)
        {
            personB = GameObject.Find("PersonB").GetComponent<Text>();
        }
        if (personC == null)
        {
            personC = GameObject.Find("PersonC").GetComponent<Text>();
        }
        if (goiDienText == null)
        {
            goiDienText = GameObject.Find("GoiDienText").GetComponent<Text>();
        }
        if (quitText == null)
        {
            quitText = GameObject.Find("QuitText").GetComponent<Text>();
        }
        if (yesText == null)
        {
            yesText = GameObject.Find("TextYes").GetComponent<Text>();
        }
        if (noText == null)
        {
            noText = GameObject.Find("TextNo").GetComponent<Text>();
        }
        if (yesText2 == null)
        {
            yesText2 = GameObject.Find("TextYes2").GetComponent<Text>();
        }
        if (noText2 == null)
        {
            noText2 = GameObject.Find("TextNo2").GetComponent<Text>();
        }
        if (playAgainText == null)
        {
            playAgainText = GameObject.Find("PlayAgainText").GetComponent<Text>();
        }
        if (winText == null)
        {
            winText = GameObject.Find("WinText").GetComponent<Text>();
        }
        if (winContinue == null)
        {
            winContinue = GameObject.Find("WinContinue").GetComponent<Text>();
        }
        if (winExit == null)
        {
            winExit = GameObject.Find("WinExit").GetComponent<Text>();
        }
        if (TimeText == null)
        {
            TimeText = GameObject.Find("TimeText").GetComponent<Text>();
        }
    }
}
