using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GnomeClassique : Gnome
{
    void Start(){
        // do the initialisation of THe gnome class inherited
        base.Initiate();
    }

    void Update()
    {
        base.Move();
    }
}
