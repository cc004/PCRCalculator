
/*=========================================
* Author: springDong
* Description: functional formula graph example.
==========================================*/

using System.Collections.Generic;
using SpringGUI;
using UnityEngine;

public class FunctionalGraphExample : MonoBehaviour
{
    public FunctionalGraph FunctionalGraph = null;

    private void Start()
    {
        // method one
        FunctionalGraph.Inject(Mathf.Sin , Color.red);

        // method two
        FunctionalGraph.Inject(new FunctionFormula(Mathf.Cos , Color.green , 2.0f));

        // method three
        FunctionalGraph.Inject( new List<FunctionFormula>
        {
            new FunctionFormula(Mathf.Log10,Color.yellow,2.0f),
            new FunctionFormula(Mathf.Abs,Color.cyan,2.0f)
        } );
    }
}
