using UnityEngine;

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
    public float baseMoveSpeed;
    public float SprintSpeed;
    public float CrouchSpeed;
    public float SlideSpeedBoost;

    [Space]
    [Header("Jump stuff")]
    public float JumpPower;
    public float JumpTime;
    public int TotalJumps = 1;
    public float CyoteTime = 10;

    [Space(5)]
    [Header("Drag Values")]
    public float GroundDrag = 1;
    public float SlopeDrag = 0;
    public float AirDrag;
    public float SlideDrag;
    public float IdleDrag;

    [Space]
    [Header("Gravity Values")]
    public float GroundGravity = 1f;
    public float SlopeGravity;
    public float AirGravity = 5f;


    [Space]
    [Header("Grapple Stuff")]
    public Sprite GrapplePointSprite;
    public LayerMask LaymaskGrapple;
    public float GrappleReelSpeed;
    public float GrappleSwingSpeed;

    [HideInInspector]
    public Vector2 CrouchOffset = new Vector2(0,.5f);
    [HideInInspector]
    public Vector2 CrouchSize = new Vector2(1, 1f);
    [HideInInspector]
    public Vector2 NormalOffset = new Vector2(0, 0);
    [HideInInspector]
    public Vector2 NormalSize = new Vector2(1, 2);
}
