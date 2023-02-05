using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake(float duration, float shakeStrenght, float frequency = 0)
    {
        float elapsedTime = 0;
        Vector3 originalPos = transform.localPosition;

        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-1,2) * shakeStrenght;
            float yOffset = Random.Range(-1,2) * shakeStrenght;

            this.transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(frequency);
        }

        this.transform.localPosition = originalPos;
    }
}
