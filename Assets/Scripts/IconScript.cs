using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour {

    public GameObject Image1, Image2, BG1, BG2, BG3, BG4, BG5, BG6;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Image1.transform.Rotate(Vector3.forward * 90 * Time.deltaTime);
        Image2.transform.Rotate(Vector3.back * 90 * Time.deltaTime);
        BG2.transform.Rotate(Vector3.back * 30 * Time.deltaTime);
        BG3.transform.Rotate(Vector3.forward * 0 * Time.deltaTime);
        BG4.transform.Rotate(Vector3.forward * 20 * Time.deltaTime);
        BG5.transform.Rotate(Vector3.back * 10 * Time.deltaTime);

    }
}
