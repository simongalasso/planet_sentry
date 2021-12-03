using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Planet;
    public ParticleSystem deathParticles;
    public float timerForNewPath;
    public float speed;
    public float health;
    public float waitTime;
    public float alertDistance;
    public float playerDistance;
    public float shootDistance;
    public bool friendly;
    public bool mobile;

    private GameObject Player;
    private AudioSource[] audioSources;

    float currentTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        timer = timerForNewPath;
        currentTime = waitTime;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // DIE
        if (health <= 0)
            Destroy(this.gameObject);

        //MOVEMENT CALCUL
        Vector3 spokeToActual = transform.position - Planet.transform.position;
        Vector3 spokeToCorrect = Player.transform.position - Planet.transform.position;
        float angleFromCenter = Vector3.Angle(spokeToActual, spokeToCorrect);
        float distance = 2 * Mathf.PI * Planet.GetComponent<SphereCollider>().radius * (angleFromCenter / 360);

        if (distance < alertDistance)
        {
            //ROTATIONS TO PLAYER
            Vector3 point = Player.transform.position;
            Vector3 v = point - transform.position;
            Vector3 d = Vector3.Project(v, transform.up.normalized);
            Vector3 projectedPoint = point - d;

            float angle = Vector3.Angle(-transform.forward, (transform.position - projectedPoint));
            float sign = Mathf.Sign(Vector3.Dot((transform.position - projectedPoint), -transform.right));
            float finalAngle = angle * sign;

            transform.Rotate(0, finalAngle, 0);
        }
        else if (timer <= 0)
        {
            //ROTATIONS RANDOM
            float newY = transform.rotation.y + (Random.Range(-45, 45));
            transform.Rotate(0, newY, 0);
            timer = timerForNewPath;
        }

        //MOVEMENT
        if (mobile && distance > playerDistance)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        timer -= 1 * Time.deltaTime;

        //SHOOT
        if (!friendly && distance < shootDistance && currentTime <= 0)
        {
            Instantiate(Bullet.transform, this.gameObject.transform.GetChild(0).position, transform.rotation);
            audioSources[1].PlayOneShot(audioSources[1].clip, 0.5f);
            currentTime = waitTime;
        }
        currentTime -= 1 * Time.deltaTime;
    }
}
