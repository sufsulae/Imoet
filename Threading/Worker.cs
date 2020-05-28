//Imoet Library
//Copyright © 2020 Yusuf Sulaeman
#if IMOET_INCLUDE_THREADING || IMOET_INCLUDE_NETWORKING
namespace Imoet.Threading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    public sealed class Worker
    {
        public delegate bool WorkerDelegate(out object Result);
        public delegate void WorkerFinishedDelegate(Exception e, object Result);
        public delegate void WorkerOnProgressDelegate(int progress);

        private static List<WorkerClass> m_workerList;

        static Worker()
        {
            m_workerList = new List<WorkerClass>();
        }

        ~Worker()
        {
            DisposeAllWorker();
        }

        public static int StartWorker(WorkerDelegate onWork, WorkerFinishedDelegate onFinish = null, WorkerOnProgressDelegate onUpdate = null)
        {
            for (int i = 0; i < m_workerList.Count; i++)
            {
                if (!m_workerList[i].worker.IsBusy)
                {
                    m_workerList[i].onWork = onWork;
                    m_workerList[i].onProgress = onUpdate;
                    m_workerList[i].onFinish = onFinish;
                    m_workerList[i].worker.RunWorkerAsync();
                    return i;
                }
            }
            WorkerClass worker = new WorkerClass();
            worker.onWork = onWork;
            worker.onProgress = onUpdate;
            worker.onFinish = onFinish;
            m_workerList.Add(worker);
            worker.worker.RunWorkerAsync();
            return m_workerList.Count - 1;
        }

        public static void StopWorker(int handler)
        {
            m_workerList[handler].worker.CancelAsync();
        }

        public static void StopAllWorker()
        {
            foreach (var item in m_workerList)
            {
                item.worker.CancelAsync();
            }
        }

        public static void DisposeAllWorker()
        {
            foreach (var item in m_workerList)
            {
                item.worker.CancelAsync();
                item.worker.Dispose();
            }
            m_workerList.Clear();
        }

        internal class WorkerClass
        {
            public BackgroundWorker worker;
            public WorkerClass()
            {
                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.ProgressChanged += Worker_ProgressChanged;
            }
            public WorkerDelegate onWork;
            public WorkerOnProgressDelegate onProgress;
            public WorkerFinishedDelegate onFinish;

            private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                onProgress?.Invoke(e.ProgressPercentage);
            }

            private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (onFinish != null && !e.Cancelled)
                    onFinish(e.Error, e.Result);
            }

            private void Worker_DoWork(object sender, DoWorkEventArgs e)
            {
                if (onWork != null)
                {
                    object result = null;
                    e.Cancel = !onWork(out result);
                    e.Result = result;
                }
            }
        }
    }
}
#endif