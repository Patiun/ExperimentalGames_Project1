using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour {

    public bool debug;
    public GameObject debugPanel;
    public Text debugText;
    public PlayerController pc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            debug = !debug;
            debugPanel.SetActive(debug);
            if (debug)
            {
                debugText.text = "DEBUG: Debug Mode Enabled";
            }
            else
            {
                pc.immortal = false;
            }
        }
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                pc.immortal = !pc.immortal;
                debugText.text = "DEBUG: Player immortality = " + pc.immortal;
            }
        }
	}
}
