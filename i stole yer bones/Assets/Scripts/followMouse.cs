using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //Putting this in followMouse because this script looks lonely. Fight me, Kent.
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
