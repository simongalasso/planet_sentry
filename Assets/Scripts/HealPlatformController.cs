using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlatformController : MonoBehaviour
{
    public float delay;
    public float healPower;
    public GameObject Sphere;

    private float duration = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Sphere.activeInHierarchy == false)
        {
            duration += 1 * Time.deltaTime;
            if (duration > delay)
                Sphere.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Sphere.activeInHierarchy == true)
        {
            audioSource.PlayOneShot(audioSource.clip, 0.5f);
            float currentHealth = other.gameObject.GetComponent<Player>().health;
            float value = (currentHealth + healPower > 100) ? 100 - currentHealth : healPower;
            other.gameObject.GetComponent<Player>().health += value;
            Sphere.SetActive(false);
            duration = 0;
        }
    }
}
