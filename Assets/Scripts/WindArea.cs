using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    List<Rigidbody> RigidbodiesInWindZone = new List<Rigidbody>();
    public float strength;
    public Vector3 direction;

    public ShakeCamera shakeCamera;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if(rb != null)
        {
            RigidbodiesInWindZone.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();

        if(rigidbody != null)
        {
            RigidbodiesInWindZone.Remove(rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if(RigidbodiesInWindZone.Count > 0)
        {
            foreach(Rigidbody rigid in  RigidbodiesInWindZone)
            {
                rigid.AddForce(direction * strength);
                StartCoroutine(shakeCamera.Shake(.15f, .4f));
            }
        }
    }
}
