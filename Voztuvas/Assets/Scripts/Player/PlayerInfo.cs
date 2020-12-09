using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo: MonoBehaviour
{
    public static PlayerInfo instance;
    public GameObject player;


    private void Awake()
    {
        instance = this;
    }

}
