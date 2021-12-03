using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHeatUi : MonoBehaviour
{
    public Image WeaponHeatBar2;
    public Image bar;
    public float heatValue;
    public Color colorMin;
    public Color colorMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float amount = (heatValue / 100.0f) * 72.0f / 360;
        float amount = (heatValue * 0.2f) / 100.0f;

        bar.fillAmount = amount;
        bar.color = Color.Lerp(colorMin, colorMax, amount / 0.2f);

        WeaponHeatBar2.fillAmount = amount;
        WeaponHeatBar2.color = Color.Lerp(colorMin, colorMax, amount / 0.2f);
    }
}
