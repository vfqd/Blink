﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class FlashlightScript : MonoBehaviour {

    [SerializeField] private GameObject flashlight;
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPC;
    [SerializeField] private float rotation_speed;
    [SerializeField] private float minsOfPower;

    Vector3 normalRot = new Vector3(0, 357, 0);
    Vector3 runRot = new Vector3(30, 357, 0);
    float fadeValue, currentIntensity;
    bool flickering, resetting;
    int flickerCount = 30, totalFlickerCount = 30;

    void Start()
    {
        fadeValue = flashlight.GetComponent<Light>().intensity/(minsOfPower*60);
        flickering = false;
        resetting = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("resetting = " + resetting + " | flickering = "+flickering);

        //F to turn on and off
        if (Input.GetKeyDown(KeyCode.F) && !flickering)
        {
            flashlight.gameObject.SetActive(!flashlight.activeInHierarchy);
            if (resetting)
            {
                resetting = false;
                flickering = false;
                flashlight.GetComponent<Light>().intensity = currentIntensity;
            }
        }

        //Aim flashlight down if running
        if (!FPC.m_IsWalking)
        {
            FPC.m_UseHeadBob = true;
            flashlight.transform.localRotation = Quaternion.Lerp(flashlight.transform.localRotation, Quaternion.Euler(runRot), rotation_speed);
        } else
        {
            FPC.m_UseHeadBob = false;
            flashlight.transform.localRotation = Quaternion.Lerp(flashlight.transform.localRotation, Quaternion.Euler(normalRot), rotation_speed);
        }

        //Flashlight Flickering
        if (flickering && flickerCount > 0)
        {
            flashlight.GetComponent<Light>().intensity = Random.Range(0f, currentIntensity);
            flickerCount--;
        }

        if (flickerCount <= 0 && flickering)
        {
            flashlight.GetComponent<Light>().intensity = 0f;
            resetting = true;
            flickering = false;
            flickerCount = totalFlickerCount;
            flashlight.gameObject.SetActive(false);
        }

        //Flashlight Dying
        if (flashlight.activeInHierarchy && !flickering && !resetting)
        {
            flashlight.GetComponent<Light>().intensity = Mathf.Lerp(flashlight.GetComponent<Light>().intensity, 0f, fadeValue * Time.deltaTime);
        }
    }

    //Chance to flicker in FixedUpdate for randomization
    void FixedUpdate()
    {
        float flickerNow = Random.Range(0, 4000);
        if (flickerNow > 3998 && !flickering && !resetting && flashlight.GetComponent<Light>().intensity < 2)
        {
            flickering = true;
            currentIntensity = flashlight.GetComponent<Light>().intensity;
        }
    }
}
