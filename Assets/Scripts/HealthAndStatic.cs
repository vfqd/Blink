﻿using UnityEngine;
using System.Collections;

public class HealthAndStatic : MonoBehaviour {

    public float health = 100f;
    private float startingHealth;
    [SerializeField] private float healthDecayRate = 5f;
    [SerializeField] private Renderer staticRenderer;
    [SerializeField] private GameObject monster, deathScreen;

    bool playerHasLost = false;

    // Use this for initialization
    void Start () {
        startingHealth = health;
        staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  0f);
    }
	
    void Update()
    {
        //Simple check at the moment, will be improved later
        if (!playerHasLost)
        {
            if (Vector3.Distance(monster.transform.position, this.transform.position) < 15 && monster.GetComponent<Renderer>().isVisible)
            {
                DecreaseHealth();
            }
            else
            {
                IncreaseHealth();
            }
        }
        OffsetTexture();
    }

    void IncreaseHealth()
    {
        float growthModifier = startingHealth / healthDecayRate;
        health += growthModifier * Time.deltaTime;

        float newAlpha = 1f - (health / startingHealth);
        staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  newAlpha);

        if (health >= startingHealth)
        {
            health = startingHealth;
        }
    }

	void DecreaseHealth () {
        float decayModifier = startingHealth / healthDecayRate;

        health -= decayModifier * Time.deltaTime;

        float newAlpha = 1f - (health / startingHealth);
        staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  newAlpha);

        if (health <= 0)
        {
            Debug.Log("Player out of health");
            playerHasLost = true;
            staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  0.65f);
            deathScreen.SetActive(true);
            //LOSE CONDITION
        }
	}

    void OffsetTexture()
    {
        float xOffset = Random.value;
        float yOffset = Random.value;

        staticRenderer.material.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
