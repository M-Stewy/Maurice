using UnityEngine;

/// <summary>
/// Made by Stewy 
///     Edited by: 
///     
///  This Sript holds all the information about the player
///     such as their move speeds,  gravity values, current ability, health etc.
///  Because it is a scriptableObject it can be easily modified by other scripts without needing to
///     get refernces to the gameObject with the script and then the componet of the script itself
///  
/// </summary>

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Different Abilitys and Stuff")]
    [Space]

    [Header("Ability that is currently in use")]
    [Tooltip("only one of these should be true at a time")]
    public bool AblityIsGrapple;
    


    [Space(5)]
    [Header("Move Speed")]
    public float baseMoveSpeed = 30f;
    public float SprintSpeed = 60f;
    public float CrouchSpeed = 20f;
    public float SlideSpeedBoost = 5f;

    [Space]
    [Header("Jump stuff")]
    public float JumpPower = 25f;
    public float JumpTime = 75f;
    public int TotalJumps = 1;
    public float CyoteTime = 10;

    [Space(5)]
    [Header("Drag Values")]
    public float GroundDrag = 1f;
    public float SlopeDrag = 0.0001f;
    public float AirDrag = 0.75f;
    public float SlideDrag = 0.001f;
    public float IdleDrag = 5f;

    [Space]
    [Header("Gravity Values")]
    public float GroundGravity = 1f;
    public float SlopeGravity = 3f;
    public float AirGravity = 5f;


    [Space]
    [Header("Grapple Stuff")]
    public Sprite GrapplePointSprite;
    [Tooltip("Uncheck Ground if you dont want player to be able to grapple to the ground")]
    public LayerMask LaymaskGrapple;
    public float GrappleReelSpeed = 5f; //This gets weird at higher values need to firgure out why
    public float GrappleSwingSpeed = 20f;

    //[HideInInspector] 
    [Space]
    [Header("DO NOT CHANGE THESE FROM DEFAULT, \n" +
        "           (unless we change player size)")]
    public Vector2 CrouchOffset = new Vector2(-0.27f, -0.32f);
    //[HideInInspector]
    public Vector2 CrouchSize = new Vector2(0.4f, 0.65f);
   // [HideInInspector]
    public Vector2 NormalOffset = new Vector2(-0.27f, -0.32f);
    // [HideInInspector]
    public Vector2 NormalSize = new Vector2(0.85f, 1.31f);



    [Header("Debug Stuff")]
    public bool ShowEnterStateInConsole;
}
