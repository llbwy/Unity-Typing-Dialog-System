using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public TextAsset dialogsFile;
    public float playTextSpeed;
    public int index;
    private Transform textObject;
    private Text dialog;
    private List<string> textList = new List<string>();
    private bool notTyping, cancelTyping;

    private void Awake()
    {
        InitTextFile(dialogsFile);
    }

    //这是一个字符列表
    private void OnEnable()
    {
        //🍎来限定打字是否完成，不然会出乱码
        notTyping = true;
        //🍎来限定打字是否完成，不然会出乱码
        //一开启就显示第一行
        // dialog.text = textList[index];
        // index++;
        StartCoroutine(PlayText());
    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.X) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        // //🍎
        // if (Input.GetKeyDown(KeyCode.X) && notTyping)
        // {
        //     // dialog.text = textList[index];
        //     // index++;
        //     StartCoroutine(PlayText());
        // }

        #region //直接显示完字

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (notTyping && !cancelTyping)
            {
                StartCoroutine(PlayText());
            }
            else if (!notTyping)
            {
                cancelTyping = !cancelTyping;
            }
        }

        #endregion
    }

    //初始化
    void InitTextFile(TextAsset dialogsFile)
    {
        textList.Clear();
        index = 0;
        //先清零,包括列表文本和下标
        textObject = transform.GetChild(0);
        dialog = textObject.GetComponent<Text>();
        //-----------------------------------------------------------------------
        var lineData = dialogsFile.text.Split('\n');
        //是以回车键切割,并将所有的都存进lineData
        foreach (var line in lineData)
        {
            textList.Add(line);
            //将每行存进列表
        }
    }

    IEnumerator PlayText()
    {
        //🍎来限定打字是否完成，不然会出乱码
        notTyping = false;
        //🍎来限定打字是否完成，不然会出乱码
        dialog.text = "";
        //是因为累积起来的dialog都存到一起了 因此务必清零，这一步也把原本文字框的文字给清零了

        //🥝字符界定 因为之前的字符串里可能含有转义符号 因此加了.Trim()就可以保留最基本的信息
        switch (textList[index].Trim())
        {
            case "A":
                print("A");
                index++;
                //直接略过去;
                break;

            case "B":
                print("B");
                index++;
                //直接略过去;
                break;
        }
        //🥝


        //核心♥

        #region While

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length)
        {
            dialog.text += textList[index][letter];
            yield return new WaitForSeconds(playTextSpeed);
            letter++;
        }
        dialog.text = textList[index];
        cancelTyping = false;
        #endregion

        // for (int i = 0; i < textList[index].Length; i++)
        // {
        //     dialog.text += textList[index][i];
        //     yield return new WaitForSeconds(playTextSpeed);
        // }

        //核心♥
        index++;
        //🍎来限定打字是否完成，不然会出乱码
        notTyping = true;
        //🍎来限定打字是否完成，不然会出乱码
    }
}