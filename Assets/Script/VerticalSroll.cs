using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSroll : MonoBehaviour
{
    [SerializeField] float scrollRateStarting = 0.2f;
    [SerializeField] float scrollRateLater = 0.5f;


    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector2(0f, scrollRateStarting * Time.deltaTime));
        Coroutine myCoroutine = StartCoroutine(WaterRising());
    }

    IEnumerator WaterRising()
    {
        transform.Translate(new Vector2(0f, scrollRateStarting * Time.deltaTime));
        yield return new WaitForSeconds(15f);
        scrollRateStarting = scrollRateLater;
    }
}
