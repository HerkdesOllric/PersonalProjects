using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using Base.Game.Signal;
namespace Main
{
    public abstract class InputManager : MonoBehaviour
    {
        //Needs more testing and performence improvements, currently stable but a bit broken and performence unfriendly!

        public Touch Touch1;

        public float SwipeDragSensetivity = 100f;
        [Range(0.2f, 1f)]
        public float MaxInputTimer = 0.2f;
        float CountingTimer;

        float MaxSwipeTimer = 0.3f;
        float SwipeTimer;

        int _touchCount;

        public bool SwipeControl;

        public void TouchControlStart()
        {
            StartCoroutine(TapTimerControl());
            StartCoroutine(TapCheck());
            switch (SwipeControl)
            {
                case true:
                    StartCoroutine(SwipeTimerControl());
                    break;
                case false:
                    SwipeDragSensetivity = 999;
                    break;
            }
        }
        public void TouchControl()
        {
            if (Input.touchCount > 0)
            {
                Touch1 = Input.GetTouch(0);
                if (Input.touchCount <= 1 && Touch1.phase == TouchPhase.Began)
                {
                    //On tap or double tap control
                    OnSingleTap();
                    CountingTimer = MaxInputTimer;
                    _touchCount += 1;
                }
                else if (Input.touchCount <= 1 && Touch1.phase == TouchPhase.Stationary)
                {
                    //On hold control
                    if (CountingTimer < 0)
                    {
                        _touchCount = 0;
                        OnHold();
                    }
                }
                else if (Input.touchCount <= 1 && Touch1.phase == TouchPhase.Moved)
                {
                    //On Drag or swpie control

                    _touchCount = 0;
                    CountingTimer = .3f;
                    if (Input.GetTouch(0).deltaPosition.x > SwipeDragSensetivity || Input.GetTouch(0).deltaPosition.y > SwipeDragSensetivity)
                    {
                        SwipeTimer = MaxSwipeTimer;
                    }
                    else if (Input.GetTouch(0).deltaPosition.x <= SwipeDragSensetivity || Input.GetTouch(0).deltaPosition.y <= SwipeDragSensetivity)
                    {
                        if (SwipeTimer <= 0)
                        {
                            OnDrag();
                        }
                    }
                }
            }
        }

        IEnumerator TapCheck()
        {
            yield return new WaitUntil(() => CountingTimer > 0);
            if (_touchCount == 1 && CountingTimer < 0)
            {
                _touchCount = 0;
            }
            else if (_touchCount > 1 && CountingTimer > 0)
            {
                OnDoubleTap();
                _touchCount = 0;
            }
            StartCoroutine(TapCheck());
        }

        IEnumerator TapTimerControl()
        {
            while (CountingTimer > 0)
            {
                CountingTimer -= 1 * Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitUntil(() => CountingTimer > 0);
            StartCoroutine(TapTimerControl());
        }

        IEnumerator SwipeTimerControl()
        {
            yield return new WaitUntil(() => SwipeTimer > 0);
            OnSwipe();
            while (SwipeTimer > 0)
            {
                SwipeTimer -= 1 * Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            StartCoroutine(SwipeTimerControl());
        }

        public virtual void OnSingleTap()
        {

        }

        public virtual void OnDoubleTap()
        {

        }

        public virtual void OnHold()
        {

        }

        public virtual void OnDrag()
        {

        }

        public virtual void OnSwipe()
        {

        }
    }
}

