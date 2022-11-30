using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

#if UNITY_EDITOR
using System.IO;
#endif

[CreateAssetMenu(fileName = "ColorSelectorData", menuName = "Scriptable Objects/ColorSelectorData")]
public class ColorSelectorData : ScriptableObject
{
    public ColorSelectors colorSelector;

    //public UnityEvent Response;

    public IColorSelector GetColorSelector()
    {

        return new ColorSelector();
    }

    


    #if UNITY_EDITOR
    public void UpdateDatabase()
    {
        List<string> classNames = new List<string>();
        classNames.Clear();
        List<string> files = new List<string>();
        string[] filesArr = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        files.AddRange(filesArr);
        string[] lines;
        foreach (string path in files)
        {
            lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                //int j = i + 2;
                if (lines[i].ToLower().Contains("public class"))
                {
                    string lClassname = lines[i].Replace("public class", "");
                    //Debug.Log(lClassname);
                    string[] classAndInterfaceName = lClassname.Split(':');

                    if (classAndInterfaceName.Length > 1)//is derived from something
                    {
                        if (classAndInterfaceName[1].Contains("IColorSelector"))
                        {
                            classNames.Add(classAndInterfaceName[0]);
                        }
                    }
                    break;
                }
            }
        }


        //Open file and write new variables
        string fileName = "ColorSelectors";
        filesArr = Directory.GetFiles(Application.dataPath, fileName + ".cs", SearchOption.AllDirectories);

        StreamWriter file = new(filesArr[0]);
        
        file.WriteLine("public enum " + fileName);
        file.WriteLine("{");
        for (int i = 0; i < classNames.Count; i++)
        {
            if (i != 0)
            {
                file.WriteLine(",");
            }
            file.Write("\t"+classNames[i].Trim());
            
        }
        file.WriteLine();
        file.WriteLine("}");
        file.Flush();
        file.Close();

    }
    #endif


}