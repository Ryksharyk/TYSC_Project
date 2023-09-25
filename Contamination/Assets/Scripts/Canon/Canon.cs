using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class Canon : MonoBehaviour
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
        Debug.Log(distance);
        timer += Time.deltaTime;

        

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

    public Vector3 rangeAngle;


    void rotate()
    {
        rangeAngle = transform.eulerAngles;
        
        if (!triggered)
        {
            
            //Debug.Log(rotationSpeed);
            //Debug.Log(rangeAngle);
            if (rangeAngle.z > 90 && rangeAngle.z < 270)
            {
                Debug.Log("Reached angle 60");
                rotationSpeed = -rotationSpeed;    
            }
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
        
}
