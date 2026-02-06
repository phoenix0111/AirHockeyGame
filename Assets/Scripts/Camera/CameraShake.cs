using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;
    Coroutine shakeRoutine;

    void Awake()
    {
       // Screen.SetResolution(1080, 2400, true);
        originalPos = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, strength));
    }

    IEnumerator ShakeRoutine(float duration, float strength)
    {
        float time = 0f;

        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        shakeRoutine = null;
    }
}
