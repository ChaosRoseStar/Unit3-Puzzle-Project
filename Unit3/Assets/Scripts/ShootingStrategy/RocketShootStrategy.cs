using UnityEngine;

public class RocketShootStrategy : MonoBehaviour, IShootStrategy
{
    private GameObject weaponSignifier;

    public RocketShootStrategy(GameObject signifier)
    {
        weaponSignifier = signifier;
        Debug.Log("Rocket Strategy Selected");
        weaponSignifier.GetComponent<Renderer>().material.color = Color.blue;
    }


    public void Shoot(ShootAbility ability)
    {
        Rigidbody clonedProjectile = ability.rocketPool.RetrieveAvailableBullet().GetRigidbody();

        if (clonedProjectile == null)
        {
            Debug.LogError("No objects available in the object pool.");
            return;
        }

        clonedProjectile.position = ability.firePoint.position;
        clonedProjectile.rotation = ability.firePoint.rotation;
        clonedProjectile.AddForce(ability.firePoint.forward * ability.GetShootForce());
    }
}
