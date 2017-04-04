using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Tobii;


public class StartCalibration : MonoBehaviour {

    public string path;

    //C:\Program Files (x86)\Tobii\Tobii EyeX Config\Tobii.EyeX.Configuration.exe

    // Use this for initialization
    void Start () {
        path = System.IO.File.ReadAllText("tobiiPath");
        //UnityEngine.Debug.LogError("PATH_ " + path);
    }
	
    public void StartQuickCalibration() {

        try {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //myProcess.StartInfo.CreateNoWindow = true;
            //myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = path;
            myProcess.StartInfo.Arguments = "-quick-calibration";
            //myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;
            //print(ExitCode);
        }
        catch (Exception e) {
            UnityEngine.Debug.LogError(e);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
