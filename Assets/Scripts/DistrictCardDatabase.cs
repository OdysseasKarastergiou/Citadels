using UnityEngine;
using System.Collections.Generic;

public class DistrictCardDatabase : MonoBehaviour
{
    public static DistrictCardDatabase Instance { get; private set; }

    [System.Serializable]
    public class DistrictCardData
    {
        public string Name;
        public int Cost;
        public DistrictType Type;
        public string Description; // For special abilities
    }

    [Header("Noble Districts")]
    [SerializeField] private DistrictCardData[] nobleDistricts = new DistrictCardData[]
    {
        new DistrictCardData { Name = "Manor", Cost = 3, Type = DistrictType.Noble, Description = "Basic noble district" },
        new DistrictCardData { Name = "Castle", Cost = 4, Type = DistrictType.Noble, Description = "Basic noble district" },
        new DistrictCardData { Name = "Palace", Cost = 5, Type = DistrictType.Noble, Description = "Basic noble district" }
    };

    [Header("Religious Districts")]
    [SerializeField] private DistrictCardData[] religiousDistricts = new DistrictCardData[]
    {
        new DistrictCardData { Name = "Temple", Cost = 1, Type = DistrictType.Religious, Description = "Basic religious district" },
        new DistrictCardData { Name = "Church", Cost = 2, Type = DistrictType.Religious, Description = "Basic religious district" },
        new DistrictCardData { Name = "Monastery", Cost = 3, Type = DistrictType.Religious, Description = "Basic religious district" },
        new DistrictCardData { Name = "Cathedral", Cost = 5, Type = DistrictType.Religious, Description = "Basic religious district" }
    };

    [Header("Trade Districts")]
    [SerializeField] private DistrictCardData[] tradeDistricts = new DistrictCardData[]
    {
        new DistrictCardData { Name = "Tavern", Cost = 1, Type = DistrictType.Trade, Description = "Basic trade district" },
        new DistrictCardData { Name = "Market", Cost = 2, Type = DistrictType.Trade, Description = "Basic trade district" },
        new DistrictCardData { Name = "Trading Post", Cost = 2, Type = DistrictType.Trade, Description = "Basic trade district" },
        new DistrictCardData { Name = "Docks", Cost = 3, Type = DistrictType.Trade, Description = "Basic trade district" },
        new DistrictCardData { Name = "Harbor", Cost = 4, Type = DistrictType.Trade, Description = "Basic trade district" },
        new DistrictCardData { Name = "Town Hall", Cost = 5, Type = DistrictType.Trade, Description = "Basic trade district" }
    };

    [Header("Military Districts")]
    [SerializeField] private DistrictCardData[] militaryDistricts = new DistrictCardData[]
    {
        new DistrictCardData { Name = "Watchtower", Cost = 1, Type = DistrictType.Military, Description = "Basic military district" },
        new DistrictCardData { Name = "Prison", Cost = 2, Type = DistrictType.Military, Description = "Basic military district" },
        new DistrictCardData { Name = "Battlefield", Cost = 3, Type = DistrictType.Military, Description = "Basic military district" },
        new DistrictCardData { Name = "Fortress", Cost = 5, Type = DistrictType.Military, Description = "Basic military district" }
    };

    [Header("Special Districts")]
    [SerializeField] private DistrictCardData[] specialDistricts = new DistrictCardData[]
    {
        new DistrictCardData { Name = "Haunted City", Cost = 2, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" },
        new DistrictCardData { Name = "Keep", Cost = 3, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" },
        new DistrictCardData { Name = "School of Magic", Cost = 6, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" },
        new DistrictCardData { Name = "Library", Cost = 6, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" },
        new DistrictCardData { Name = "Great Wall", Cost = 6, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" },
        new DistrictCardData { Name = "University", Cost = 6, Type = DistrictType.Special, Description = "Can't be destroyed by the Warlord" }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public DistrictCardData GetDistrictData(string districtName)
    {
        foreach (var district in nobleDistricts)
        {
            if (district.Name == districtName) return district;
        }
        foreach (var district in religiousDistricts)
        {
            if (district.Name == districtName) return district;
        }
        foreach (var district in tradeDistricts)
        {
            if (district.Name == districtName) return district;
        }
        foreach (var district in militaryDistricts)
        {
            if (district.Name == districtName) return district;
        }
        foreach (var district in specialDistricts)
        {
            if (district.Name == districtName) return district;
        }
        return null;
    }

    public DistrictCardData[] GetAllDistricts()
    {
        var allDistricts = new List<DistrictCardData>();
        allDistricts.AddRange(nobleDistricts);
        allDistricts.AddRange(religiousDistricts);
        allDistricts.AddRange(tradeDistricts);
        allDistricts.AddRange(militaryDistricts);
        allDistricts.AddRange(specialDistricts);
        return allDistricts.ToArray();
    }
} 