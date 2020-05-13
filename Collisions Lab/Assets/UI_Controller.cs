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

    public TMP_InputField txtMass1;
    public TMP_InputField txtForce1;
    public TMP_InputField txtPosition1;

    public TMP_InputField txtMass2;
    public TMP_InputField txtForce2;
    public TMP_InputField txtPosition2;

    public GameObject cartPrefab;
    private GameObject cartOne;
    private GameObject cartTwo;

    public Material cartOneMat;
    public Material cartTwoMat;

    // Start is called before the first frame update
    void Start()
    {
        cartOne = Instantiate(cartPrefab) as GameObject;
        Renderer rend1 = cartOne.GetComponent<Renderer>();
        rend1.material = cartOneMat;

        cartTwo = Instantiate(cartPrefab) as GameObject;
        Renderer rend2 = cartTwo.GetComponent<Renderer>();
        rend2.material = cartTwoMat;
        
        setParameters();
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
        setParameters();
    }

    private void setParameters() 
    {
        float x_coord_1 = float.Parse(txtPosition1.text) -24.5f;
        float x_coord_2 = float.Parse(txtPosition2.text) - 24.5f;

        cartOne.transform.position = new Vector3(x_coord_1, 1,0);
        cartTwo.transform.position = new Vector3(x_coord_2, 1, 0);
    }
}
