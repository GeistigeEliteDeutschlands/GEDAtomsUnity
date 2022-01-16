using UnityEngine;
using System;

public class Nuclide : MonoBehaviour
{
    protected NuclideDatabase ndb;
    public NuclideDatabase.NuclideData nuclideData;

    protected void Start()
    {
        ndb = NuclideDatabase.instance;
    }

    protected void Update()
    {
        float scale = Mathf.Pow(nuclideData.A, 1f / 3f);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "nuclide")
        {
            NuclideDatabase.NuclideData otherNuclideData = collision.gameObject.GetComponent<Nuclide>().nuclideData;

            if(ndb.checkExistence(otherNuclideData.A + nuclideData.A, otherNuclideData.Z + nuclideData.Z))
            {
                if(otherNuclideData.A < nuclideData.A)
                {
                    nuclideData = ndb.getNuclideData(otherNuclideData.A + nuclideData.A, otherNuclideData.Z + nuclideData.Z);
                }
                else if(otherNuclideData.A > nuclideData.A)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}