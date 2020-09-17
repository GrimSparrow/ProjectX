using System;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField]private Quest quest;

    public Quest Quest
    {
        get => quest;
        set => quest = value;
    }

    private static  QuestController instance;
    public  static  QuestController Instance
    {
        get => instance;
        set => instance = value;
    }
    
    private void Start()
    {
        if (Instance == null) 
        { 
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
        quest.Restart();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            NotificationManager.Instance.SetNotification(GetHint());
        }
    }

    public TaskBaseNode GetCurrentTask()
    {
        return quest.current;
    }

    public void MoveToNextTask(TaskBaseNode task)
    {
        if (task != null && task == GetCurrentTask())
        {
            var currentNode = quest.current as TaskNode;
            if (currentNode != null) currentNode.TryToComplete();
        }
    }

    public string GetHint()
    {
        TaskNode currentNode = quest.current as TaskNode;

        return currentNode != null ? currentNode.HelpText : string.Empty;
    }
}
