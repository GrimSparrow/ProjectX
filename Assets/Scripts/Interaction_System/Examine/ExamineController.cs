using System;
using System.Collections;
using System.Collections.Generic;
using ProjectX;
using UnityEngine;

public class ExamineController : MonoBehaviour
{
    private Vector3 posLatFrame;
    [SerializeField] private float speed = 100f;
   
    private GameObject examineObject = null;
    private bool isDragged;
    private bool isExamine;
    [SerializeField]private Canvas ExamineCanvas;
    [SerializeField] private GameObject slot;

    private static  ExamineController instance;
    
    public  static  ExamineController Instance
    {
        get => instance;
        set => instance = value;
    }

    public bool IsExamine
    {
        get => isExamine;
        set => isExamine = value;
    }

    void Start()
    {
        if (Instance == null) 
        { 
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

  
    
    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            StopExamining();
        }

        isDragged = Input.GetMouseButton(0);
    }
    public void Examine(InteractableBase interactable)
    {
        var obj = interactable.gameObject;
        if (obj == null) return;
        examineObject =  Instantiate(obj, Vector3.zero, obj.transform.rotation);
        var collider = examineObject.GetComponent<Collider>();
        var rigid = examineObject.GetComponent<Rigidbody>();
        if (collider != null )
        {
            collider.enabled = false;
        }
        if (rigid != null )
        {
            Destroy(rigid);
        }
        
        examineObject.transform.parent = ExamineCanvas.transform;
        examineObject.transform.localPosition = Vector3.zero;

        examineObject.transform.Rotate(Vector3.zero);
        ChangeCursorState(true);
    }

    public void StopExamining()
    {
        if (examineObject == null) return;
        Destroy(examineObject);
        ChangeCursorState(false);
    }
    private void FixedUpdate()
    {
        if (examineObject == null || !isDragged) return;
        
        float rotX = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * speed * Mathf.Deg2Rad;
        
        examineObject.transform.RotateAround(Vector3.up, -rotX);
        examineObject.transform.RotateAround(Vector3.right, rotY);
    }

    void ChangeCursorState(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
        IsExamine = isActive;
    }
}
