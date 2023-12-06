using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public GameObject detectionPoint;

    public float direction;
    public LayerMask groundLayer;


    public Health pHealth;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {

    }




    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().currentHealth -= damage;
        }
    }

    private void LateUpdate()
    {
        Debug.DrawRay(detectionPoint.transform.position, Vector2.down, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(detectionPoint.transform.position,
           Vector2.down, 1, groundLayer);

        if (hit.collider == null)
        {
            // Tämä ajetaan jos säde ei osunut maahan.
            ChangeDirection();
        }

        RaycastHit2D hit2 = Physics2D.Raycast(detectionPoint.transform.position,
            Vector2.right, 0.2f, groundLayer);

        if (hit2.collider != null)
        {
            // Tämä ajetaan jos säde ei osunut maahan.
            ChangeDirection();
        }

    }

    void ChangeDirection()
    {
        direction *= -1; // Aina kun suunta vaihtuu, kerrotaan suunta -1:llä, jolloin
        // transformin ja loclascalen suunta on eri. direction on joko 1 tai -1 
    }



}