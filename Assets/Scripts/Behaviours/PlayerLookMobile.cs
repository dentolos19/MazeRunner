using UnityEngine;

public class PlayerLookMobile : MonoBehaviour
{

    private void Update()
    {
        if (Input.touchCount <= 0)
            return;
        var touch = Input.touches[0];
        switch (touch.phase)
        {
            case TouchPhase.Began:
                // TODO
                break;
            case TouchPhase.Ended:
                // TODO
                break;
        }
    }

}