using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.Universal;
using UnityEngine;

    public class CanonType1 : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;

    

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
        RotateObject();
        rotate();    
    }
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = true;
            RotateObject();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (distance < 30)
            {
                triggered = true;
                RotateObject();
                if (timer > 0.5)
                {
                    timer = 0;
                    shoot();
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = false;
        }
    }

    public float rotationSpeed;
    private bool triggered=false;
    void RotateObject()
    {
        if (triggered)
        {

            Vector3 direction = player.transform.position - transform.position;
            
            float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
    }

    private Vector3 rangeAngle;
    public float Angle1;
    public float Angle2;
    private bool inrange = true;
    void rotate()
    {
        rangeAngle = transform.eulerAngles;
        if (rangeAngle.z > Angle1+1 && rangeAngle.z < Angle2-1)
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
                if (rangeAngle.z > Angle1+1 && rangeAngle.z < 180)
                {
                    rotationSpeed = -50;
                }
                if (rangeAngle.z < Angle2-1 && rangeAngle.z > 180)
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
                laserLight.color = new Color(0,255,0);
            }
            else if (turnOn > OffFor && !lightOn)
            {
                lightOn = true;
                turnOff = 0;
                laserCollider.enabled = true;
                laserLight.color = new Color(255,0,0);
            }
        }
    }

    
        
}
