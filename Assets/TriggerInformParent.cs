using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInformParent : MonoBehaviour
{

    public event Action OnTriggered;
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
        PlayerTest playerMovement;
        other.gameObject.TryGetComponent<PlayerTest>(out playerMovement);
        if(playerMovement != null)
        {
            OnTriggered?.Invoke();
        }
    }
}
