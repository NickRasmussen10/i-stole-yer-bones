using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    int instructions = 1;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && instructions == 7)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        else if (Input.anyKeyDown)
        {
            instructions++;
            anim.SetTrigger("continue");
        }
    }
}
