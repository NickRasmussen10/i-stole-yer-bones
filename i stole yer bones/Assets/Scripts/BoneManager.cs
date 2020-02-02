﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoneManager : MonoBehaviour
{
    [SerializeField] List<insideBone> insideBones = new List<insideBone>();
    [SerializeField] Slider healthbar;
    [SerializeField] Text scoreText;
    [SerializeField] List<button> buttons = new List<button>();
    [SerializeField] List<Animator> quoteAnimators = new List<Animator>();

    [SerializeField] SickoMode sickoMode;

    AudioManager audio;
    Vector3 healthBarOrigin;
    
    public float health = 1.0f;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionManager());
        healthBarOrigin = healthbar.transform.position;
        score = FindObjectOfType<Score>();
        audio = FindObjectOfType<AudioManager>();
        score.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int missingBones = GetNumMissingBones();
        if (missingBones == 0) {
            if (healthbar.transform.position != healthBarOrigin) healthbar.transform.position = healthBarOrigin;
            health += 0.0005f;
            StopCoroutine(Shake(healthbar.transform));
        }
        else
        {
            health -= missingBones * 0.0005f;
            StartCoroutine(Shake(healthbar.transform));
        }
        healthbar.value = health;
        if (health <= 0) SceneManager.LoadScene(2);

        AddScores();
    }

    int GetNumMissingBones()
    {
        int missing = 0;
        foreach (insideBone bone in insideBones)
        {
            if (!bone.hasBone) missing++;
        }
        return missing;
    }

    void AddScores()
    {
        int buttonsPressed = 0;
        foreach (button b in buttons)
        {
            if (b.pressed) buttonsPressed++;
        }
        score.score += buttonsPressed;

        if(buttonsPressed == 4 && !sickoMode.isSicko)
        {
            Debug.Log("we goin sicko");
            sickoMode.GoSickoMode();
        }
        else if (buttonsPressed < 4 && sickoMode.isSicko)
        {
            Debug.Log("no longer sicko");
            sickoMode.RIPSickoMode();
        }

        scoreText.text = "Score: " + score.score;
    }

    IEnumerator ExplosionManager()
    {
        int explosionChance;
        while (true)
        {
            explosionChance = Random.Range(0, 7);
            if(explosionChance == 0 && GetNumMissingBones() != 8)
            {
                int whoIsTheLuckyBone;
                do
                {
                    whoIsTheLuckyBone = Random.Range(0, 8);
                } while (!insideBones[whoIsTheLuckyBone].hasBone);
                insideBones[whoIsTheLuckyBone].Explode();

                int quoteIndex = Random.Range(1, 8);
                FindObjectOfType<AudioManager>().Play("kent" + quoteIndex);
                quoteAnimators[quoteIndex - 1].SetTrigger("play");
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    IEnumerator Shake(Transform t)
    {
        Vector3 position = t.position;
        while (true)
        {
            position.x += Random.Range(-0.01f, 0.01f);
            position.y += Random.Range(-0.01f, 0.01f);
            t.position = position;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
