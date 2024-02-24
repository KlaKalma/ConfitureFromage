using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeInsecte : Gnome
{

    // non serialized
    public override int MoneyOnDeath { get; set; } = 20;

    protected override void Start(){
        // do the initialisation of THe gnome class inherited
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
