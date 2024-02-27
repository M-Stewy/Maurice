using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move Speed")]
    public float baseMoveSpeed;
    public float SprintSpeed;
    public float CrouchSpeed;

    [Header("Jump stuff")]
    public float JumpPower;
    public float JumpTime;


}
