//TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManagement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;

    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;

    public float playerSpeed,
                 firstLinePos,
                 lineDistance,
                 lineChangeSpeed;

    int lineNumber = 1,
        linesCount = 2;

    bool canChangeLine = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveVector = new Vector3(1, 0, 0);
    }

    void Update()
    {
        moveVector.x = playerSpeed;
        moveVector *= Time.deltaTime;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            if (!canChangeLine)
            {
                canChangeLine = true;
                lineNumber += (int)Mathf.Sign(direction.x);
                lineNumber = Mathf.Clamp(lineNumber, 0, linesCount);
            }
        }
        else canChangeLine = false;
        characterController.Move(moveVector);

        Vector3 newPosition = transform.position;
        newPosition.z = Mathf.Lerp(newPosition.z, firstLinePos + (lineNumber * lineDistance), Time.deltaTime * lineChangeSpeed + 0.06f);
        transform.position = newPosition;
    }
}