
/*=========================================
* Author: springDong
* Description: SpringGUI.LineChartGraph example.
==========================================*/

using System.Collections;
using System.Collections.Generic;
using PCRCaculator.Guild;
using SpringGUI;
using UnityEngine;

public class LineChartGraphExample : MonoBehaviour
{
    public class TestData : IValue
    {
        int IValue.xValue => (int) xValue;
        public float xValue { get; set; }
        public float yValue { get; set; }

        public TestData( float x ,float y )
        {
            xValue = x;
            yValue = y;
        }
    }
    public List<TestData> lista = new List<TestData>();
    public List<TestData> listb = new List<TestData>();

    public LineChart LineChart = null;

    public void Awake()
    {
        var data1 = new List<TestData>
        {
                new TestData(0.0f,0.0f),
                new TestData(0.1f,0.9f),
                new TestData(0.2f,0.2f),
                new TestData(0.3f,0.8f),
                new TestData(0.4f,0.3f),
                new TestData(0.5f,0.7f),
                new TestData(0.6f,0.4f),
                new TestData(0.7f,0.6f),
                new TestData(0.8f,0.5f),
                new TestData(0.9f,0.2f),
                new TestData(1f,0.5f),
            };
        var data2 = new List<TestData>
        {
                new TestData(0.0f,0.7f),
                new TestData(0.1f,0.1f),
                new TestData(0.2f,0.5f),
                new TestData(0.3f,0.6f),
                new TestData(0.4f,0.7f),
                new TestData(0.5f,0.2f),
                new TestData(0.6f,0.72f),
                new TestData(0.7f,0.24f),
                new TestData(0.8f,0.52f),
                new TestData(0.9f,0.1f),
                new TestData(1f,0.0f),
            };
        for (int i = 0; i < 11; i++)
        {
            lista.Add(new TestData(i /540.0f , Mathf.Abs(Mathf.Sin(i*99)*15)));
            listb.Add(new TestData(i /10.0f, Mathf.Abs(Mathf.Cos(i*99)*15)));
        }

        LineChart.Inject(lista);
        LineChart.Inject(listb);
        LineChart.ShowUnit();
        StartCoroutine(UpdateThis());
    }
    public IEnumerator UpdateThis()
    {
        for(int i = 0; i < 550; i++)
        {
            lista.Add(new TestData(i / 540.0f, Mathf.Abs(Mathf.Sin(i * 99))));
            LineChart.Replace(0, lista);
            yield return 0;
        }
    }
    /*public void OnGUI()
    {
        if (GUILayout.Button("Reset"))
        {
            LineChart.Refresh();
        }
        GUILayout.Label("Static Data");
        if (GUILayout.Button("Mode1"))
        {
            LineChart.lineChartType = LineChart.LineChartType.LineChart1;
            LineChart.Refresh();
        }
        if (GUILayout.Button("Mode2"))
        {
            LineChart.lineChartType = LineChart.LineChartType.LineChart2;
            LineChart.Refresh();
        }
        if (GUILayout.Button("Mode3"))
        {
            LineChart.lineChartType = LineChart.LineChartType.LineChart3;
            LineChart.Refresh();
        }
        if (GUILayout.Button("Delete one line "))
        {
            LineChart.RemoveLine(1);
        }
        GUILayout.Label("Dynamic Data");
        GUILayout.Space(5);
        if (GUILayout.Button("Replace all data"))
        {
            LineChart.Replace(1 , new List<TestData>()
                {
                    new TestData(0.0f,0.11f),
                    new TestData(0.1f,0.1f),
                    new TestData(0.2f,0.27f),
                    new TestData(0.3f,0.16f),
                    new TestData(0.4f,0.1f),
                    new TestData(0.5f,0.14f),
                    new TestData(0.6f,0.52f),
                    new TestData(0.7f,0.2f),
                    new TestData(0.8f,0.11f),
                    new TestData(0.9f,0.8f),
                    new TestData(1f,0.18f),
                });
        }
        GUILayout.Space(5);
        if ( GUILayout.Button(" Dynamic stream data ") )
        {
            LineChart.InjectVertexStream(1 , new List<TestData>()
                {
                    new TestData(0.0f,0.9f),
                    new TestData(0.0f,0.2f),
                    new TestData(0.0f,0.4f),
                    new TestData(0.0f,0.5f),
                    new TestData(0.0f,0.6f),
                    new TestData(0.0f,0.1f),
                    new TestData(0.0f,0.23f),
                    new TestData(0.0f,0.15f),
                    new TestData(0.0f,0.61f),
                    new TestData(0.0f,0.1f),
                    new TestData(0.0f,0.5f),
                    new TestData(0.0f,0.1f),
                });
        }
    }*/
}