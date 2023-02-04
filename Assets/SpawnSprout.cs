using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSprout : MonoBehaviour
{

    [SerializeField] Vector2 spawnDelay;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
