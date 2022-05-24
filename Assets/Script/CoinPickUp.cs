using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;

    [SerializeField] AudioClip mySound;


    bool wasPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(mySound, Camera.main.transform.position);

        if (other.tag=="Player" && !wasPickedUp)
        {
            wasPickedUp = true;
            FindObjectOfType<GameSession>().AddScore(scoreValue);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
