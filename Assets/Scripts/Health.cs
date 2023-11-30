using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;

    // Debug dataa varten
    public GUIStyle myStyle;

    // Start is called before the first frame update
    void Start()
    {
        myStyle.fontSize = 18;
        myStyle.normal.textColor = Color.white;


        currentHealth = maxHealth;
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // We're dead

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "TestiVihollinen")
        {
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (collision.tag == "Player")
        {
            var healthComponent = collision.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        // Debug data
        GUI.Label(new Rect(10, 10, 100, 20), "Health : " + currentHealth, myStyle);
    }


}