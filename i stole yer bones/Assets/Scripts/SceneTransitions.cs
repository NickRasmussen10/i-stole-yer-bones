using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    int instructions = 1;
    [SerializeField] Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.anyKeyDown && instructions == 7)
            {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
            else if (Input.anyKeyDown)
            {
                instructions++;
                anim.SetTrigger("continue");
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
