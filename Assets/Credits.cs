using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    [SerializeField] GameObject credits; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCredit()
    {
        credits.SetActive(!credits.activeInHierarchy);
    }
    
}
