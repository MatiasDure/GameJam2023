using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSprout : MonoBehaviour
{

    [SerializeField] Vector2 spawnDelay;
    [SerializeField] float amountToDecreaseDelayBy;

    void Start()
    {
        StartCoroutine(Spawn());
        DistanceTracker.OnMarkPassed += DecreaseDelayRange;
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            float timeToWait = Random.Range(spawnDelay.x, spawnDelay.y);
            var sprout = SproutPool.Instance.GetPooledRoot();
            sprout.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private void DecreaseDelayRange()
    {
        spawnDelay.y -= amountToDecreaseDelayBy;
    }

    private void OnDestroy()
    {
        DistanceTracker.OnMarkPassed -= DecreaseDelayRange;
    }
}
