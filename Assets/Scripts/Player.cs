using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject weaponHeatBar;
    public Slider Healthbar;
    public float health;
    public float speed = 4;
    public GameObject ForceField;
    public GameObject WeaponObj;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public float waitTime;

    private bool mouseButtonPressed = false;
    private bool overHeated = false;
    private float nextActionTime = 0.0f;
    private Camera mainCamera;
    private GameObject playerPlane;
    float currentTime = 0;
    AudioSource[] audioDatas;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        GetComponent<Rigidbody>().freezeRotation = true;
        audioDatas = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //ROTATION
        Plane playerPlane = new Plane(WeaponObj.transform.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);

            float angle = Vector3.Angle(-transform.forward, (transform.position - targetPoint));
            float sign = Mathf.Sign(Vector3.Dot((transform.position - targetPoint), -transform.right));
            float finalAngle = angle * sign;

            WeaponObj.transform.localRotation = Quaternion.AngleAxis(finalAngle, Vector3.up);
        }

        //MOVEMENT
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, 0, z);

        //WEAPON HEAT
        if (!overHeated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseButtonPressed = true;
            }
            if (mouseButtonPressed)
            {
                if (weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue < 100)
                    weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue += 0.3f;
                else
                {
                    overHeated = true;
                }
            }
            else if (weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue > 0)
                weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue -= 0.8f;
            if (Input.GetMouseButtonUp(0))
            {
                mouseButtonPressed = false;
            }
        }
        else if (weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue < 2)
        {
            overHeated = false;
            mouseButtonPressed = false;
        }
        else
        {
            weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue -= 0.8f;
        }
        audioDatas[1].pitch = (weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue * 3) / 110;
        audioDatas[1].volume = (weaponHeatBar.GetComponent<WeaponHeatUi>().heatValue * 0.3f) / 110;

        //SHOOTING
        if (Input.GetMouseButton(0) && overHeated == false && mouseButtonPressed)
        {
            shoot();
        }

        //FORCEFIELD
        if (Input.GetKeyDown(KeyCode.Space))
            useForceField();

        //HEALING
        if (health < 100 && Time.time > nextActionTime)
        {
            nextActionTime += 1;
            health += 1;
        }

        // HEALTH & DIE
        if (health > 0)
            Healthbar.value = health / 100f;
        // you died
    }

    private void useForceField()
    {
        if (ForceField.activeInHierarchy == false)
            ForceField.SetActive(true);
        else
            ForceField.SetActive(false);
    }

    private void shoot()
    {
        if (currentTime >= waitTime)
        {
            Instantiate(bullet.transform, bulletSpawn.transform.position, WeaponObj.transform.rotation);
            audioDatas[0].PlayOneShot(audioDatas[0].clip, 0.5f);
            currentTime = 0;
        }
        currentTime += 1 * Time.deltaTime;
    }
}
