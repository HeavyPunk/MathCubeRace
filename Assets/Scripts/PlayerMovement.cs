using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;


    public float playerSpeed,
                 firstLinePos,
                 lineDistance,
                 lineChangeSpeed;

    int lineNumber = 1,
        linesCount = 2;

    bool canChangeLine = false;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveVector = new Vector3(1, 0, 0);
    }
    
    void Update()
    {
        moveVector.x = playerSpeed;
        moveVector *= Time.deltaTime;

        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > .1f)
        {
            if (!canChangeLine)
            {
                canChangeLine = true;
                lineNumber += (int)Mathf.Sign(input);
                lineNumber = Mathf.Clamp(lineNumber, 0, linesCount);
            }
        }
        else
        {
            canChangeLine = false;
        }

        characterController.Move(moveVector);

        Vector3 newPosition = transform.position;
        newPosition.z = Mathf.Lerp(newPosition.z, firstLinePos + (lineNumber * lineDistance), Time.deltaTime * lineChangeSpeed + 0.06f);
        transform.position = newPosition;
    }
}
