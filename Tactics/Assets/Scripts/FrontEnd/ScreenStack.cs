using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenStack {

	private static FSprite tBackground;
	private static FSprite tBackground2;
	public static List<UIScreen> tStack = new List<UIScreen>();
	public static FStage tBackgroundStage = new FStage("BACKGROUND");
	public static FStage tTextStage = new FStage("TEXT");

	public static void Init()
	{
		InitialiseFutile();
		AddBackground();
		AddTextStage();
	}

	public static void Add(UIScreen tScreen)
	{
		tScreen.Init();
		tStack.Add(tScreen);
	}

	public static void Update()
	{
		if(tStack.Count > 0)
		{
			tStack[0].Update();
			tStack[0].UpdateChildren();
		}
	}

	public static void GUIDisplay()
	{
		if(tStack.Count > 0)
		{
			tStack[0].GUIDisplay();
		}
	}

	public static void SetBackgroundCol(Color tCol)
	{
		if(Futile.instance != null && Futile.instance.enabled == true)
		{
			if(tBackground != null)
			{
				tBackground.color = tCol;
			}
		}
	}

	private static void AddBackground()
	{
		if(Futile.instance != null && Futile.instance.enabled == true)
		{
			if(tBackground == null)
			{
				tBackground = new FSprite("blank");
				tBackground.color = Color.grey;
				tBackground.width = Futile.screen.width;
				tBackground.height = Futile.screen.height;
				tBackgroundStage.AddChild(tBackground);
				Futile.AddStageAtIndex(tBackgroundStage, 0);
			}
		}
	}

	public static void AddTextStage()
	{
		Futile.AddStage(tTextStage);
	}

	public static void InitialiseFutile()
	{
		FutileParams fParams = new FutileParams(true, true, false, false);
		int iWidth = 960;
		int iHeight = 640;
		fParams.AddResolutionLevel(Mathf.Max(iWidth,iHeight), 1, 1, "");
		fParams.origin = new Vector2(0.5f, 0.5f);

		Futile.instance.Init(fParams);
		//Load all atlases
		FutileLoadAll();

	}

	public static void FutileLoadAll()
	{
		string[] sAtlases = Directories.GetFolderNames(Directories.GetAtlasFolder());
		string[] sFonts = Directories.GetFolderNames(Directories.GetFontsFolder());

		//Load texture atalses
		foreach(string sAtlas in sAtlases)
		{
			string[] sFiles = Directories.GetFileNames(sAtlas, "*.txt");
			foreach(string sFile in sFiles)
			{
				string sAtlasName = Directories.GetFileNameFromPath(sAtlas, false);
				string sPath = Directories.sAtlasFolder + sAtlasName + "/" + Directories.GetFileNameFromPath(sFile, false);
				Futile.atlasManager.LoadAtlas(sPath);
			}
		}

		//Load font atlases
		foreach(string sFont in sFonts)
		{
			string[] sFiles = Directories.GetFileNames(sFont, "*.txt");
			string sAtlasName = Directories.GetFileNameFromPath(sFont, false);
			foreach(string sFile in sFiles)
			{
				string sPath = Directories.sFontFolder + sAtlasName + "/" + Directories.GetFileNameFromPath(sFile, false);
				Futile.atlasManager.LoadAtlas(sPath);
			}

			string sFontPath = sFont + "/" + Directories.sFontSubFolder;
			string[] sFontFiles = Directories.GetFileNames(sFontPath, "*.txt");
			foreach(string sFile in sFontFiles)
			{
				string sFileName = Directories.GetFileNameFromPath(sFile, false);
				string sConfig = Directories.sFontFolder+sAtlasName+"/"+Directories.sFontSubFolder+sFileName;
				Futile.atlasManager.LoadFont(sFileName, sFileName, sConfig, 0, 0);
			}
		}
	}	
}
