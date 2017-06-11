using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

    public GameObject testObj;
    public GameObject testObj2;
    public Image image;
    public Image image2;

    // Use this for initialization
    void Start () {
        image.transform.position = Camera.main.WorldToScreenPoint(testObj.transform.position);
        image2.transform.position = Camera.main.WorldToScreenPoint(testObj.transform.position);

        image.transform.position = Camera.main.WorldToScreenPoint(testObj2.transform.position);
        image2.transform.position = Camera.main.WorldToScreenPoint(testObj2.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
