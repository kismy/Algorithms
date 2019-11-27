using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private Transform player;
    private const float mixDistance = 0.5f;
    private int diedai = 0;

    private bool isHaveObstacleInView = false;
	
	void Start ()
	{
	    player = GameObject.Find("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(player.position,player.up,1);
        transform.LookAt(player.position);


	    Ray myRay;
        RaycastHit hit;
	    if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
	    {
	       
            Debug.DrawLine(transform.position, hit.point, Color.red, 1);

            if (hit.transform.gameObject.name != "Player")
	        {
	            isHaveObstacleInView = true;
	        }
            else if (hit.transform.gameObject.name == "Player")
            {
	            isHaveObstacleInView = false;
	        }
	    }


	    if (Vector3.Distance(player.position ,((player.position + transform.position) / 2.0f) +transform.position)/ 2.0f
           < mixDistance)
	    {
	        diedai++;
	        Vector3 rarOriginVector3 = (player.position + transform.position) /2;
            if (Physics.Raycast(rarOriginVector3, transform.forward, out hit, 20))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1);

                if (hit.transform.gameObject.name != "Player")
                {
                    isHaveObstacleInView = true;
                }
                else if (hit.transform.gameObject.name == "Player")
                {
                    isHaveObstacleInView = false;
                }
                print(isHaveObstacleInView);
            }

            Vector3 cameraTargetPos;

        }


	}
}
