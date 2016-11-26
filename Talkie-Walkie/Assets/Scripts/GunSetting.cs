using UnityEngine;
using System.Collections;

public class GunSetting : MonoBehaviour {
    public int AmmoClipMax = 5;
    public int currAmmo;
    public float FireRate = 0.5f;
    public float effectRange = 100f;

    // Use this for initialization
    void Awake () {
        ReloadClip();
	}
	
    public void ReloadClip()
    {
        currAmmo = AmmoClipMax;
    }

    public bool AmmoUsed()
    {
        if (currAmmo > 0)
        {
            currAmmo --;
            return true;
        }
        else { return false; }
    }

}
