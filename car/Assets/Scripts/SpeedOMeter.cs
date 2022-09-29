using System;
using UnityEngine;
using UnityEngine.UI;

public enum SpeedUnit
{
    mph,
    kmh
}

public class SpeedOMeter : MonoBehaviour
{
    private float currentSpeed;
    [SerializeField] private Text speedText;
    [SerializeField] private string previousText;
    [SerializeField] private SpeedUnit speedUnit;
    [SerializeField] private int decimalPlaces;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

        if (speedUnit == SpeedUnit.mph)
        {

            // 2.23694 is the constant to convert a value from m/s to mph.
            currentSpeed = (float)Math.Round(rb.velocity.magnitude * 2.23694f, decimalPlaces);

            //currentSpeed = (float)Math.Round((double)rb.velocity.magnitude * 2.23694f, 0);

            speedText.text = previousText + currentSpeed.ToString() + " mph";

        }

        else 
        {

            // 3.6 is the constant to convert a value from m/s to km/h.
            currentSpeed = (float)Math.Round(rb.velocity.magnitude * 3.6f, decimalPlaces);

            //currentSpeed = (float)Math.Round((double)rb.velocity.magnitude * 3.6f, 0);

            speedText.text = previousText + currentSpeed.ToString() + " km/h";

        }
    }
}