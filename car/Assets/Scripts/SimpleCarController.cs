using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50; //Kecepatan
    public float brakeForce = 2000;
    private float presentBreakForce = 0f;
    public float throttleForce;

    public Rigidbody rb;

    public void GetInput()
    {
        m_horizontalInput = SimpleInput.GetAxis("Horizontal"); // Belok kiri kanan menggunakan A dan D
        m_verticalInput = SimpleInput.GetAxis("Vertical");
        Debug.Log(m_horizontalInput);   // Maju mundur menggunakan W dan S
    }

    private void Steer() //setir
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate() // berjalan
    {
        // frontDriverW.motorTorque = m_verticalInput * motorForce;
        // frontPassengerW.motorTorque = m_verticalInput * motorForce;
        rearDriverW.motorTorque = m_verticalInput * motorForce;
        rearPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    public void ApplyBreaks()
    {
        // if (Input.GetKey(KeyCode.Space))
        //     presentBreakForce = brakingForce;
        // else
        //     presentBreakForce = 0f;

        StartCoroutine(brake());


    }
    IEnumerator brake() // mengerem
    {
        presentBreakForce = brakeForce;

        frontDriverW.brakeTorque = presentBreakForce;
        frontPassengerW.brakeTorque = presentBreakForce;
        rearDriverW.brakeTorque = presentBreakForce;
        rearPassengerW.brakeTorque = presentBreakForce;
        // rb.AddForce(transform.forward * Time.deltaTime * (1 + brakeForce));

        yield return new WaitForSeconds(2f);

        presentBreakForce = 0f;

        frontDriverW.brakeTorque = presentBreakForce;
        frontPassengerW.brakeTorque = presentBreakForce;
        rearDriverW.brakeTorque = presentBreakForce;
        rearPassengerW.brakeTorque = presentBreakForce;


    }

    private void UpdateWheelPoses()// supaya roda terlihat berputar
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        brake();
        // Debug.Log(Input.GetAxis("Vertical"));
    }


}
