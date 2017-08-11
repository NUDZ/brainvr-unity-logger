using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using System;
//important namespace
//for the write function

namespace BrainVR.UnityLogger
{
    public class Header
    {
        public string Participant;
        public string Timestamp;

        public Header(string participant, string timestamp)
        {
            Participant = participant;
            Timestamp = timestamp;
        }
    }

    public class Log
    {
        readonly StreamWriter _logFile;

        public string FilePath;

        public string DateString;

        const string RELATIVE_PATH = "/../logs/";

        //this class should have the basic functionality of logging
        //instantiating functional class along with the instantiating the write file connection
        //making the basic write line and write text
        //it cannot log on itself, as it doesnt't use monobehaviour

        public Log(string id, string logName)
        {
            DateTime dateTime = System.DateTime.Now;
            DateString = dateTime.ToString("HH-mm-ss-dd-MM-yyy");
            FilePath = Application.dataPath + RELATIVE_PATH + id + "/";

            //creates a folder for the patient, if it doesnt exist
            Directory.CreateDirectory(FilePath);

            //vytvoři logfile - poslední parametr určuje, zda v případě existejnce souboru bude pokračovat v zápisu (true), či jej smaže (false);
            _logFile = new StreamWriter(FilePath + id + "_" + logName + "_" + DateString + ".txt", true);
            
            //AUTOFLUSH - makes sure we never loose the data
            _logFile.AutoFlush = true;
            
            //because we can restart etc. many times during a day, the file continues but always writes a new header
            WriteHeader(id);
        }

        /// <summary>
        /// polymorph for init with timepstmp provided by master log 
        /// </summary>
        /// <param name="id">ParticipantId of the participant</param>
        /// <param name="logName">name of the log file, e.g. Player/experiment etc.</param>
        /// <param name="Timestamp">timestamp provided by master log</param>
        public Log(string id, string logName, string timeStamp)
        {
            FilePath = Application.dataPath + RELATIVE_PATH + id + "/";
            //creates a folder for the patient, if it doesnt exist

            Directory.CreateDirectory(FilePath);
            //vytvoři logfile - poslední parametr určuje, zda v případě existejnce souboru bude pokračovat v zápisu (true), či jej smaže (false);

            _logFile = new StreamWriter(FilePath + id + "_" + logName + "_" + timeStamp + ".txt", true);

            //AUTOFLUSH - makes sure we never loose the data
            _logFile.AutoFlush = true;

            //because we can restart etc. many times during a day, the file continues but always writes a new header
            WriteHeader(id);
        }

        //simple header file for each new star of the experiment
        private void WriteHeader(string id)
        {
            Header header = new Header(id, System.DateTime.Now.ToString("HH-mm-ss-dd-MM-yyy"));
            _logFile.WriteLine("***SESSION HEADER***");
            _logFile.WriteLine(JsonConvert.SerializeObject(header, Formatting.Indented));
            _logFile.WriteLine("---SESSION HEADER---");
        }
        //takes a string and writes it down
        public void WriteLine(string str)
        {
            //this is the header line for analysiss software
            _logFile.WriteLine(str);
        }

        public void Close() 
        {
            _logFile.Close();
        }

        //takes a list of string as an argument and turns them into a one line that is written into the file
        public void WriteList(List<string> data)
        {
            //basically LINQ foreach
            string line = data.Aggregate("", (current, text) => current + (text + ";"));
            _logFile.WriteLine(line);
        }

        #region Helpers
        public static string NewLine { get { return Environment.NewLine; } }
        #endregion
    }
}