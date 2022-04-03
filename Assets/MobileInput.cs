using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private float _savedX;
    private float _savedY;
    private bool _isRight;

    private float _distanceBetweenTouches;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            SwipeCheck();
        }

        else if (Input.touchCount == 2)
        {
            IncreaseScaleCheck();
        }

        
    }

    private void SwipeCheck()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            _isRight = touch.deltaPosition.x >= 0 ? true : false;
            _savedX = touch.position.x;
            _savedY = touch.position.y;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            float deltaY = Mathf.Abs(touch.position.y - _savedY);
            float deltaX = touch.position.x - _savedX;
            if (deltaX >= 100 && deltaY < 50)
            {
                Debug.Log("Свайп вправо");
            }
            Debug.Log("END Tap!");
        }
        else
        {
            bool changeToRight = touch.deltaPosition.x > 0 && !_isRight;
            bool changeToLeft = touch.deltaPosition.x < 0 && _isRight;
            if (changeToLeft || changeToRight)
            {
                Debug.Log("CHANGE DIRECTION!");
                _isRight = !_isRight;
                _savedX = touch.position.x;
                _savedY = touch.position.y;
            }

        }
    }

    private void IncreaseScaleCheck()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);


        if (firstTouch.phase == TouchPhase.Began || firstTouch.phase == TouchPhase.Stationary)
        {
            _distanceBetweenTouches = Vector3.Distance(firstTouch.position, secondTouch.position);
        }
        else
        {
            float direction = Vector2.Dot(firstTouch.deltaPosition, secondTouch.deltaPosition);
            if (direction <= 0)
            {
                float deltaDistanceBetweenTouches = Vector3.Distance(firstTouch.position, secondTouch.position) - _distanceBetweenTouches;
                if (deltaDistanceBetweenTouches / _distanceBetweenTouches >= 1.1)
                {
                    Debug.Log("Жест увеличение");
                }
            }
        }
    }
}