using System.IO;
using System.Collections.Generic;
using System;

// NuclideDatabase is a Singleton.
// Use NuclideDatabase.instance to get an instance
// it cannot be created as an Object
public class NuclideDatabase
{
    private static NuclideDatabase _instance = null;
    public static NuclideDatabase instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NuclideDatabase();
            }
            return _instance;
        }
    }

    public enum DecayMode
    {
        NONE = -1,
        ALPHA,
        B_MINUS,
        TWO_B_MINUS,
        B_PLUS,
        TWO_B_PLUS,
        PROTON,
        TWO_PROTON,
        NEUTRON,
        TWO_NEUTRON
    }

    // somehow cannot be declared const ???
    static readonly Dictionary<string, DecayMode> STR_TO_DECAY_MODE = new Dictionary<string, DecayMode>()
    {
        {"None", DecayMode.NONE},
        {"A", DecayMode.ALPHA},
        {"B-", DecayMode.B_MINUS},
        {"2B-", DecayMode.TWO_B_MINUS},
        {"EC", DecayMode.B_PLUS},
        {"2EC", DecayMode.TWO_B_PLUS},
        {"P", DecayMode.PROTON},
        {"2P", DecayMode.TWO_PROTON},
        {"N", DecayMode.NEUTRON},
        {"2N", DecayMode.TWO_NEUTRON}
    };
    
    public class NuclideData
    {
        public string symbol;
        public uint A; // mass
        public uint Z; // atomic number
        public float halfLife;
        public DecayMode decayMode;

        public NuclideData(string symbol_, uint A_, uint Z_, float halfLife_, DecayMode decayMode_)
        {
            symbol = symbol_;
            A = A_;
            Z = Z_;
            halfLife = halfLife_;
            decayMode = decayMode_;
        }
    }

    private Dictionary<Tuple<uint, uint>, NuclideData> nuclideDict;

    private Dictionary<string, string> symbolToName;

    private NuclideDatabase()
    {
        nuclideDict = new Dictionary<Tuple<uint, uint>, NuclideData>();

        string path = "Assets/NuclideData/NuclideData.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] raw_nuclide_info = line.Split('\t');

                string symbol = raw_nuclide_info[0];

                uint Z = uint.Parse(raw_nuclide_info[1]);
                uint A = uint.Parse(raw_nuclide_info[2]);

                float halfLife;

                if (raw_nuclide_info[3] == "inf")
                {
                    halfLife = float.PositiveInfinity;
                }
                else
                {
                    halfLife = float.Parse(raw_nuclide_info[3]);
                }

                DecayMode decayMode = STR_TO_DECAY_MODE[raw_nuclide_info[4]];


                nuclideDict.Add(new Tuple<uint, uint>(A, Z), new NuclideData(symbol, A, Z, halfLife, decayMode));
            }
        }

        symbolToName = new Dictionary<string, string>();

        path = "Assets/NuclideData/ElementNames.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] raw_element_name_data = line.Split('\t');

                string name = raw_element_name_data[0];
                string symbol = raw_element_name_data[1];

                symbolToName.Add(symbol, name);
            }
        }
    }

    // A = mass; Z = atomic number. E.g. Uranium-235: A = 235, Z = 92
    public NuclideData getNuclideData(uint A, uint Z)
    {
        return nuclideDict[new Tuple<uint, uint>(A, Z)];
    }

    public bool checkExistence(uint A, uint Z)
    {
        return nuclideDict.ContainsKey(new Tuple<uint, uint>(A, Z));
    }

    public NuclideData getDecayProduct(NuclideData nuclide)
    {
        switch (nuclide.decayMode)
        {
            case DecayMode.ALPHA:
                return getNuclideData(nuclide.A - 4, nuclide.Z - 2);
            case DecayMode.B_MINUS:
                return getNuclideData(nuclide.A, nuclide.Z + 1);
            case DecayMode.TWO_B_MINUS:
                return getNuclideData(nuclide.A, nuclide.Z + 2);
            case DecayMode.B_PLUS:
                return getNuclideData(nuclide.A, nuclide.Z - 1);
            case DecayMode.TWO_B_PLUS:
                return getNuclideData(nuclide.A, nuclide.Z - 2);
            case DecayMode.PROTON:
                return getNuclideData(nuclide.A - 1, nuclide.Z - 1);
            case DecayMode.TWO_PROTON:
                return getNuclideData(nuclide.A - 2, nuclide.Z - 2);
            case DecayMode.NEUTRON:
                return getNuclideData(nuclide.A - 1, nuclide.Z);
            case DecayMode.TWO_NEUTRON:
                return getNuclideData(nuclide.A - 2, nuclide.Z);
            default:
                return nuclide;
        }
    }

    public string getName(string symbol)
    {
        return symbolToName[symbol];
    }
}