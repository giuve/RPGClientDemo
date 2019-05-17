using System.Collections.Generic;
namespace Game.Config
{
public class CityConfig : BaseConfig
{
private int _ID;
public int ID { get { return _ID; } }
private int _CityLevel;
public int CityLevel { get { return _CityLevel; } }
private string _Icon;
public string Icon { get { return _Icon; } }
private int _CityNum;
public int CityNum { get { return _CityNum; } }
private int _MaxNum;
public int MaxNum { get { return _MaxNum; } }
private List<List<string>> _BaoMingTime;
public List<List<string>> BaoMingTime { get { return _BaoMingTime; } }
private List<int> _AttackWeekDay;
public List<int> AttackWeekDay { get { return _AttackWeekDay; } }
private string _AttackTime;
public string AttackTime { get { return _AttackTime; } }
private List<int> _Award;
public List<int> Award { get { return _Award; } }
private List<List<int>> _DayAward;
public List<List<int>> DayAward { get { return _DayAward; } }
private int _DayAwardNum;
public int DayAwardNum { get { return _DayAwardNum; } }
private int _ZhanMengZiJin;
public int ZhanMengZiJin { get { return _ZhanMengZiJin; } }
}
}
