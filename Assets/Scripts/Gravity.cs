using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject Planet;

    Vector3 gravityUp;
    float gravity = -10;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        gravityUp = (transform.position - Planet.transform.position).normalized;
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 50 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
    }
}
