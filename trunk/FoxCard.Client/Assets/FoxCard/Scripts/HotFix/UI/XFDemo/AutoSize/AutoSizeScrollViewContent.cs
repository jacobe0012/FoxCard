using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
[DefaultExecutionOrder(1600)]
public class AutoSizeScrollViewContent : MonoBehaviour
{
    
    public ScrollRect autoContent;
    public List<TextMeshProUGUI> autoTmpList;
    public List<GameObject> PosList;
    private int PosIndex = 0;
    private Vector2 autoHegith = new Vector2(0, 0);
    private Vector2 PosPosition=new Vector2(242, 0);
    private float Margin = 0f;
    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < autoTmpList.Count; i++)
        {
            //Margin = 25f;
            var textHeight=autoTmpList[i].GetComponent<RectTransform>().Height();
            if (textHeight >= 50)
            {
                autoHegith += new Vector2(0, textHeight - 50);
            }
            else
            {
                autoHegith += new Vector2(0, 60);
            }
          
            PosIndex++;
            if (PosIndex < 5)
            {
                PosList[PosIndex].GetComponent<RectTransform>().anchoredPosition3D = PosPosition - autoHegith - new Vector2(0, Margin);
            }
            
        }


        autoContent.content.sizeDelta = autoHegith; 
        LayoutRebuilder.ForceRebuildLayoutImmediate(autoContent.content.GetComponent<RectTransform>());

    }

    
}
