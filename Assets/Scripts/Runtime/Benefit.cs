using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benefit : Bullet
{
       List<BenefitType> possibleBenefits = new List<BenefitType>
    {
        BenefitType.health,
        BenefitType.rareBullet,
        BenefitType.epicBullet,
        BenefitType.legendaryBullet,
        BenefitType.rareWeapon,
        BenefitType.epicWeapon,
        BenefitType.legendaryWeapon
    };
    public override void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.TryGetComponent(out Player player))
         {
            int benefit = UnityEngine.Random.Range(0, possibleBenefits.Count);
            Debug.Log(possibleBenefits[benefit].ToString());
            player.ApplyBenefit(possibleBenefits[benefit]);
            gameObject.SetActive(false);
         }
    }
   

}

public enum BenefitType
{
    health, rareBullet, epicBullet, legendaryBullet, rareWeapon, epicWeapon, legendaryWeapon
}