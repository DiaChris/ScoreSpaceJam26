using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.25f;
    public float magnitude = 1;
    public void StartShaking()
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f,1f)*magnitude;
            float y = Random.Range(-1f,1f)*magnitude;

            transform.localPosition = new Vector3(x,y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
