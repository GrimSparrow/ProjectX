using System;
using ProjectX;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ExamineController : MonoBehaviour
{
    private Vector3 posLatFrame;
    [SerializeField] private float speed = 100f;
   
    private bool isExamine;
    
    private bool isDragged;
    
    [SerializeField] private Canvas ExamineCanvas;
    [SerializeField] private TextMeshProUGUI ObjectName;
    [SerializeField] private GameObject NoteContainer;
     private InteractionUIPanel InteractionUiPanel;

    private static  ExamineController instance;
    
    public  static  ExamineController Instance
    {
        get => instance;
        set => instance = value;
    }
    
    private ExaminableBase examineObject;

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
    void LateUpdate () {

        if (Input.GetKey(KeyCode.Escape) || CrossPlatformInputManager.GetButtonDown("Use") && isExamine)
        {
            StopExamining();
        }
        isDragged = Input.GetMouseButton(0);
    }
    public void Examine(ExaminableBase examine)
    {
        var cam = GetComponent<Canvas>();
        cam.worldCamera = Camera.main;
        
        examineObject = examine;
        ChangeCursorState(true);
        examineObject.transform.parent = ExamineCanvas.transform;
        examineObject.transform.localPosition = Vector3.zero;
        //examineObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        ObjectName.text = examine.ObjectName;
        examineObject.Use();
    }

    public void StopExamining()
    {
        NoteContainer.gameObject.SetActive(false);
        ChangeCursorState(false);
        if (examineObject == null) return;
        Destroy(examineObject.gameObject);
    }
    private void FixedUpdate()
    {
        if (examineObject == null || !isDragged) return;
        
        examineObject.Drag(speed);
    }

    void ChangeCursorState(bool isActive)
    {
        //Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        //Cursor.visible = isActive;
        IsExamine = isActive;
        ObjectName.text = string.Empty;
        InteractionUiPanel = FindObjectOfType<InteractionUIPanel>();
        InteractionUiPanel.SetVisibility(!isActive);
    }

    public void ShowNote(Note note)
    {
        ChangeCursorState(true);
        NoteContainer.gameObject.SetActive(true);
        var template = NoteContainer.GetComponent<Image>();
        var text = NoteContainer.GetComponentInChildren<TextMeshProUGUI>();
        template.sprite = note.Template;
        text.SetText(note.Text);
    }
}
