using System.Collections;
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
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionManager());
        healthBarOrigin = healthbar.transform.position;
        score = FindObjectOfType<Score>();
        audio = FindObjectOfType<AudioManager>();
        score.score = 0;
        isDead = false;
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
        if (health <= 0 && !isDead) StartCoroutine(Die());

        AddScores();
    }

    IEnumerator Die()
    {
        isDead = true;
        for(int i = 0; i < insideBones.Count; i++)
        {
            HingeJoint2D h = insideBones[i].gameObject.transform.parent.GetComponent<HingeJoint2D>();
            if(h) h.enabled = false;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, 5);
        foreach (Collider2D c in colliders)
        {
            Rigidbody2D r = c.gameObject.GetComponent<Rigidbody2D>();
            if (r)
            {
                int direction = Random.Range(0, 4);
                Vector2 force = Vector2.zero;
                switch (direction)
                {
                    case 0:
                        force = new Vector2(5.0f, 5.0f);
                        break;
                    case 1:
                        force = new Vector2(-5.0f, 5.0f);
                        break;
                    case 2:
                        force = new Vector2(5.0f, -5.0f);
                        break;
                    case 3:
                        force = new Vector2(-5.0f, -5.0f);
                        break;
                }
                r.AddForce(force, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(2);
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
        if (buttonsPressed < 4 && sickoMode.isSicko)
        {
            Debug.Log("no longer sicko");
            sickoMode.RIPSickoMode();
        }

        scoreText.text = "Score: " + score.score;
    }

    IEnumerator ExplosionManager()
    {
        int explosionChance;
        float timeSinceLast = 0.0f;
        while (true)
        {
            timeSinceLast += Time.deltaTime;
            explosionChance = Random.Range(0, 7);
            if((explosionChance == 0 || timeSinceLast > 3.0f) && GetNumMissingBones() != 8)
            {
                timeSinceLast = 0.0f;
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
