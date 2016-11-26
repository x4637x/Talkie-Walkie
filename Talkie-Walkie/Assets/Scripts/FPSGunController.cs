using UnityEngine;
using System.Collections;

public class FPSGunController : MonoBehaviour {
    public GunSetting Gun;
    float effectRange = 200f;
    float TimeBetweenShots = 0.5f;
    int HitableMask;

    private LineRenderer ShotLine;
    private Light MuzzleFlashlight;
    private ParticleSystem MuzzleFlash;
    private AudioSource ShotAudio;
    float ShotTimer;
    float effectsDisplayTime = 0.1f;
    Ray BulletRay;
    RaycastHit BulletHit;
    
	// Use this for initialization
	void Awake () {
        HitableMask = LayerMask.GetMask("Hitable");

        ShotLine = this.GetComponent<LineRenderer>();
        MuzzleFlashlight = this.GetComponent<Light>();
        MuzzleFlash = this.GetComponent<ParticleSystem>();
        ShotAudio = this.GetComponent<AudioSource>();

        effectRange = Gun.effectRange;
        TimeBetweenShots = Gun.FireRate;
	}
	
	// Update is called once per frame
	void Update () {



        if (ShotTimer <= 10f) { ShotTimer += Time.deltaTime; }
        
	    if (Input.GetButtonUp("Fire1") && ShotTimer>= TimeBetweenShots && Time.timeScale !=0 && Gun.AmmoUsed())
        {
            FireShot();
        }

        if (ShotTimer>= TimeBetweenShots * effectsDisplayTime) { DisableEffects(); }

        if (Input.GetButtonUp("Reload")) { Gun.ReloadClip(); }


	}

    void DisableEffects()
    {
        ShotLine.enabled = false;
        MuzzleFlashlight.enabled = false;
    }

    void FireShot()
    {
        ShotTimer = 0f;
        ShotAudio.Play();
        MuzzleFlashlight.enabled = true;
        MuzzleFlash.Stop();
        MuzzleFlash.Play();

        ShotLine.enabled = true;
        ShotLine.SetPosition(0, transform.position);

        BulletRay.origin = transform.position;
        BulletRay.direction = transform.forward;

        if(Physics.Raycast(BulletRay,out BulletHit, effectRange, HitableMask))
        {
            //do something to the hitted staff here
            ShotLine.SetPosition(1, BulletHit.point);
        }
        else { ShotLine.SetPosition(1, BulletRay.origin + BulletRay.direction * effectRange); }
    }
}
