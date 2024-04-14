using UnityEngine;

public class ToZeroG : MonoBehaviour
{
    [SerializeField]
    private PlayerData Norm_PD;
    [SerializeField]
    private PlayerData ZeroG_PD;

   public void SetPlayerToZeroG()
    {
        GetComponent<Player>().UpdateStateMachine();
        GetComponent<Player>().playerData = ZeroG_PD;
    }

    public void SetPlayerToNormG()
    {
        GetComponent<Player>().UpdateStateMachine();
        GetComponent<Player>().playerData = Norm_PD;
    }
}
