using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SpawnSprout : MonoBehaviour
{
    [SerializeField] float spawnDelay;

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
            var sprout = SproutPool.Instance.GetPooledRoot();
            sprout.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
