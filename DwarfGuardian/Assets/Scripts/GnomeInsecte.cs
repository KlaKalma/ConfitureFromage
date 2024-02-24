using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeInsecte : Gnome
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
