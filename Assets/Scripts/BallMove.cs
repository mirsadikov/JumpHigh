using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BallMove : MonoBehaviour
{
    private Rigidbody rbBall;
    private Vector3 startPostion;
    public float xForce;
    public float airXForce;
    public float groundYForce;
    public float yForce;
    public Camera myCamera;
    public Text meter;
    //public List<Preset> presets = new List<Preset>();
    void Start()
    {
        rbBall = GetComponent<Rigidbody>();
        startPostion = transform.position;
    }

    void FixedUpdate()
    {
        meter.text = $@"{Math.Round((startPostion.x - transform.position.x) / 2)}m";

        float x = 0.0f;
        float z = 0.0f;
        float y = 0.0f;

        if (IsGrounded())
            rbBall.mass = 4f;
        else
            rbBall.mass = 1f;

        myCamera.fieldOfView = Mathf.MoveTowards(myCamera.fieldOfView, 65f + Math.Abs(rbBall.velocity.x * 0.8f),
            Time.deltaTime * 10);
        // myCamera.fieldOfView = 65f - rbBall.velocity.x * 2;

        if (Input.GetKey(KeyCode.Space) || Input.touchSupported && Input.touchCount > 0)
        {
            if (IsGrounded())
            {
                y = y - groundYForce;
                x = x - xForce;
            }
            else
            {
                y = y - yForce;
                x = x - airXForce;
            }
        }

        rbBall.AddForce(x, y, z);
    }

    RaycastHit hit;

    Boolean IsGrounded()
    {
        var position = transform.position;
        return Physics.Raycast(position, Vector3.down, 0.8f)
               || Physics.Raycast(position, Vector3.forward, 0.8f)
               || Physics.Raycast(position, Vector3.left, 0.8f)
               || Physics.Raycast(position, Vector3.right, 0.8f)
               || Physics.Raycast(position, new Vector3(-1, -1, 0), 0.8f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killer")
        {
            // rbBall.velocity = Vector3.zero;
            // rbBall.angularVelocity = Vector3.zero;
            // rbBall.position = startPostion;
            // myCamera.fieldOfView = 65f;

            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            Time.timeScale = 1;
        }

        // if (other.CompareTag(("Changer")))
        // {
        //     switch (other.name)
        //     {
        //         case "TractorWheel":
        //             presets[1].ApplyTo(gameObject);
        //             break;
        //         case "CarWheel":
        //             presets[0].ApplyTo((gameObject));
        //             break;
        //     }
        // }
    }
}