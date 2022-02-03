using UnityEngine;

public class PlayerNuclide : Nuclide
{
    private string symbolold = "xy";
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
        if(nuclideData.symbol != symbolold)
        {
            Debug.Log(nuclideData.symbol);
            symbolold = nuclideData.symbol;
        }
    }
}
