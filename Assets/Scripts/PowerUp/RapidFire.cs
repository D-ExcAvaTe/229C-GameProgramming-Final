using UnityEngine;

class RapidFire : PowerUp
{
    [SerializeField] float multiplier = 0.15f;
    [SerializeField] float duration = 3;
    public override void DoPowerUp(Player player)
    {
        player.projectile.StartFireCoroutine(multiplier, duration);
    }
}
