//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_UTILITY
namespace Imoet.Utility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Timers;
    public class Log
    {
        private static Log m_instance;
        private Timer m_timer;
        private List<ImoetAction> m_actionPool;
        private bool m_isExecuting;

        public FileInfo LogFile { get; }
        public string LogFilePath { get; }

        public string LogText {
            get { return File.ReadAllText(LogFilePath); }
        }

        //Constructor
        public Log() : this("Log.log") { }
        public Log(string filePath) {
            LogFilePath = filePath;
            LogFile = new FileInfo(LogFilePath);
            if (!LogFile.Exists)
            {
                LogFile.Directory.Create();
                var stream = LogFile.Create();
                stream.Close();
            }
            m_actionPool = new List<ImoetAction>();
            m_timer = new Timer(1);
            m_timer.Elapsed += m_onUpdate;
            m_timer.Start();
        }

        private void m_onUpdate(object obj, ElapsedEventArgs arg) {
            if (m_isExecuting)
                return;
            if (m_actionPool.Count > 0)
            {
                m_actionPool[0].Invoke();
                m_actionPool.RemoveAt(0);
            }
            else {
                m_isExecuting = false;
                m_timer.Stop();
            }
        }

        //Public Function
        public void Write(object obj) {
            m_regWrite(obj);
        }

        public void Write(string text) {
            m_regWrite(text);
        }

        public void Write(string[] texts) {
            foreach (var item in texts) {
                m_regWrite(item);
            }
        }
        public void Write(object[] obj)
        {
            foreach (var item in obj)
            {
                m_regWrite(item);
            }
        }

        private void m_regWrite(object obj) {
            m_actionPool.Add(() =>
            {
                var m_obj = obj;
                using (var writer = LogFile.AppendText()){
                    writer.WriteLine(m_obj.ToString());
                }
            });
            if (!m_isExecuting) {
                m_timer.Start();
            }
        }

        //Public Static Function
        public static void WriteLog(object obj) {
            if (m_instance == null)
                m_instance = new Log();
            m_instance.Write(obj);
        }
        public static void WriteLog(string obj)
        {
            if (m_instance == null)
                m_instance = new Log();
            m_instance.Write(obj);
        }
        public static void WriteLog(string[] obj)
        {
            if (m_instance == null)
                m_instance = new Log();
            m_instance.Write(obj);
        }
        public static void WriteLog(object[] obj)
        {
            if (m_instance == null)
                m_instance = new Log();
            m_instance.Write(obj);
        }
    }
}
#endif