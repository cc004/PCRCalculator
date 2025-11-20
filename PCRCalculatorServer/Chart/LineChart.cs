using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace XCharts.Runtime
{
    public class Line
    {
        internal object lineType;
    }
    public class YAxis
    {
        internal float max;
        internal float min;
    }
    public class XAxis
    {

    }
    public class LineChart : MonoBehaviour
    {
        internal object theme;

        internal void AddData(params object[] _)
        {
            throw new NotImplementedException();
        }

        internal T AddSerie<T>()
        {
            throw new NotImplementedException();
        }

        internal void AddXAxisData(string v)
        {
            throw new NotImplementedException();
        }

        internal T GetChartComponent<T>()
        {
            throw new NotImplementedException();
        }

        internal void RemoveData()
        {
            throw new NotImplementedException();
        }
    }
    public enum LineType
    {
        StepEnd
    }
}
