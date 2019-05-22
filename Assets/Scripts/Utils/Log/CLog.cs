using UnityEngine;
using System.Collections;
using System;

public class CLog {

    static CLog()
    {
        LogLevel = LogLevel.INFO;
    }

    public static void Log(string s)
    {
        if (LogLevel >= LogLevel.INFO);
        Debug.Log(s);
    }

	public static void LogFormat(string s, params object[] format)
    {
        if (LogLevel >= LogLevel.INFO);
        Debug.LogFormat(s, format);
    }

    public static void LogError(string s)
    {
        if (LogLevel >= LogLevel.RELEASE);
        Debug.LogError(s);
    }

    public static void LogErrorFormat(string s, params object[] format)
    {
        if (LogLevel >= LogLevel.RELEASE);
        Debug.LogErrorFormat(s, format);
    }

    public static void LogException(Exception e)
    {
        if (LogLevel >= LogLevel.DEBUG);
        Debug.LogException(e);
    }

    public static void LogWarning(string s)
    {
        if (LogLevel >= LogLevel.DEBUG);
        Debug.LogWarning(s);
    }

    public static void LogWarningFormat(string s, params object[] format)
    {
        if (LogLevel >= LogLevel.DEBUG);
        Debug.LogWarningFormat(s, format);
    }

    public static LogLevel LogLevel { set; get; }
}

public enum LogLevel
{
    RELEASE,
    DEBUG,
    INFO
}
