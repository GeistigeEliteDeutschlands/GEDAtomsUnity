using UnityEngine;

public class Nuclide : MonoBehaviour
{
    protected NuclideDatabase ndb;
    public NuclideDatabase.NuclideData nuclideData;

    protected SpriteRenderer spriteRenderer;
    private NuclideSpriteHandler nuclideSpriteHandler;

    private const float SCALE_FACTOR = 3f;

    protected void Start()
    {
        ndb = NuclideDatabase.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        nuclideSpriteHandler = NuclideSpriteHandler.instance;

        transform.localScale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, 1);
    }

    protected void Update()
    {

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
                    updateSprite();
                }
                else if(otherNuclideData.A > nuclideData.A)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    protected void updateSprite()
    {
        spriteRenderer.sprite = nuclideSpriteHandler.getNuclideSprite(nuclideData.A, nuclideData.Z);
    }
}