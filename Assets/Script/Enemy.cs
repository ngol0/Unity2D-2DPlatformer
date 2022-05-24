using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D()
    {
        //flip speed
        moveSpeed = -moveSpeed;

        //Flip Sprite
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }

}
