using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager sm;

    public AudioClip backgroundMusic1, backgroundMusic2, music5050, musicCorrect, musicIncorrect,musicVictories,musicDefeat;
    public AudioSource audio;

    private void Awake()
    {
        if(sm == null)
        {
            sm = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        Invoke("playBackGround", 6);
	}
	public void playBackGround()
    {
        audio.Play();
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
