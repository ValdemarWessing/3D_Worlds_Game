using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Singleton instance
    public static GameState Instance { get; private set; }

    private readonly Dictionary<string, bool> flags = new Dictionary<string, bool>();
    
    
    public event Action<string, bool> OnFlagChanged;

    void Awake()
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

    public void SetFlag(string key, bool value)
    {
        flags[key] = value;
        OnFlagChanged?.Invoke(key, value);
    }

    public bool GetFlag(string key)
    {
        return flags.TryGetValue(key, out var v) && v;
    }
}