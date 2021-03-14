using UnityEngine;

public class HPPointBehaviour : AbstractItem
{
    public int IncreasedHP { get; set; } = 25;

    public override void InvokeActionOnPlayer(Collider other)
    {
        other.GetComponent<PlayerHP>().IncreaseHP(IncreasedHP);
    }
}