using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ProjectX
{    
    public class InputHandler : MonoBehaviour
    {
        public static event Action CancelButtonClickEvent;
        
        #region Data
            [Space,Header("Input Data")]
            [SerializeField] private CameraInputData cameraInputData = null;
            [SerializeField] private MovementInputData movementInputData = null;
            [SerializeField] private InteractionInputData interactionInputData = null;
            [SerializeField] private Joystick moveJoy;
            
            private UiService uiService;
            private LookAroundController _lookAroundController;
            private CameraController _cameraController;
        #endregion

        #region BuiltIn Methods
            void Start()
            {
                cameraInputData.ResetInput();
                movementInputData.ResetInput();
                interactionInputData.ResetInput();
                
                uiService = FindObjectOfType<UiService>();
                _lookAroundController = FindObjectOfType<LookAroundController>().GetComponent<LookAroundController>();
                _cameraController = FindObjectOfType<CameraController>().GetComponent<CameraController>();
            }

            void Update()
            {
                if (!ExamineController.Instance.IsExamine)
                {
                    SetCameraControllerScript(movementInputData.IsMobile);
                    if (movementInputData.IsMobile)
                    {
                        GetMobileControls();
                    }
                    else
                    {
                        GetPcControls();
                    }
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


        void SetCameraControllerScript(bool isMobile)
        {
            _lookAroundController.enabled = isMobile;
            _cameraController.enabled = !isMobile;
        }
        //Мобильное управление
        void GetMobileControls()
        {
            
            //Отслеживание нажатия кнопки использовать
            interactionInputData.InteractedClicked = CrossPlatformInputManager.GetButtonDown("Use");
            interactionInputData.InteractedReleased = CrossPlatformInputManager.GetButtonUp("Use");
            
             //Движение камеры
            cameraInputData.InputVectorX = CrossPlatformInputManager.GetAxisRaw("Mouse X");
            cameraInputData.InputVectorY = CrossPlatformInputManager.GetAxisRaw("Mouse Y");
            
            //Движение
            movementInputData.InputVectorX = moveJoy.Horizontal;
            movementInputData.InputVectorY = moveJoy.Vertical;
            
            //Прыжок
            movementInputData.JumpClicked = CrossPlatformInputManager.GetButtonDown("Jump");
            //Красться
            movementInputData.CrouchClicked = CrossPlatformInputManager.GetButtonDown("Crouch");
            
            //Открытие инвентаря  отмена для UI
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                CancelButtonClickEvent?.Invoke();
            }
            
            if (CrossPlatformInputManager.GetButtonDown("Inventory"))
            {
                uiService.ShowInventoryWindow();
            }
        }
        
        void GetPcControls()
        {
            
            //Отслеживание нажатия кнопки использовать
            interactionInputData.InteractedClicked = Input.GetButtonDown("Use");
            interactionInputData.InteractedReleased = Input.GetButtonUp("Use");
            
            //Движение камеры
            cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
            cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");
            
            //Движение
            movementInputData.InputVectorX = Input.GetAxis("Horizontal");
            movementInputData.InputVectorY = Input.GetAxis("Vertical");
            
            //Прыжок
            movementInputData.JumpClicked = Input.GetButtonDown("Jump");
            //Красться
            movementInputData.CrouchClicked = Input.GetButtonDown("Crouch"); 
            
            //Открытие инвентаря  отмена для UI
            if (Input.GetButtonDown("Cancel"))
            {
                CancelButtonClickEvent?.Invoke();
            }
            
            if (Input.GetButtonDown("Inventory"))
            {
                uiService.ShowInventoryWindow();
            }
        }
        #endregion
    }
}