using System.Collections.Generic;
using UnityEngine;

public class TextureHandler
{
    private static TextureHandler _instance = null;
    public static TextureHandler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TextureHandler();
            }
            return _instance;
        }
    }

    private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

    TextureHandler()
    {
        // You can preload some Textures here:
        loadTexture("neutron");
        loadTexture("proton");
    }
    
    // loads Texture2D to internal texture storage.
    private bool loadTexture(string name)
    {
        Texture2D tex = Resources.Load<Texture2D>(name);
        if (tex == null) return false;
        textures.Add(name, tex);
        return true;
    }

    // Returns a Texture2D. Loads it, if it hasn't been loaded before.
    // Returns null if the name could not be found.
    public Texture2D getTexture(string name)
    {
        if (textures.ContainsKey(name))
        {
            return textures[name];
        }

        if (loadTexture(name))
        {
            return textures[name];
        }
        
        return null;
    }
}
