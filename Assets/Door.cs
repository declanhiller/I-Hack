using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private float doorMoveDistance = 1;

    private Vector3 _leftStartDoorPoint;
    private Vector3 _leftEndDoorPoint;

    private Vector3 _rightStartDoorPoint;
    private Vector3 _rightEndDoorPoint;

    private Coroutine _currentDoorAnimation;

    [SerializeField] private float doorVelocity;
    
    public bool IsOpen { get; private set; }


    private void Start() {
        _leftStartDoorPoint = leftDoor.position;
        _rightStartDoorPoint = rightDoor.position;
        _leftEndDoorPoint = leftDoor.position + leftDoor.transform.up * doorMoveDistance;
        _rightEndDoorPoint = rightDoor.position + rightDoor.transform.up * -doorMoveDistance;
    }


    public void UseDoor(bool closed) {
        if (_currentDoorAnimation != null) {
            StopCoroutine(_currentDoorAnimation);
        }

        _currentDoorAnimation = StartCoroutine(DoorAnimation(closed));

        IsOpen = closed;
    }

    IEnumerator DoorAnimation(bool isOpening) {
        
        float distanceSquared = 0;
        float timer = 0;

        Vector3 leftDoorStartingPosition = leftDoor.position;
        Vector3 rightDoorStartingPosition = rightDoor.position;

        Vector3 leftDoorTargetPosition = isOpening ? _leftEndDoorPoint : _leftStartDoorPoint;
        Vector3 rightDoorTargetPosition = isOpening ? _rightEndDoorPoint : _rightStartDoorPoint;

        float distance = Vector3.Magnitude(leftDoorStartingPosition - leftDoorTargetPosition);

        float totalTime = distance / doorVelocity;
        
        while (timer < totalTime) {

            Vector3 leftDoorPosition = Vector3.Lerp(leftDoorStartingPosition, leftDoorTargetPosition, timer / totalTime);
            Vector3 rightDoorPosition = Vector3.Lerp(rightDoorStartingPosition, rightDoorTargetPosition, timer / totalTime);

            leftDoor.position = leftDoorPosition;
            rightDoor.position = rightDoorPosition;

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        leftDoor.position = isOpening ? _leftEndDoorPoint : _leftStartDoorPoint;
        rightDoor.position = isOpening ? _rightEndDoorPoint : _rightStartDoorPoint;
    }
}
