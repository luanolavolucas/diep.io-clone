using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(Character))]
public class CharacterControl : MonoBehaviourPunCallbacks
{
    public Character character;
    protected virtual void Awake()
    {
        character = GetComponent<Character>();
    }
}
