using Lowscope.Saving;
using UnityEngine;

namespace Lowscope.Saving.Components
{
    /// <summary>
    /// Example class of how to store a rotation.
    /// Also very useful for people looking for a simple way to store a rotation.
    /// </summary>

    [AddComponentMenu("Saving/Components/Save Transform"), DisallowMultipleComponent]
    public class SaveTransform : MonoBehaviour, ISaveable
    {
        private Vector3 lastRotation;
        private Vector3 activeRotation;

        Vector3 lastPosition;
        
        [System.Serializable]
        public struct SaveData
        {
            public Vector3 rotation;
            public Vector3 position;
        }

        public void OnLoad(string data)
        {
            lastRotation = JsonUtility.FromJson<SaveData>(data).rotation;
            transform.rotation = Quaternion.Euler(lastRotation);
            
            var pos = JsonUtility.FromJson<SaveData>(data).position;
            transform.position = pos;
            lastPosition = pos;
        }

        public string OnSave()
        {
            lastRotation = activeRotation;
            lastPosition = transform.position;
            return JsonUtility.ToJson(new SaveData() { rotation = transform.rotation.eulerAngles, position = lastPosition});
        }

        public bool OnSaveCondition()
        {
            activeRotation = transform.rotation.eulerAngles;
            return (lastRotation != activeRotation) || lastPosition != transform.position;
        }
    }
}