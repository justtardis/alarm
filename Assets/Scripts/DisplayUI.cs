using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    [SerializeField] private Text _textInfo;
    [SerializeField] private string[] _textSpeeches = { "Жулик снаружи", "Жулик внутри" };
    private string _textOut;

    private void Start()
    {
        _textOut = _textSpeeches[0];
    }

    public void SetNewText(bool isPenetrate)
    {
        _textOut = isPenetrate ? _textSpeeches[1] : _textSpeeches[0];
    }

    private void Update()
    {
        _textInfo.text = _textOut;
    }
}
