using System;
using System.Collections.Generic;
using System.Linq;
using ProjectX;
using UnityEngine;

public class GroundBehaviour : MonoBehaviour
{
    [SerializeField] private List<GroundType> GroundTypes = new List<GroundType>();
    [SerializeField] private FirstPersonController FPC;
    [SerializeField] private string currentGround;

    private void Start()
    {
        SetGroundType(GetGroundType("Default")); 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        SetGroundType(GetGroundType(hit.transform.tag));
    }

    private GroundType GetGroundType(string tag)
    {
        var type = GroundTypes.FirstOrDefault(g => g.name == tag);
        return type ?? GroundTypes.First(g => g.name == "Default");
    }

    public void SetGroundType(GroundType ground)
    {
        if (currentGround != ground.name)
        {
            FPC.groundType = ground;
            currentGround = ground.name;
        }
    }
}

[Serializable]
public class GroundType
{
    public string name;
    public AudioClip[] m_FootstepSounds;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public AudioClip m_JumpSound;
    public AudioClip m_LandSound; 
}
