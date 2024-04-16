using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    //Attach this script to a Camera in the editor.
    //Change start to true when shift or

    public bool start = false;
    public AnimationCurve curve;
    public float duration = 0.25f;

    public float returnSpeed = 0.1f;

    private void Update()
    {
        if(start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    private IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            Debug.Log(Random.insideUnitSphere);
            transform.localPosition += Random.insideUnitSphere * (strength/2);
            yield return null;
        }
       
        while(Vector2.Distance(transform.position, startPos) > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, returnSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
