//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_UTILITY
namespace Imoet.Utility
{
    using System;
    using System.Collections.Generic;
    public class EventExecutor
    {
        private List<ImoetAction> m_list = new List<ImoetAction>();
        public static EventExecutor operator +(EventExecutor e, ImoetAction f)
        {
            e.m_list.Add(f);
            return e;
        }
        public static EventExecutor operator -(EventExecutor e, ImoetAction f)
        {
            e.m_list.Remove(f);
            return e;
        }
        public void AddEvent(ImoetAction ImoetDelegate)
        {
            m_list.Add(ImoetDelegate);
        }
        public void RemoveEvent(ImoetAction ImoetDelegate)
        {
            m_list.Remove(ImoetDelegate);
        }
        public void ClearEvent()
        {
            m_list.Clear();
        }
        public void Invoke()
        {
            foreach (ImoetAction f in m_list)
            {
                f.Invoke();
            }
        }
        public int eventCount {
            get { return m_list.Count; }
        }
    }
    public class EventExecutor<T>
    {
        private List<Action<T>> m_list = new List<Action<T>>();
        public static EventExecutor<T> operator + (EventExecutor<T> e, Action<T> f) {
            e.m_list.Add(f);
            return e;
        }
        public static EventExecutor<T> operator -(EventExecutor<T> e, Action<T> f)
        {
            e.m_list.Remove(f);
            return e;
        }
        public void AddEvent(Action<T> ImoetDelegate)
        {
            m_list.Add(ImoetDelegate);
        }
        public void RemoveEvent(Action<T> ImoetDelegate)
        {
            m_list.Remove(ImoetDelegate);
        }
        public void ClearEvent()
        {
            m_list.Clear();
        }
        public void Invoke(T arg) {
            foreach (Action<T> f in m_list) {
                f.Invoke(arg);
            }
        }
        public int eventCount
        {
            get { return m_list.Count; }
        }
    }
    public class EventExecutor<T1,T2>
    {
        private List<ImoetAction<T1,T2>> m_list = new List<ImoetAction<T1, T2>>();
        public static EventExecutor<T1,T2> operator +(EventExecutor<T1, T2> e, ImoetAction<T1, T2> f)
        {
            e.m_list.Add(f);
            return e;
        }
        public static EventExecutor<T1, T2> operator -(EventExecutor<T1, T2> e, ImoetAction<T1, T2> f)
        {
            e.m_list.Remove(f);
            return e;
        }
        public void AddEvent(ImoetAction<T1,T2> ImoetDelegate)
        {
            m_list.Add(ImoetDelegate);
        }
        public void RemoveEvent(ImoetAction<T1,T2> ImoetDelegate)
        {
            m_list.Remove(ImoetDelegate);
        }
        public void ClearEvent()
        {
            m_list.Clear();
        }
        public void Invoke(T1 arg1, T2 arg2)
        {
            foreach (ImoetAction<T1, T2> f in m_list)
            {
                f.Invoke(arg1, arg2);
            }
        }
        public int eventCount
        {
            get { return m_list.Count; }
        }
    }
    public class EventExecutor<T1,T2,T3>
    {
        private List<ImoetAction<T1, T2, T3>> m_list = new List<ImoetAction<T1, T2, T3>>();
        public static EventExecutor<T1, T2, T3> operator +(EventExecutor<T1, T2, T3> e, ImoetAction<T1, T2, T3> f)
        {
            e.m_list.Add(f);
            return e;
        }
        public static EventExecutor<T1, T2, T3> operator -(EventExecutor<T1, T2, T3> e, ImoetAction<T1, T2, T3> f)
        {
            e.m_list.Remove(f);
            return e;
        }
        public void AddEvent(ImoetAction<T1,T2,T3> ImoetDelegate)
        {
            m_list.Add(ImoetDelegate);
        }
        public void RemoveEvent(ImoetAction<T1,T2,T3> ImoetDelegate)
        {
            m_list.Remove(ImoetDelegate);
        }
        public void ClearEvent()
        {
            m_list.Clear();
        }
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            foreach (ImoetAction<T1, T2, T3> f in m_list)
            {
                f.Invoke(arg1,arg2,arg3);
            }
        }
        public int eventCount
        {
            get { return m_list.Count; }
        }
    }
    public class EventExecutor<T1,T2,T3,T4>
    {
        private List<ImoetAction<T1, T2, T3, T4>> m_list = new List<ImoetAction<T1, T2, T3, T4>>();
        public static EventExecutor<T1, T2, T3, T4> operator +(EventExecutor<T1, T2, T3, T4> e, ImoetAction<T1, T2, T3, T4> f)
        {
            e.m_list.Add(f);
            return e;
        }
        public static EventExecutor<T1, T2, T3, T4> operator -(EventExecutor<T1, T2, T3, T4> e, ImoetAction<T1, T2, T3, T4> f)
        {
            e.m_list.Remove(f);
            return e;
        }
        public void AddEvent(ImoetAction<T1,T2,T3,T4> ImoetDelegate)
        {
            m_list.Add(ImoetDelegate);
        }
        public void RemoveEvent(ImoetAction<T1,T2,T3,T4> ImoetDelegate)
        {
            m_list.Remove(ImoetDelegate);
        }
        public void ClearEvent()
        {
            m_list.Clear();
        }
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            foreach (ImoetAction<T1, T2, T3, T4> f in m_list)
            {
                f(arg1, arg2, arg3, arg4);
            }
        }
        public int eventCount
        {
            get { return m_list.Count; }
        }
    }
}
#endif