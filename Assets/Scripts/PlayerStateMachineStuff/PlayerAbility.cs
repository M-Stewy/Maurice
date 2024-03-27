using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility
{
    public bool isUnlocked { get; private set; }
    public bool isEquiped { get; private set; }

    public string name { get; private set; }
    public string actionAnim { get; private set; }
    public string animBool { get; private set; }

    public void SetUnlocked(bool Q)
    {
        isUnlocked = Q;
    }
    public void SetEquiped(bool Q)
    {
        isEquiped = Q;
    }
    public void ChangeSprite(GameObject hand)
    {
        hand.GetComponent<Animator>().SetBool(animBool, isEquiped);
    }
    public void DoAction(GameObject hand, bool isTrue)
    {
        hand.GetComponent<Animator>().SetBool(animBool, !isTrue);
        hand.GetComponent<Animator>().SetBool(actionAnim, isTrue);
        Debug.Log("ACtionTest");
    }

    public PlayerAbility(bool isUnlocked, bool isEquiped, string name, string animBool, string actionAnim)
    {
        this.isUnlocked = isUnlocked;
        this.isEquiped = isEquiped;
        this.name = name;
        this.animBool = animBool;
        this.actionAnim = actionAnim;
    }
}
