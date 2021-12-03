using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceHolder : MonoBehaviour
{
    public GameObject Player;
    public GameObject Planet;
    public float smooth = 0.1f;

    // Update is called once per frame
    void Update()
    {
        //SMOOTH

        //POSITION
        transform.position = Vector3.Lerp(transform.position, Player.transform.position, smooth);

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        //ROTATION
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, smooth);
    }
}