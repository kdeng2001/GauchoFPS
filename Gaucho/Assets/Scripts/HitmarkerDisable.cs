using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerDisable : MonoBehaviour
{
    // Start is called before the first frame update
    public float awakeTime = 10f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(disable(awakeTime));
        }
    }

    IEnumerator disable(float awakeTime)
    {
        //Debug.Log("yielding...");
        yield return new WaitForSeconds(awakeTime);
        gameObject.SetActive(false);
    }
}
