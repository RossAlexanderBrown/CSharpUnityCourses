using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projSpeed = 10f;

    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        MoveBoundaries();
    }

    
    private void MoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.05f, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.9f, 0.05f, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.05f, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.95f, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXpos, newYpos);

    }

}
