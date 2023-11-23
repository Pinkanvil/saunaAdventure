using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
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
}
