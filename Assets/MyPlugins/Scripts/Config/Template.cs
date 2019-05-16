using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Template
{

    //基础类型
    private int _tInt;
    public int tInt { get { return _tInt; } }
    private float _tFloat;
    public float tFloat { get { return _tFloat; } }
    private long _tLong;
    public long tLong { get { return _tLong; } }
    private string _tString;
    public string tString { get { return _tString; } }

    //组合类型
    private ReadOnlyCollection<int> _tArrInt;
    public ReadOnlyCollection<int> tArrInt { get { return _tArrInt; } }
    private ReadOnlyCollection<float> _tArrFloat;
    public ReadOnlyCollection<float> tArrFloat { get { return _tArrFloat; } }
    private ReadOnlyCollection<long> _tArrLong;
    public ReadOnlyCollection<long> tArrLong { get { return _tArrLong; } }
    private ReadOnlyCollection<string> _tArrString;
    public ReadOnlyCollection<string> tArrString { get { return _tArrString; } }

    public void Parse()
    {
        _tInt = 1;
        _tFloat = 2.2f;
        _tLong = 333;
        _tString = "ttString";

        List<int> itArrInt = new List<int>();
        itArrInt.Add(10);
        itArrInt.Add(20);
        _tArrInt = itArrInt.AsReadOnly();

        List<float> itArrFloat = new List<float>();
        itArrFloat.Add(22.2f);
        itArrFloat.Add(33.3f);
        _tArrFloat = itArrFloat.AsReadOnly();

        List<long> itArrLong = new List<long>();
        itArrLong.Add(444);
        itArrLong.Add(555);
        _tArrLong = itArrLong.AsReadOnly();

        List<string> itArrString = new List<string>();
        itArrString.Add("sss");
        itArrString.Add("ttt");
        _tArrString = itArrString.AsReadOnly();
    }
}