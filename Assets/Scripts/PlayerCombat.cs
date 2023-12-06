using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeTime;
    private bool isCharging;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && chargeTime < 2) 
        {
            isCharging = true;
            if (isCharging == true) 
            {
                chargeTime += Time.deltaTime * chargeSpeed;
            }
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Attack();
            chargeTime = 0;
        }
        else if (Input.GetMouseButtonUp(0) && chargeTime >= 2) 
        {
            ReleaseCharge();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies) 
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    void ReleaseCharge() 
    {
        isCharging = false;
        chargeTime = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
