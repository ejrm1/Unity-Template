using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCDManager : MonoBehaviour
{
    private Dictionary<string, Ability> _abilities;

    private class Ability
    {
        public bool IsInCooldown;
        public float CooldownTime;
        public float OriginalCooldownTime;
    }

    private void Start()
    {
        _abilities = new Dictionary<string, Ability>
        {
            {
                "Teleporter",
                new Ability { IsInCooldown = false, CooldownTime = 0.5f, OriginalCooldownTime = 0.5f }
            },
            {
                "Shooting",
                new Ability { IsInCooldown = false, CooldownTime = 0.1f, OriginalCooldownTime = 0.1f }
            }
        };
    }

    private void Update()
    {
        foreach (var ability in _abilities.Values)
        {
            if (ability.IsInCooldown)
            {
                ability.CooldownTime -= Time.deltaTime;
                if (ability.CooldownTime <= 0)
                {
                    ability.IsInCooldown = false;
                    ability.CooldownTime = ability.OriginalCooldownTime;
                }
            }
        }
    }

    public void StartAbilityCooldown(string abilityName)
    {
        if (_abilities.TryGetValue(abilityName, out var ability))
        {
            ability.IsInCooldown = true;
        }
    }

    public bool IsAbilityInCooldown(string abilityName)
    {
        if (_abilities.TryGetValue(abilityName, out var ability))
        {
            return ability.IsInCooldown;
        }

        Debug.LogError("Ability " + abilityName + " not found.");
        return false;
    }
}
