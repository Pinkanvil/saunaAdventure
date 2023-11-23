using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

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
        if(currentHealth < 0) 
        { 
            // We're dead
            
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
