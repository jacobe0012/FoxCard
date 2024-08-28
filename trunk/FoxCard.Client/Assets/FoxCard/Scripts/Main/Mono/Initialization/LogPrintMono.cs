using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.Burst;
using Unity.Collections;
//using Unity.Entities;
using Unity.Mathematics;
//using Unity.Transforms;
using UnityEngine;
using static UnityEngine.Application;
/// <summary>
/// 
/// </summary>

public class LogPrintMono : MonoBehaviour
{
    // ʹ��StringBuilder���Ż��ַ������ظ�����
    StringBuilder m_logStr = new StringBuilder();
    // ��־�ļ��洢λ��
    string m_logFileSavePath;
    string m_logPrintTime;

    void Awake()
    {
        // ��ǰʱ��
        var t = System.DateTime.Now.ToString("yyyyMMddhhmmss");
        m_logFileSavePath = string.Format("{0}/output_{1}.log", Application.persistentDataPath, t);
        Debug.Log(m_logFileSavePath);
        Application.logMessageReceived += OnLogCallBack;
        Debug.Log("��־�洢����");
    }

    /// <summary>
    /// ��ӡ��־�ص�
    /// </summary>
    /// <param name="condition">��־�ı�</param>
    /// <param name="stackTrace">���ö�ջ</param>
    /// <param name="type">��־����</param>
    private void OnLogCallBack(string condition, string stackTrace, LogType type)
    {
        m_logPrintTime = System.DateTime.Now.ToString("T");
        m_logStr.Append(condition);
        m_logStr.Append("\n");
        m_logStr.Append(stackTrace);
        m_logStr.Append("\n");
        m_logStr.Append(m_logPrintTime);

        if (m_logStr.Length <= 0) return;
        if (!File.Exists(m_logFileSavePath))
        {
            var fs = File.Create(m_logFileSavePath);
            fs.Close();
        }
        using (var sw = File.AppendText(m_logFileSavePath))
        {
            sw.WriteLine(m_logStr.ToString());
        }
        m_logStr.Remove(0, m_logStr.Length);
    }
}
