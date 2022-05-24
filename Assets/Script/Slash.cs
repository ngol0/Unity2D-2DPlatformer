using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] float slashSpeed = 5f;
    float xSpeed;

    //reference
    Rigidbody2D myRigidBody;
    Player player;
    
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.transform.localScale.x * slashSpeed;
        transform.localScale = new Vector2(player.transform.localScale.x, 1f);

    }

    private void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed,0f);
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
