using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class DirFunction
{
	public static string baseDir = Application.dataPath;
	public static DateTime GetMostRecentFileDate(string sDirectory)
	{
		DirectoryInfo directory = new DirectoryInfo(baseDir + sDirectory);
		return directory.LastWriteTime;
	}
	
	public static string[] GetFiles(string path, string searchPattern)
	{
	    string[] m_arExt = searchPattern.Split(';');
	
	    List<string> strFiles = new List<string>();
	    foreach(string filter in m_arExt)
	    {
	        strFiles.AddRange(
	               System.IO.Directory.GetFiles(baseDir + path, filter));
	    }
	    return strFiles.ToArray();
	}

	public static string[] GetFolders(string path)
	{
		return Directory.GetDirectories(baseDir + path);
	}
	
	public static void SaveText(string path, string text, bool bAppend, bool bAddBaseDir)
	{
		if(bAddBaseDir)
			path = baseDir + path;
		if(!bAppend)
			File.WriteAllText(path, text);
		else
			File.AppendAllText(path, text);
	}
	
	public static string LoadText(string path, bool bAddBaseDir)
	{
		if(bAddBaseDir)
			path = baseDir + path;
		
		return File.ReadAllText(path);
	}
	
	public static void CreateFolder(string path, bool bAddBaseDir)
	{
		if(!Directory.Exists(baseDir + path))
			Directory.CreateDirectory(baseDir + path);
	}
}
