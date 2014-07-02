using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class TinyXmlReader
{
    private string xmlString = "";
    private int idx = 0;
 
    public TinyXmlReader(string newXmlString)
    {
        xmlString = newXmlString;
    }
 
    public string tagName = "";
    public bool isOpeningTag = false;
    public string content = "";
	private List<XMLAttribute> attributes = new List<XMLAttribute>();
 
 
	// properly looks for the next index of _c, without stopping at line endings, allowing tags to be break lines	
	int IndexOf(char _c, int _i)
	{
		int i = _i;
		while (i < xmlString.Length)
		{
			if (xmlString[i] == _c)
				return i;
 
			++i;
		}
 
		return -1;
	}
 
    public bool Read()
    {
		if (idx > -1)
        	idx = xmlString.IndexOf("<", idx);
 
        if (idx == -1)
        {
            return false;
        }
        ++idx;
 
		// skip attributes, don't include them in the name!
		int endOfTag = IndexOf('>', idx);
		int endOfName = IndexOf(' ', idx);
		int attributeStart = endOfName+1;
        if ((endOfName == -1) || (endOfTag < endOfName))
	    {
			endOfName = endOfTag;
		}
 
		if (endOfTag == -1)
        {
            return false;
        }
 
        tagName = xmlString.Substring(idx, endOfName - idx);
 
		attributes.Clear();
		while(attributeStart < endOfTag && attributeStart >= 0)
		{
			int valueStart = IndexOf('"', attributeStart) + 1;
			int valueEnd = IndexOf('"', valueStart);
			
			if(valueStart - attributeStart >= 2 && valueEnd - valueStart > 0)
			{
				string sName = xmlString.Substring(attributeStart, valueStart - attributeStart - 2);
				string sValue = xmlString.Substring(valueStart, valueEnd - valueStart);
				
				attributes.Add(new XMLAttribute(sName, sValue));
				
				attributeStart = IndexOf(' ', valueEnd)+1;
			}
			else
				break;
		}
		
        idx = endOfTag;
 
        // check if a closing tag
        if (tagName.StartsWith("/"))
        {
            isOpeningTag = false;
            tagName = tagName.Remove(0, 1); // remove the slash
        }
        else
        {
            isOpeningTag = true;
        }
 
        // if an opening tag, get the content
        if (isOpeningTag)
        {
            int startOfCloseTag = xmlString.IndexOf("<", idx);
            if (startOfCloseTag == -1)
            {
                return false;
            }
 
            content = xmlString.Substring(idx+1, startOfCloseTag-idx-1);
            content = content.Trim();
        }
 
        return true;
    }
 
    // returns false when the endingTag is encountered
    public bool Read(string endingTag)
    {
        bool retVal = Read();
        if (tagName == endingTag && !isOpeningTag)
        {
            retVal = false;
        }
        return retVal;
    }
	
	public string GetAttributeValue(string sName)
	{
		foreach(XMLAttribute tAttribute in attributes)
		{
			if(tAttribute.sAttName == sName)
				return tAttribute.sValue;
		}
		return "";
	}
}

class XMLAttribute
{
	public XMLAttribute(string name, string attValue)
	{
		sAttName = name;
		sValue = attValue;
	}
	public string sAttName;
	public string sValue;
}