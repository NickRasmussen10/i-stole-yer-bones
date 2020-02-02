using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    bool instructions = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && instructions)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        else if (Input.anyKeyDown)
        {
            instructions = true;
        }
    }
}
