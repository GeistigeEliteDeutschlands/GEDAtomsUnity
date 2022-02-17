using UnityEngine;

public class NPCNuclide : Nuclide
{
    new void Start()
    {
        base.Start();

        uint randomA = (uint)Random.Range(1, 5);

        nuclideData = ndb.getNuclideData(randomA, randomA - 1);
        updateSprite();

        GetComponent<EnemyRaycast>().myMass = nuclideData.A;
    }
}