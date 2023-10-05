using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LaserRotator : MonoBehaviour
{
    private GameObject player;
    private float timer;
    CanonType1 CanonType1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private float distance;
    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
        timer += Time.deltaTime;
        laserOff();
    }
    private void FixedUpdate()
    {
        
        rotate();
    }


    public float rotationSpeed;
    private bool triggered = false;
    

    private Vector3 rangeAngle;
    public float Angle1;
    public float Angle2;
    private bool inrange = true;
    void rotate()
    {
        rangeAngle = transform.eulerAngles;
        if (rangeAngle.z > Angle1 + 1 && rangeAngle.z < Angle2 - 1)
        {
            inrange = false;
        }

        if (!triggered)
        {
            if (inrange)
            {
                if (rangeAngle.z > Angle1 && rangeAngle.z < Angle2)
                {
                    rotationSpeed = -rotationSpeed;
                }
            }
            if (!inrange)
            {
                if (rangeAngle.z > Angle1 + 1 && rangeAngle.z < 180)
                {
                    rotationSpeed = -50;
                }
                if (rangeAngle.z < Angle2 - 1 && rangeAngle.z > 180)
                {
                    rotationSpeed = 50;
                }
            }
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    private float turnOff;
    private float turnOn;
    public float OnFor;
    public float OffFor;
    private bool lightOn = true;
    [SerializeField] Collider2D laserCollider;
    [SerializeField] Light2D laserLight;
    void laserOff()
    {
        turnOff += Time.deltaTime;
        turnOn += Time.deltaTime;

        if (!triggered)
        {
            if (turnOff > OnFor && lightOn)
            {
                lightOn = false;
                turnOn = 0;
                laserCollider.enabled = false;
                laserLight.color = new Color(0, 255, 0);
            }
            else if (turnOn > OffFor && !lightOn)
            {
                lightOn = true;
                turnOff = 0;
                laserCollider.enabled = true;
                laserLight.color = new Color(255, 0, 0);
            }
        }
    }
}
