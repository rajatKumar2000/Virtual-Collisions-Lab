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

    private Rigidbody rbCartOne;
    private Rigidbody rbCartTwo;

    public float stopDrag = 500;
    public float normalDrag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        cartOne = Instantiate(cartPrefab) as GameObject;
        rbCartOne = cartOne.GetComponent<Rigidbody>();
        Renderer rend1 = cartOne.GetComponent<Renderer>();
        rend1.material = cartOneMat;

        cartTwo = Instantiate(cartPrefab) as GameObject;
        rbCartTwo = cartTwo.GetComponent<Rigidbody>();
        Renderer rend2 = cartTwo.GetComponent<Renderer>();
        rend2.material = cartTwoMat;

        setStaticParameters();
    }

    public void btnStart_ClickEvent() 
    {
        btnStart.interactable = false;
        txtSet.text = "Reset";
        applyForce();
    }

    public void btnSet_ClickEvent()
    {
        btnStart.interactable = true;
        txtSet.text = "Set";
        setStaticParameters();
    }

    private void setStaticParameters() 
    {
        float x_coord_1 = float.Parse(txtPosition1.text) -24.5f;
        float x_coord_2 = float.Parse(txtPosition2.text) - 24.5f;
        float mass_1 = float.Parse(txtMass1.text);
        float mass_2 = float.Parse(txtMass2.text);

        cartOne.transform.position = new Vector3(x_coord_1, 1, 0); //1.7f
        cartTwo.transform.position = new Vector3(x_coord_2, 1, 0);

        cartOne.transform.localEulerAngles = new Vector3(0, 0, 0);
        cartTwo.transform.localEulerAngles = new Vector3(0, 0, 0);

        rbCartOne.drag = stopDrag;
        rbCartTwo.drag = stopDrag;

        rbCartOne.mass = mass_1;
        rbCartTwo.mass = mass_2;

        rbCartOne.velocity = new Vector3(0,0,0);
        rbCartTwo.velocity = new Vector3(0,0,0);

        rbCartOne.angularVelocity = new Vector3(0, 0, 0);
        rbCartTwo.angularVelocity = new Vector3(0, 0, 0);
    }

    private void applyForce() 
    {
        float force_1 = float.Parse(txtForce1.text);
        float force_2 = float.Parse(txtForce2.text);

        rbCartOne.drag = normalDrag;
        rbCartTwo.drag = normalDrag;

        rbCartOne.AddForce(Vector3.right * force_1);
        rbCartTwo.AddForce(Vector3.right * force_2);

    }
}
