using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public Button btnStart;
    public Button btnSet;
    public TextMeshProUGUI txtSet;

    public TextMeshProUGUI txtMass1;
    public TextMeshProUGUI txtForce1;
    public TextMeshProUGUI txtPosition1;

    public TextMeshProUGUI txtMass2;
    public TextMeshProUGUI txtForce2;
    public TextMeshProUGUI txtPosition2;

    public GameObject cartPrefab;
    private GameObject cartOne;
    private GameObject cartTwo;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cartOne = Instantiate(cartPrefab) as GameObject;
       // setParameters();
    }

    public void btnStart_ClickEvent() 
    {
        btnStart.interactable = false;
        txtSet.text = "Reset";
    }

    public void btnSet_ClickEvent()
    {
        btnStart.interactable = true;
        txtSet.text = "Set";
       // setParameters();
    }

    private void setParameters() 
    {
        Debug.Log("bro: " + txtPosition1.text);
        double x_coord = Convert.ToDouble("0");
        Debug.Log("" + x_coord);
    }
}
