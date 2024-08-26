using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[DefaultExecutionOrder(0)]
public class AutoSizeTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float lineHeightMultiplier = 1f; // 可调整的行高乘数

    private void UpdateHeightBasedOnLineCount()
    {
        // 获取当前文本框的文字内容
        string text = textMeshPro.text;

        // 获取当前 TMP 文本框的布局信息
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // 获取当前文本的行数
        int lineCount = textInfo.lineCount;

        // 获取每一行的高度
        float totalHeight = 0f;
        for (int i = 0; i < lineCount; i++)
        {
            float lineHeight = textInfo.lineInfo[i].lineHeight * lineHeightMultiplier;
            totalHeight += lineHeight;
        }

        // 设置 TMP 文本框的高度为所有行的总高度
        textMeshPro.rectTransform.sizeDelta = new Vector2(textMeshPro.rectTransform.sizeDelta.x, totalHeight);
    }

    private void Start()
    {
        // 在 Update 中调用更新高度的方法
        UpdateHeightBasedOnLineCount();
    }
}
