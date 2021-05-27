using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    Vector3 startDistance,
            moveVector;

    // Start is called before the first frame update
    void Start()
    {
        startDistance = transform.position - target.position;
        if (PlayerPrefs.GetString("SwipeMode").Equals("On"))
        {
            target.GetComponent<PlayerMovement>().enabled = false;
            target.GetComponent<SwipeManagement>().enabled = true;
        }
        else if (PlayerPrefs.GetString("SwipeMode").Equals("Off"))
        {
            target.GetComponent<PlayerMovement>().enabled = true;
            target.GetComponent<SwipeManagement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = target.position + startDistance;
        moveVector.z = 0;
        moveVector.y = startDistance.y;

        transform.position = moveVector;
    }
}
