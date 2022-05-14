using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
	// Start is called before the first frame update
	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;
    public GameObject gotHitScreen;


	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
		if(gotHitScreen != null)
		{
			if (gotHitScreen.GetComponent<Image>().color.a > 0)
			{
				var color = gotHitScreen.GetComponent<Image>().color;
				color.a -= 0.05f;

		        gotHitScreen.GetComponent<Image>().color = color;
			}

		}
		
	}

	public void TakeDamage(int damage)
	{
		gotHit();
		GetComponent<AudioSource>().Play();
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
	}

	void gotHit()
	{
		var color = gotHitScreen.GetComponent<Image>().color;
		color.a = 0.8f;

		gotHitScreen.GetComponent<Image>().color = color;
	}
}
