using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ProjectX
{    
    public class InputHandler : MonoBehaviour
    {
        #region Data
            [Space,Header("Input Data")]
            [SerializeField] private CameraInputData cameraInputData = null;
            [SerializeField] private MovementInputData movementInputData = null;
            [SerializeField] private InteractionInputData interactionInputData = null;
            [SerializeField] private Joystick moveJoy;
        #endregion

        #region BuiltIn Methods
            void Start()
            {
                cameraInputData.ResetInput();
                movementInputData.ResetInput();
                interactionInputData.ResetInput();
            }

            void Update()
            {
                if (!ExamineController.Instance.IsExamine)
                {
                    GetCameraInput();
                    GetMovementInputData();
                    GetInteractionInputData();
                }
                else
                {
                    cameraInputData.ResetInput();
                    movementInputData.ResetInput();
                    interactionInputData.ResetInput();
                }
            }
        #endregion

        #region Custom Methods
            void GetInteractionInputData()
            {
                interactionInputData.InteractedClicked = CrossPlatformInputManager.GetButtonDown("Use");
                interactionInputData.InteractedReleased = CrossPlatformInputManager.GetButtonUp("Use");
            }

            void GetCameraInput()
            {
                cameraInputData.InputVectorX = CrossPlatformInputManager.GetAxisRaw("Mouse X");
                cameraInputData.InputVectorY = CrossPlatformInputManager.GetAxisRaw("Mouse Y");
                

                cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
                cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
            }

            void GetMovementInputData()
            {
                movementInputData.InputVectorX = moveJoy.Horizontal;
                movementInputData.InputVectorY = moveJoy.Vertical;

                movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
                movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

                if(movementInputData.RunClicked)
                    movementInputData.IsRunning = true;

                if(movementInputData.RunReleased)
                    movementInputData.IsRunning = false;

                movementInputData.JumpClicked = CrossPlatformInputManager.GetButtonDown("Jump");
                movementInputData.CrouchClicked = CrossPlatformInputManager.GetButtonDown("Crouch");
            }
        #endregion
    }
}