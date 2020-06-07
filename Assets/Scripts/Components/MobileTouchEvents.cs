using UnityEngine;

public class MobileTouchEvents : MonoBehaviour
{

    protected static int CurrentTouchId;
    
    public int currentTouchWatch = 64;
    
    private void Update()
    {
        if (!Game.IsMobilePlatform)
            return;
        if (Input.touches.Length <= 0)
            return;
        foreach (var touch in Input.touches)
        {
            CurrentTouchId = touch.fingerId;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    currentTouchWatch = CurrentTouchId;
                    OnTouchBegan();
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved();
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }
        }
    }

    protected virtual void OnTouchBegan() { }
    protected virtual void OnTouchEnded() { }
    protected virtual void OnTouchMoved() { }

}