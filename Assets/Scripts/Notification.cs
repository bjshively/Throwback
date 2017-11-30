using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{


    private Text notifyText;
    private GameObject background;
    private Canvas canvas;


    // Use this for initialization
    void Start()
    {
        canvas = transform.parent.gameObject.GetComponent<Canvas>();
        canvas.sortingLayerName = "UI";
        notifyText = GetComponent<Text>();
        background = GameObject.Find("notifybackground");
        background.SetActive(false);
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void clearMessage()
    {
        background.SetActive(false);
        notifyText.text = "";
    }

    public void show(string t)
    {
        notifyText.text = t;
        background.SetActive(true);
    }
}
