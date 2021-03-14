using UnityEngine;

public class AmmoBehaviour : AbstractItem
{
    public int IncreasedAmmo { get; set; } = 10;

    public override void InvokeActionOnPlayer(Collider other)
    {
        other.GetComponent<PlayerShoot>().AddAmmo(IncreasedAmmo);
    }
}