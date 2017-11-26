using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{


    private Text notifyText;

    // Use this for initialization
    void Start()
    {
	
        notifyText = GetComponent<Text>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void clearMessage()
    {
        notifyText.text = "";
    }

    public void show(string t, int delay)
    {
        notifyText.text = t;
        Invoke("clearMessage", delay);
    }
}
