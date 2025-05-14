using UnityEngine;

class Healing : PowerUp
{
    [SerializeField] int healAmount = 20;
    public override void DoPowerUp(Player player)
    {
        Debug.Log(player.health);
        player.TakeHeal(healAmount);
        Debug.Log(player.health);
    }
    
}
