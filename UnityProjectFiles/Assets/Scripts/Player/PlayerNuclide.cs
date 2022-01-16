using UnityEngine;

public class PlayerNuclide : Nuclide
{
    new void Start()
    {
        base.Start();

        nuclideData = ndb.getNuclideData(4, 2);
    }

    private void OnDestroy()
    {
        Debug.Log("Lost B#tch");
    }

    private new void Update()
    {
        base.Update();
        Debug.Log(nuclideData.symbol);
    }
}
