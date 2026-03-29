using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false; 
    [SerializeField]
    private bool IsRotatingDoor = true;
    [SerializeField] 
    private float speed = 1f;

    [Header("Rotation Configs")] 
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;

    [Header("Sliding Configs")] 
    [SerializeField]
    private Vector3 SlidingDirection = Vector3.back;
    [SerializeField]
    private float SlidingAmount = 1.9f;
    
    private Vector3 StartPosition;
    private Vector3 StartRotation;
    private Vector3 Forward;
    
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Since "Foward" is pointing into the door frame
        Forward = transform.right;
        StartPosition = transform.position;
    }

    public void OpenDoor(Vector3 playerPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (playerPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }
        IsOpen = true;
        
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlidingDirection * SlidingAmount;
        Vector3 startPosition = transform.position;
        
        
        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Slerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
    public void CloseDoor()
    {
        if (IsOpen)
        {
            StopCoroutine(AnimationCoroutine);
        }

        if (IsRotatingDoor)
        {
            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
        else
        {
            AnimationCoroutine = StartCoroutine(DoSlidingClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        
        IsOpen = false;
        
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        
        
        float time = 0;
        IsOpen = false;
        while (time < 1)
        {
            transform.position = Vector3.Slerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
    


}
