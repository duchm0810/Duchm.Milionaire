using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScale : MonoBehaviour {

    void Start()
    {
        var worlHeight = Camera.main.orthographicSize * 2;
        var worlWidth = worlHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3(worlWidth, worlHeight, 0);

    }
}
