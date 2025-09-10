using UnityEngine;

public class BulletShootStrategy : MonoBehaviour, IShootStrategy
{
    private GameObject weaponSignifier;

    public BulletShootStrategy(GameObject signifier)
    {
        weaponSignifier = signifier;
        Debug.Log("Bullet Strategy Selected");
        weaponSignifier.GetComponent<Renderer>().material.color = Color.red;
    }


    public void Shoot(ShootAbility ability)
    {
        Rigidbody clonedProjectile = ability.bulletPool.RetrieveAvailableBullet().GetRigidbody();

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
