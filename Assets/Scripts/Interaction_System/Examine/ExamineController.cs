using ProjectX;
using TMPro;
using UnityEngine;

public class ExamineController : MonoBehaviour
{
    private Vector3 posLatFrame;
    [SerializeField] private float speed = 100f;
   
    private bool isExamine;
    
    private bool isDragged;
    
    [SerializeField] private Canvas ExamineCanvas;
    [SerializeField] private TextMeshProUGUI ObjectName;
    [SerializeField] private InteractionUIPanel InteractionUiPanel;

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
    void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            StopExamining();
            examineObject.Use();
        }

        if (Input.GetKey(KeyCode.F) && examineObject != null && isExamine)
        {
            examineObject.Use();
            StopExamining();
        }
        isDragged = Input.GetMouseButton(0);
    }
    public void Examine(ExaminableBase examine)
    {
        examineObject = examine;
        ChangeCursorState(true);
        examineObject.transform.parent = ExamineCanvas.transform;
        examineObject.transform.localPosition = Vector3.zero;
        examineObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        ObjectName.text = examine.ObjectName;
    }

    public void StopExamining()
    {
        if (examineObject == null) return;
        Destroy(examineObject.gameObject);
        ChangeCursorState(false);
    }
    private void FixedUpdate()
    {
        if (examineObject == null || !isDragged) return;
        
        examineObject.Drag(speed);
    }

    void ChangeCursorState(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
        IsExamine = isActive;
        ExamineCanvas.gameObject.SetActive(isActive);
        InteractionUiPanel.SetVisibility(!isActive);
    }
}
