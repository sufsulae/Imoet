//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_THREADING || IMOET_INCLUDE_NETWORKING
namespace Imoet.Threading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    public sealed class Worker
    {
        private static List<WorkerClass> m_workerList;
        static Worker()
        {
            m_workerList = new List<WorkerClass>();
        }

        ~Worker()
        {
            DisposeAllWorker();
        }

        public static int StartWorker(ImoetDelegateReturn<bool> onWork, ImoetAction<Exception> onFinish = null, ImoetAction<int> onUpdate = null)
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

        public static void DisposeAllWorker() {
            foreach (var item in m_workerList) {
                item.worker.CancelAsync();
                item.worker.Dispose();
            }
            m_workerList.Clear();
        }
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
        public ImoetDelegateReturn<bool> onWork;
        public ImoetAction<int> onProgress;
        public ImoetAction<Exception> onFinish;

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (onProgress != null)
                onProgress(e.ProgressPercentage);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (onFinish != null && !e.Cancelled)
                onFinish(e.Error);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (onWork != null)
                e.Cancel = !onWork();
        }
    }
}
#endif