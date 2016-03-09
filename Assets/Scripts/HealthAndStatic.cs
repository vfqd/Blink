using UnityEngine;
using System.Collections;

public class HealthAndStatic : MonoBehaviour {

    public float health = 100f;
    private float startingHealth;
    [SerializeField] private float healthDecayRate = 5f;
    [SerializeField] private Renderer staticRenderer;
	[SerializeField] private GameObject monster, deathScreen;
	[SerializeField] private AudioClip deathSound;
    public bool playerHasLost = false;
	public sound staticSoundScript;

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

        float newAlpha = 0.8f - (health / startingHealth);
        staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  newAlpha);
		staticSoundScript.audio.volume = newAlpha;

        if (health >= startingHealth)
        {
            health = startingHealth;
        }
    }

	public void DecreaseHealth () {
        float decayModifier = startingHealth / healthDecayRate;

        health -= decayModifier * Time.deltaTime;

        float newAlpha = 0.8f - (health / startingHealth);
        staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  newAlpha);
		staticSoundScript.audio.volume = newAlpha;
        if (health <= 0)
        {
            playerHasLost = true;
            staticRenderer.material.color = new Color(staticRenderer.material.color.r,
                                                  staticRenderer.material.color.g,
                                                  staticRenderer.material.color.b,
                                                  0.65f);
            deathScreen.SetActive(true);
			AudioSource.PlayClipAtPoint (deathSound, transform.position, 1);
            Invoke("QuitGame", 9f);
            //LOSE CONDITION
        }

	}

    void QuitGame()
    {
        Application.Quit();
    }

    void OffsetTexture()
    {
        float xOffset = Random.value;
        float yOffset = Random.value;

        staticRenderer.material.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
