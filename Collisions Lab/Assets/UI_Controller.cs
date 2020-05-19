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
     
    public TMP_Text lblInitialVelocity1;
    public TMP_Text lblFinalVelocity1;

    public TMP_Text lblInitialVelocity2;
    public TMP_Text lblFinalVelocity2;

    public GameObject cartPrefab;
    private GameObject cartOne;
    private GameObject cartTwo;

    public Material cartOneMat;
    public Material cartTwoMat;

    private Rigidbody rbCartOne;
    private Rigidbody rbCartTwo;

    private BoxCollider box1;
    private BoxCollider box2;

    private CartController cartOneController;
    private CartController cartTwoController;

    private float force_1;
    private float force_2;

    public Toggle inelasticToggle, elasticToggle;

    // Start is called before the first frame update
    void Start()
    {
        cartOne = Instantiate(cartPrefab) as GameObject;
        rbCartOne = cartOne.GetComponent<Rigidbody>();
        box1 = cartOne.GetComponent<BoxCollider>();
        cartOneController = cartOne.GetComponent<CartController>();
        Renderer rend1 = cartOne.GetComponent<Renderer>();
        rend1.material = cartOneMat;

        cartTwo = Instantiate(cartPrefab) as GameObject;
        rbCartTwo = cartTwo.GetComponent<Rigidbody>();
        box2 = cartTwo.GetComponent<BoxCollider>();
        cartTwoController = cartTwo.GetComponent<CartController>();
        Renderer rend2 = cartTwo.GetComponent<Renderer>();
        rend2.material = cartTwoMat;

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
        resetCollisionData();
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

    private void resetCollisionData() 
    {
        lblInitialVelocity1.text = "N/A";
        lblFinalVelocity1.text = "N/A";

        lblInitialVelocity2.text = "N/A";
        lblFinalVelocity2.text = "N/A";
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

    private void FixedUpdate()
    {
        if (CartController.collisionFlag)
        {
            lblInitialVelocity1.text = cartOneController.initialVelocity.ToString();
            lblFinalVelocity1.text = cartOneController.finalVelocity.ToString();

            lblInitialVelocity2.text = cartTwoController.initialVelocity.ToString();
            lblFinalVelocity2.text = cartTwoController.finalVelocity.ToString();

            CartController.collisionFlag = false;
        }
    }
}
