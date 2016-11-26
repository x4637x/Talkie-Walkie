using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSPlayerController : MonoBehaviour {
    public GameObject EquipHand;
    public Text EquipModeText;

    int ControlMode;
    int PickUpMask;
    Ray castRay;
    RaycastHit castHit;
    float _pickUpRange = 40f;

    // Use this for initialization
    void Awake () {
        SetControlMode();
        PickUpMask = LayerMask.GetMask("Pickable");
    }
	
	// Update is called once per frame
	void Update () {
        //Update UI here
        EquipModeText.text = "Mode = " + ControlMode;
	}

    void FixedUpdate()
    {
        if (ControlMode == 0)
        {
            if (Input.GetButtonUp("PickUp"))
            {
                PickUpEquipment();
            }
        }
        else if (ControlMode > 0)
        {
            if (Input.GetButtonUp("DropDown"))
            {
                DropEquipment();
            }
        }
    }

    void SetControlMode()
    {
        if(EquipHand.transform.childCount != 0)
        {
            if (EquipHand.transform.GetChild(0).tag == "Gun")
            {
                EquipHand.transform.GetChild(0).GetComponentInChildren<FPSGunController>().enabled = true;
                ControlMode = 1;
            }
        }else
        {
            ControlMode = 0;
        }
    }

    void PickUpEquipment()
    {
        castRay.origin = EquipHand.transform.position;
        castRay.direction = EquipHand.transform.right;
        if(Physics.Raycast(castRay, out castHit, _pickUpRange, PickUpMask) && castHit.transform.tag == "Gun")
        {
            //pick equiqment up
            castHit.transform.SetParent(EquipHand.transform);
            castHit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            castHit.transform.localPosition = new Vector3(0f, 0f, 0f);
            castHit.transform.localRotation = Quaternion.Euler(Vector3.zero);
            SetControlMode();
        }
        else { Debug.Log("nothing happen."); }
    }

    void DropEquipment()
    {
        EquipHand.GetComponentInChildren<Rigidbody>().isKinematic = false;
        EquipHand.GetComponentInChildren<FPSGunController>().enabled = false;
        EquipHand.transform.DetachChildren();
        SetControlMode();
    }
}
