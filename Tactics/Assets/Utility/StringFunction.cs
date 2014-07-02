using System;
using System.Text;
using System.Globalization;
using System.Linq;

public class StringFunction
{
	public static string RemoveDiacritics(string text)
	{
	  string formD = text.Normalize(NormalizationForm.FormD);
	  StringBuilder sb = new StringBuilder();
	
	  foreach (char ch in formD)
	  {
	    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
	    if (uc != UnicodeCategory.NonSpacingMark)
	    {
	      sb.Append(ch);
	    }
	  }
	
		sb.Replace((char)216, 'O');//'Ø'
		sb.Replace((char)248, 'o');//'ø'
		sb.Replace(((char)198).ToString(), "AE");//Æ
		sb.Replace(((char)230).ToString(), "ae");//æ
		sb.Replace((char)208, 'D');//Ð
		sb.Replace((char)240, 'd');//ð
		
	  return sb.ToString().Normalize(NormalizationForm.FormC);
	}
}


