using Imoet.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Imoet.Utility
{
    public static class ObjectUtility
    {
        private static SafeDictionary<Type, List<MethodInfo>> m_methodList;
        private static SafeDictionary<Type, List<FieldInfo>> m_fieldsList;

        static ObjectUtility()
        {
            m_methodList = new SafeDictionary<Type, List<MethodInfo>>();
            m_fieldsList = new SafeDictionary<Type, List<FieldInfo>>();
        }

        public static void SendMessage(this object obj, string methodName, object[] parameters = null, bool required = false)
        {
            if (obj == null)
                throw new NullReferenceException("Target message cannot be Null");
            var t = obj.GetType();
            var methodList = m_methodList[t];
            if (methodList == null)
            {
                methodList = new List<MethodInfo>();
            }
            else
            {
                foreach (var method in methodList)
                {
                    if (method.Name == methodName)
                    {
                        try
                        {
                            method.Invoke(obj, parameters);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Function Execution Return Error: " + e);
                        }
                        return;
                    }
                }
            }

            var tMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in tMethods)
            {
                if (method.Name == methodName)
                {
                    try
                    {
                        methodList.Add(method);
                        method.Invoke(obj, parameters);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Function Execution Return Error: " + e);

                    }
                    return;
                }
            }

            if (required)
                throw new NullReferenceException("Cannot find Method (" + methodName + ") From Object (" + t.Name + ")");
        }

        public static void SendValue(this object obj, string fieldName, object value, bool required = false)
        {
            if (obj == null)
                throw new NullReferenceException("Target message cannot be Null");
            var t = obj.GetType();
            var tFields = m_fieldsList[t];
            if (tFields == null)
                tFields = new List<FieldInfo>();
            else
            {
                foreach (var field in tFields)
                {
                    if (field.Name == fieldName)
                    {
                        field.SetValue(obj, value);
                        return;
                    }
                }
            }
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.SetField | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                if (field.Name == fieldName)
                {
                    tFields.Add(field);
                    field.SetValue(obj, value);
                    return;
                }
            }
            if (required)
                throw new NullReferenceException("Cannot find Field (" + fieldName + ") From Object (" + t.Name + ")");
        }
    }
}
