using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public GameObject impact;
    public Transform planet;
    public float speed;
    public float angle;
    public float maxDistance;
    public int damage;

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
        if (other.tag == "Player")
        {
            triggeringEntity = other.gameObject;
            triggeringEntity.GetComponent<Player>().health -= damage;
            Instantiate(impact.transform, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Obstacle" || other.tag == "ForceField")
        {
            Instantiate(impact.transform, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
