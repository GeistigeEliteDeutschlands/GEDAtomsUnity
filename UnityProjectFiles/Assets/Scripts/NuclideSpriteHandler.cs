using System.Collections.Generic;
using System;
using UnityEngine;

public class NuclideSpriteHandler : MonoBehaviour
{
    private static NuclideSpriteHandler _instance = null;
    public static NuclideSpriteHandler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NuclideSpriteHandler();
            }
            return _instance;
        }
    }

    private TextureHandler textureHandler;

    const float PHI = 3.2360679f;
    const float TEXTURE_RADIUS = 10f;

    private Dictionary<Tuple<uint, uint>, Sprite> spriteCache;

    NuclideSpriteHandler()
    {
        textureHandler = TextureHandler.instance;
        spriteCache = new Dictionary<Tuple<uint, uint>, Sprite>();
    }

    // Generates a Sprite for the given A and Z.
    // If cache is true the sprite will be stored.
    public Sprite generateNuclideSprite(uint A, uint Z, bool cache = true)
    {
        float point_cloud_radius = Mathf.Pow(A, 1f / 3) * TEXTURE_RADIUS; // radius of the middle points
        float radius = point_cloud_radius + TEXTURE_RADIUS; // radius including the size of the particles
        int iRadius = (int)radius;

        // create a clear texture of the size 2 * radius
        Texture2D outTexture = new Texture2D(2 * iRadius, 2 * iRadius);
        Color[] pixels = new Color[outTexture.width * outTexture.height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }
        outTexture.SetPixels(pixels);


        int neutrons = (int)A - (int)Z;
        int protons = (int)Z;

        for (int i = 0; i < A; i++)
        {
            // fibonacci spiral
            float r = Mathf.Pow((float)i / A, 1f / 2) * point_cloud_radius;
            float theta = Mathf.PI * PHI * i;
            float xPosition = r * Mathf.Cos(theta) + radius - TEXTURE_RADIUS;
            float yPosition = r * Mathf.Sin(theta) + radius - TEXTURE_RADIUS;

            // pick proton/neutron
            Texture2D tex;
            if (UnityEngine.Random.Range(0, neutrons + protons) < neutrons)
            {
                tex = textureHandler.getTexture("neutron");
                neutrons--;
            }
            else
            {
                tex = textureHandler.getTexture("proton");
                protons--;
            }

            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    Color color = tex.GetPixel(x, y);
                    if (color.a != 0 && outTexture.GetPixel((int)xPosition + x, (int)yPosition + y).a == 0)
                    {
                        outTexture.SetPixel((int)xPosition + x, (int)yPosition + y, color);
                    }
                }
            }
        }

        outTexture.Apply();

        Sprite sprite = Sprite.Create(outTexture, new Rect(0, 0, outTexture.width, outTexture.height), new Vector2(.5f, .5f));

        if (cache)
        {
            spriteCache.Add(new Tuple<uint, uint>(A, Z), sprite);
        }

        return sprite;
    }

    public Sprite getNuclideSprite(uint A, uint Z)
    {
        Tuple<uint, uint> key = new Tuple<uint, uint>(A, Z);
        if (spriteCache.ContainsKey(key))
        {
            return spriteCache[key];
        }
        return generateNuclideSprite(A, Z);
    }
}
