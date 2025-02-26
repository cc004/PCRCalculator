﻿
/*=========================================
* Author: Administrator
* DateTime:2017/7/17 14:27:18
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using PCRCaculator.Guild;
using UnityEngine;

namespace SpringGUI
{
    public class LineChartDataMediator
    {
        /// <summary>
        /// get format data by reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vertexs"></param>
        /// <returns></returns>
        public IList<Vector2> Inject<T>( IList<T> vertexs ) where T : IValue
        {
            IList<Vector2> result = new List<Vector2>();
            Type type = typeof(T);
            PropertyInfo[] PropertyInfo = type.GetProperties();
            foreach ( T vertex in vertexs )
            {
                float x = vertex.xValue;
                float y = vertex.yValue;
                if (x > 1 || x < 0 || y > 1 || y < 0) Debugger.Break();
                result.Add(new Vector2(x , y));
            }
            return result;
        }

        public IList<Vector2>[] Inject<T>( IList<T>[] vertexs ) where T : IValue
        {
            IList<Vector2>[] result = new IList<Vector2>[vertexs.Length];
            for (int i = 0; i < vertexs.Length; i++)
                result.SetValue(Inject(vertexs[i]) , i);
            return result;
        }

        // write your own parser method
        public IList<Vector2> Inject(IList<Vector2> vertexs)
        {

            return vertexs;
        }
    }
}