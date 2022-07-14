using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class SettingPresenter : MonoBehaviour
{
    [SerializeField]
    SelectMenuData selectMenuData;
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    TextMeshProUGUI noteSpeedText;

    void Start()
    {
        selectMenuData.onNoteSpeedChanged.Subscribe(noteSpeed => noteSpeedText.text = noteSpeed.ToString());
        selectMenuData.NoteSpeed = 10f;
    }

    public void OnSettingButtonClicked()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void OnLeftButtonClicked()
    {
        selectMenuData.NoteSpeed -= 0.5f;
    }

    public void OnRightButtonClicked()
    {
        selectMenuData.NoteSpeed += 0.5f;
    }
}
