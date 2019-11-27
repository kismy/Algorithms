using System;
using System.Collections;

public class In {

    string[] lines;
    private int linesCount;
    private int currentLinesIndex = -1;

    public In(string inputTxt)
    {
        lines =   inputTxt.Split(new char[] { '\n' },StringSplitOptions.RemoveEmptyEntries);        
        linesCount = lines.Length;       
    }

    public bool hasNextLine()
    {
        //UnityEngine.MonoBehaviour.print(currentLinesIndex <= (linesCount - 1));
        return currentLinesIndex <=( linesCount-2);       
    }



    public string readLine() {
        currentLinesIndex++;
        //UnityEngine.MonoBehaviour.print(currentLinesIndex);
        return lines[currentLinesIndex];
    }
}
