using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInformParent : MonoBehaviour
{

    public event Action OnObjectEnter;
    public event Action OnObjectExit;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnObjectEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnObjectExit?.Invoke();
        }
    }


}
