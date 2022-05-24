using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator myAnimator;
    BoxCollider2D myCollider;
    GameSession score;

    //variables
    bool wasCollected = false;
    int value = 50;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }

    void Open()
    {
        int player = LayerMask.GetMask("Player");

        if(myCollider.IsTouchingLayers(player) && !wasCollected)
        {
            wasCollected = true;
            myAnimator.SetTrigger("isTouched");
            FindObjectOfType<GameSession>().AddScore(value);
            //gameObject.SetActive(false);
        }
        
    }
}
