using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impact;
    public GameObject Explosion;
    public Transform planet;
    public float speed;
    public float angle;
    public float maxDistance;
    public float damage;

    private GameObject triggeringEntity;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.RotateAround(planet.position, transform.right, angle * Time.deltaTime);

        distance += 1 * Time.deltaTime;

        if (distance >= maxDistance)
            Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            triggeringEntity = other.gameObject;
            AudioSource[] audiosources = triggeringEntity.GetComponents<AudioSource>();
            audiosources[0].PlayOneShot(audiosources[0].clip, 0.5f);
            triggeringEntity.GetComponent<EntityController>().health -= damage;
            if (triggeringEntity.GetComponent<EntityController>().health <= 0)
                Instantiate(Explosion.transform, transform.position, transform.rotation);
            Instantiate(impact.transform, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Obstacle")
        {
            Instantiate(impact.transform, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
