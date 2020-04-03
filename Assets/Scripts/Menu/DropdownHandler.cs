using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
	public List<TMP_FontAsset> fonts = new List<TMP_FontAsset>(); // assign font for each options

	public void OnButtonClick()
	{
		ChangeFonts();

	}

	void ChangeFonts()
	{
		Transform content = transform.Find("Dropdown List").Find("Viewport").Find("Content");
		for (int i = 1; i < content.transform.childCount; i++)
		{
			Transform option = content.transform.GetChild(i);
			TextMeshProUGUI text = option.GetComponentInChildren<TextMeshProUGUI>();
			text.font = fonts[i - 1];
			text.fontSize = text.fontSize - 2;
		}
	}
}
