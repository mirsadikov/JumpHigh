using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject parent;
    public GameObject ground;


    // void OnCollisionExit(Collision col)
    // {
    //     if (col.gameObject.tag == "Respawn")
    //         Debug.Log("Escaped");
    // }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Respawn") && !Physics.Raycast(col.transform.position, Vector3.left, 40))
        {
            Vector3 spawnerPosition = col.transform.position;
            Instantiate(ground, new Vector3(spawnerPosition.x - 20, 0, 0),
                ground.transform.rotation, parent.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}