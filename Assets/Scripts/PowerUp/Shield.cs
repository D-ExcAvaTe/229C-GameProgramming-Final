using UnityEngine;

class Shield : PowerUp
{
    [SerializeField] float duration = 10;
    public override void DoPowerUp(Player player)
    {
        AudioManager.instance.PlaySFX(17);
        player.projectile.StartShieldCoroutine(duration);
    }
}
