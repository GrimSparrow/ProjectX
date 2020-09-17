using ProjectX;
using UnityEngine;

public class LookAroundController : MonoBehaviour
{
    [Header("First person camera")]
    public Transform fpCameraTransform;
    [Space(20)]
    [Header("Game Settings")]
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private FirstPersonController fpc;

    float maxCameraDistance;
    bool isMoving;

    // Touch detection
    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    // Camera control
    Vector2 lookInput;
    float cameraPitch;

    // Start is called before the first frame update
    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;
    }
    float   MyAngle = 0F;
    // Update is called once per frame
    void Update()
    {
        if (ExamineController.Instance.IsExamine)
        {
            return;
        }
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1) {
            // Ony look around if the right finger is being tracked
            //Debug.Log("Rotating");
            LookAround();
        } 
    }


    void GetTouchInput() {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        //Debug.Log("Stopped tracking left finger");
                        isMoving = false;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * gameSettings.LookSensitivity/3 * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId) {

                        // calculating the position delta from the start position
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
            cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
            fpCameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
            fpc.transform.Rotate(transform.up, lookInput.x);
    }
}