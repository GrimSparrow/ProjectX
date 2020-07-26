using ProjectX;
using UnityEngine;

namespace Lowscope.Saving.Components
{
    [AddComponentMenu("Saving/Components/Save Player State"), DisallowMultipleComponent]
    public class SavePlayerState : MonoBehaviour, ISaveable
    {
        private bool isCrouching;

        [SerializeField] private MovementInputData movementInputData;
        
        [System.Serializable]
        public struct SaveData
        {
            public bool isCrouching;
        }

        public void OnLoad(string data)
        {
            isCrouching = JsonUtility.FromJson<SaveData>(data).isCrouching;
            movementInputData.IsCrouching = isCrouching;
            FindObjectOfType<FirstPersonController>().InvokeCrouchRoutine();
        }

        public string OnSave()
        {
            isCrouching = movementInputData.IsCrouching;
            return JsonUtility.ToJson(new SaveData()
                {isCrouching = movementInputData.IsCrouching});
        }

        public bool OnSaveCondition()
        {
            return true;
        }
    }
}