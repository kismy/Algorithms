using UnityEngine;
using System.Collections;

public class A : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane") return;

        print("A_OnCollisionEnter:Collision collision.name==" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane") return;

        print("A_OnTriggerEnter: Collider other.name==" + other.gameObject.name);

    }
}
