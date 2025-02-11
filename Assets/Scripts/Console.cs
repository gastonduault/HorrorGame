using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public Text displayer;
    public List<string> lines;
    // Start is called before the first frame update
    void Start()
    {
        lines = new List<string>();
        AddLine("Console started !");
    }

    public void Flush()
    {
        lines.Clear();
    }

    public void AddLine(string line)
    {
        lines.Add(line);
    }

    // Update is called once per frame
    void Update()
    {
        string content = "";
        for (int i=lines.Count-1; i>=0; i--)
        {
            content += lines[i] + "\n";
        }
        displayer.text = content;
    }
}