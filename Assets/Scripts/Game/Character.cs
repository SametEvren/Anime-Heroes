using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public int id;
    public string name;
    public Sprite artwork;
    public int damage;
    public int health;
}
