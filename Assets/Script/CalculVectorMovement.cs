using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculVectorMovement : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    public float distanceToSwipe;
    public GameObject objectToMove;
    public float delay;
    private float initialDelay;

    private void Start()
    {
        initialDelay = delay;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            objectToMove.SetActive(true);
            objectToMove.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

            delay -= Time.deltaTime;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Stockage du point de départ
                    startPosition = touch.position;
                    delay = initialDelay;
                    break;
                case TouchPhase.Ended:
                    // Stockage du point de fin
                    endPosition = touch.position;
                    AnalyseSwipe();
                    break;
            }
        }
        else
        {
            objectToMove.SetActive(false);
        }
    }

    private void AnalyseSwipe()
    {
        if ((Vector2.Distance(startPosition, endPosition) > distanceToSwipe) && (delay > 0))
        {
            Debug.Log(startPosition);
            Debug.Log(endPosition);
        }
    }
}