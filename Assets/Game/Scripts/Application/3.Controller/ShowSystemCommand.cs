using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShowSystemCommand : Controller
{

    public override void Execute(object data)
    {
        Time.timeScale = 0;
    }

}
