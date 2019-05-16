using UnityEngine;
using System.Collections;

public class FormatTypeData {
    public FormatBaseType BaseType { set; get; }
	public FormatType Type { set; get; }
    /// <summary>
    /// 如果是多层数组类型，有多少层
    /// </summary>
    public int ExtCount { set; get; }
}
