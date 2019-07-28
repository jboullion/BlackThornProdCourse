using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileStart;
    [SerializeField] float projectileDelay;

    float shotTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetAxis("Fire1") != 0)
        //if (Input.GetMouseButton(0))
        { 
            if(Time.time >= shotTime)
            {
                Instantiate(projectile, projectileStart.position, transform.rotation);
                shotTime = Time.time + projectileDelay;
            }
        }
    }
}
