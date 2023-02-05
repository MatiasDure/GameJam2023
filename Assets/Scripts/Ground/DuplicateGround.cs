using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateGround : MonoBehaviour
{

    List<GameObject> grounds = new List<GameObject>(); 

    [SerializeField]
    float offset;



    [SerializeField]
    GameObject item;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newGround = Instantiate(item, new Vector3(0, -0.5f, offset * 2), Quaternion.Euler(-90,0,0));
            grounds.Add(newGround);
            newGround.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        grounds[0].SetActive(true);
        grounds[0].transform.position = new Vector3(0, -0.5f, 0);
        grounds[1].SetActive(true);


        //grounds[0].transform.position = 

    }

    void MakeOneActive()
    {
        foreach (var item in grounds)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = new Vector3(0, -0.5f, offset * 2);
                item.SetActive(true);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in grounds)
        {
            if (!item.activeInHierarchy) continue;
            if (item.transform.position.z < -offset -10)
            {
                MakeOneActive();
                item.SetActive(false);
            }

        }
       
        
    }
}
