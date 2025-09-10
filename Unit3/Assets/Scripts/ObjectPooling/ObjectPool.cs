using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PooledObject bulletPrefab;
    [SerializeField] private int amountOfClones = 20;

    [SerializeField] private List<PooledObject> availableBullets;
    [SerializeField] private List<PooledObject> unavailableBullets;

    private void Start()
    {
        int currentAmountOfClones = 0; 
        while(currentAmountOfClones < amountOfClones)
        {
            AddElementToPool();

            currentAmountOfClones++;
        }
    }

    private void AddElementToPool()
    {
        PooledObject clone = Instantiate(bulletPrefab);

        clone.LinkToPool(this);

        clone.gameObject.SetActive(false);
        clone.transform.SetParent(transform);
        availableBullets.Add(clone);
    }

    public PooledObject RetrieveAvailableBullet()
    {
        if(availableBullets.Count == 0)
        {
            AddElementToPool();
        }

        PooledObject first = availableBullets[0];

        availableBullets.RemoveAt(0);
        unavailableBullets.Add(first);

        first.gameObject.SetActive(true);

        return first;
    }


    public void ResetBullet(PooledObject bulletToReset)
    {
        unavailableBullets.Remove(bulletToReset);
        availableBullets.Add(bulletToReset);

        bulletToReset.GetRigidbody().linearVelocity = Vector3.zero;

        bulletToReset.gameObject.SetActive(false);
    }
}
