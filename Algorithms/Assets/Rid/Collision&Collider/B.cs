using UnityEngine;
using System.Collections;

public class B : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane") return;
        print("B_OnCollisionEnter:Collision collision.name==" + collision.gameObject.name);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane") return;

        print("B_OnTriggerEnter: Collider other.name==" + other.gameObject.name);

    }
}
