using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextComponentHandler : MonoBehaviour
{
	private enum TextFieldType
	{
		NONE,
		STANDART,
		TMP_UGUI,
		TMP
	}

	private TextFieldType type = TextFieldType.NONE;
	private Text standart;
	private TextMeshProUGUI tmpUGUI;
	private TextMeshPro tmp;


	void Start()
	{
		try
        {
			standart = GetComponent<Text>();
			tmpUGUI = GetComponent<TextMeshProUGUI>();
			tmp = GetComponent<TextMeshPro>();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

		if (standart != null)
			type = TextFieldType.STANDART;
		else if (tmpUGUI != null)
		{
			type = TextFieldType.TMP_UGUI;
		}
		else if (tmp != null)
		{
			type = TextFieldType.TMP;
		}
		else
		{
			Debug.Log("Cannot find text component");
		}
	}

	public void SetText(string text)
	{
		switch (type)
		{
			case TextFieldType.STANDART:
			{
				standart.text = text;
				break;
			}
			case TextFieldType.TMP:
			{
				tmp.text = text;
				break;
			}
			case TextFieldType.TMP_UGUI:
			{
				tmpUGUI.SetText(text);
				break;
			}
		}
	}
}
