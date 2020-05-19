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

    private float force_1;
    private float force_2;

    public Toggle inelasticToggle, elasticToggle;

    private BoxCollider box1;
    private BoxCollider box2;

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

        box1 = cartOne.GetComponent<BoxCollider>();
        box2 = cartTwo.GetComponent<BoxCollider>();

        inelasticToggle.isOn = true;  

        setStaticParameters();
    }

    public void btnStart_ClickEvent() 
    {
        btnStart.interactable = false;
        txtSet.text = "Reset";

        if (inelasticToggle.isOn)
        {
            box1.material.bounciness = 0;
            box2.material.bounciness = 0;
        }
        else
        {
            box1.material.bounciness = 1;
            box2.material.bounciness = 1;
        }

        applyForce();
    }

    public void btnSet_ClickEvent()
    {
        btnStart.interactable = true;
        txtSet.text = "Set";
        //rbCartOne.detectCollisions = true;
        setStaticParameters();
    }

    private void setStaticParameters() 
    {
        float x_coord_1 = float.Parse(txtPosition1.text) -24.5f;
        float x_coord_2 = float.Parse(txtPosition2.text) - 24.5f;
        float mass_1 = float.Parse(txtMass1.text);
        float mass_2 = float.Parse(txtMass2.text);

        cartOne.transform.position = new Vector3(x_coord_1, 1, 0); //1.7f for cartV2 object
        cartTwo.transform.position = new Vector3(x_coord_2, 1, 0);

        cartOne.transform.localEulerAngles = new Vector3(0, 0, 0);
        cartTwo.transform.localEulerAngles = new Vector3(0, 0, 0);

        rbCartOne.mass = mass_1;
        rbCartTwo.mass = mass_2;

        rbCartOne.velocity = new Vector3(0,0,0);
        rbCartTwo.velocity = new Vector3(0,0,0);

        rbCartOne.angularVelocity = new Vector3(0, 0, 0);
        rbCartTwo.angularVelocity = new Vector3(0, 0, 0);
    }

    private void applyForce() 
    {
        force_1 = float.Parse(txtForce1.text);
        force_2 = float.Parse(txtForce2.text);

        rbCartOne.AddForce(force_1,0,0, ForceMode.Impulse);
        rbCartTwo.AddForce(force_2, 0, 0, ForceMode.Impulse);
    }

    public void setInelasticCheck(bool inelastic)
    {
        if (elasticToggle.isOn == inelastic)
            elasticToggle.isOn = !inelastic;
    }

    public void setElasticCheck(bool elastic)
    {
        if (inelasticToggle.isOn == elastic)
            inelasticToggle.isOn = !elastic;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (inelasticToggle.isOn)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.anchor = collision.contacts[0].point;
            joint.connectedBody = collision.contacts[1].otherCollider.transform.GetComponentInParent<Rigidbody>();
            joint.enableCollision = false;
            //rbCartOne.detectCollisions = false;
        }
    }
}
