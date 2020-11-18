﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public enum weapons
{
    Sword = 0,
    Rifle = 1,
    Shotgun = 2,
    Pistol
}

[CreateAssetMenu(fileName = "Meele_Weapon", menuName = "ScriptableObjects/Meele_Weapon", order = 1)]
public class Meele_Weapon_Object : ScriptableObject
{
    public weapons Type;
    public GameObject Weapon;
    public bool Meele;
    public float DMG;
    public int hitCombo;
    public float cooldown;
}
