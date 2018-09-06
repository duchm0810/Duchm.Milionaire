using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroGiupKhanGia : MonoBehaviour {

    public Slider sliderA, sliderB, sliderC, sliderD;
    public Text RateTextA, RateTextB, RateTextC, RateTextD;
    public bool isStart;
    Quiz quiz;
    int rate1, rate2, rate3, rate4;
    int dem;
    bool done1, done2, done3, done4;


	// Use this for initialization
	public void Start () {
        
        InitGameObject();
        
        print(rate1 + "/" + rate2 + "/" + rate3 + "/" + rate4 + "/" + (rate1+rate2+rate3+rate4));
	}
	
	public void VeBieuDo()
    {
        dem = 0;
        rate1 = Random.Range(49, 62);
        rate2 = Random.Range(15, (101 - rate1));
        rate3 = Random.Range(0, (101 - rate1 - rate2));
        rate4 = 100 - rate1 - rate2 - rate3;
        sliderA.value = sliderB.value = sliderC.value = sliderD.value = 0;
        SetRate();
    }
    public void resetValue()
    {
        sliderA.value = sliderB.value = sliderC.value = sliderD.value = 0;
    }
    void SetRate()
    {
        

        switch (quiz.DapAn)
        {
            case 1: StartCoroutine(showPerCentA()); break;
            case 2: StartCoroutine(showPerCentB()); break;
            case 3: StartCoroutine(showPerCentC()); break;
            case 4: StartCoroutine(showPerCentD()); break;
        }
    }
    
    IEnumerator showPerCentA()
    {
        yield return new WaitForSeconds(0.05f);
        if (sliderA.value < rate1)
        {
            sliderA.value++;
            if (sliderB.value < rate2)
                sliderB.value++;
            if (sliderC.value < rate3)
                sliderC.value++;
            if (sliderD.value < rate4)
                sliderD.value++;
            RateTextA.text = sliderA.value + "%";
            RateTextB.text = sliderB.value + "%";
            RateTextC.text = sliderC.value + "%";
            RateTextD.text = sliderD.value + "%";
            StartCoroutine(showPerCentA());
        }
        
           

    }
    IEnumerator showPerCentB()
    {
        yield return new WaitForSeconds(0.05f);
        if (sliderB.value < rate1)
        {
            sliderB.value++;
            if (sliderA.value < rate2)
                sliderA.value++;
            if (sliderC.value < rate3)
                sliderC.value++;
            if (sliderD.value < rate4)
                sliderD.value++;
            RateTextA.text = sliderA.value + "%";
            RateTextB.text = sliderB.value + "%";
            RateTextC.text = sliderC.value + "%";
            RateTextD.text = sliderD.value + "%";
            StartCoroutine(showPerCentB());
        }
    }
    IEnumerator showPerCentC()
    {
        yield return new WaitForSeconds(0.05f);
        if (sliderC.value < rate1)
        {
            sliderC.value++;
            if (sliderB.value < rate2)
                sliderB.value++;
            if (sliderA.value < rate3)
                sliderA.value++;
            if (sliderD.value < rate4)
                sliderD.value++;
            RateTextA.text = sliderA.value + "%";
            RateTextB.text = sliderB.value + "%";
            RateTextC.text = sliderC.value + "%";
            RateTextD.text = sliderD.value + "%";
            StartCoroutine(showPerCentC());
        }
    }
    IEnumerator showPerCentD()
    {
        yield return new WaitForSeconds(0.05f);
        if (sliderD.value < rate1)
        {
            sliderD.value++;
            if (sliderB.value < rate2)
                sliderB.value++;
            if (sliderC.value < rate3)
                sliderC.value++;
            if (sliderA.value < rate4)
                sliderA.value++;
            RateTextA.text = sliderA.value + "%";
            RateTextB.text = sliderB.value + "%";
            RateTextC.text = sliderC.value + "%";
            RateTextD.text = sliderD.value + "%";
            StartCoroutine(showPerCentD());
        }
    }

    void InitGameObject()
    {
        quiz = GameObject.Find("Quiz").GetComponent<Quiz>();
        if(sliderA == null)
        {
            sliderA = GameObject.Find("RateA").GetComponent<Slider>();
        }
        if (sliderB == null)
        {
            sliderB = GameObject.Find("RateB").GetComponent<Slider>();
        }
        if (sliderC == null)
        {
            sliderC = GameObject.Find("RateC").GetComponent<Slider>();
        }
        if (sliderD == null)
        {
            sliderD = GameObject.Find("RateD").GetComponent<Slider>();
        }
        if(RateTextA == null)
        {
            RateTextA = GameObject.Find("RateTextA").GetComponent<Text>();
        }
        if (RateTextB == null)
        {
            RateTextB = GameObject.Find("RateTextB").GetComponent<Text>();
        }
        if (RateTextC == null)
        {
            RateTextC = GameObject.Find("RateTextC").GetComponent<Text>();
        }
        if (RateTextD == null)
        {
            RateTextD = GameObject.Find("RateTextD").GetComponent<Text>();
        }
    }
}
