using DG.Tweening;
using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 打字機效果狀態。
/// </summary>
public enum TypewriterState
{
    /// <summary>
    /// 完成。
    /// </summary>
    Completed,

    /// <summary>
    /// 進行中。
    /// </summary>
    Outputting,

    /// <summary>
    /// 被中斷。
    /// </summary>
    Interrupted
}

/// <summary>
/// TextMeshPro的打字機效果元件。
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class Typewriter : MonoBehaviour
{
    /// <summary>
    /// 每秒輸出的字符數。
    /// </summary>
    public byte OutputSpeed
    {
        get { return _outputSpeed; }
        set
        {
            _outputSpeed = value;
            CompleteOutput();
        }
    }

    /// <summary>
    /// 淡化範圍內的字符數。
    /// </summary>
    public byte FadeRange
    {
        get { return _fadeRange; }
        set
        {
            _fadeRange = value;
            CompleteOutput();
        }
    }

    /// <summary>
    /// 當前打字機狀態。
    /// </summary>
    public TypewriterState State { get; private set; } = TypewriterState.Completed;

    public bool AutoPlay = true;
    public float Delay = 0;

    [Tooltip("每秒輸出的字符數。")]
    [Range(1, 255)]
    [SerializeField]
    private byte _outputSpeed = 20;

    [Tooltip("淡化範圍內的字符數。")]
    [Range(0, 50)]
    [SerializeField]
    private byte _fadeRange = 10;

    private TMP_Text m_textComponent;
    /// <summary>
    /// TextMeshPro元件。
    /// </summary>
    private TMP_Text _textComponent { get => m_textComponent ??= GetComponent<TMP_Text>(); set => m_textComponent = value; }

    /// <summary>
    /// 用於輸出的協程。
    /// </summary>
    private Coroutine _outputCoroutine;

    /// <summary>
    /// 輸出結束時的回調。
    /// </summary>
    public UnityEvent<TypewriterState> _outputEndCallback;

    public void StartOutput()
    {
        ShowText();
        CompleteOutput();

        if (_textComponent != null)
        {
            _textComponent.ForceMeshUpdate();

            if (!string.IsNullOrEmpty(_textComponent.text))
            {
                OutputText(_textComponent.text);
            }
        }
    }

    public void HideText()
    {
        _textComponent.enabled = false;
        _textComponent.ForceMeshUpdate();
    }
    public void ShowText()
    {
        _textComponent.enabled = true;
        _textComponent.ForceMeshUpdate();
    }

    [ContextMenu("重新開始")]
    void RePrinter() => OutputText(_textComponent.text);

    /// <summary>
    /// 開始輸出文字。
    /// </summary>
    /// <param name="text">要顯示的文字</param>
    /// <param name="onOutputEnd">結束回調</param>
    public void OutputText(string text)
    {
        if (State == TypewriterState.Outputting)
        {
            StopCoroutine(_outputCoroutine);
            State = TypewriterState.Interrupted;
            OnOutputEnd(false);
        }

        if (_textComponent.enabled == false) ShowText();
        _textComponent.text = text;
        _textComponent.ForceMeshUpdate();

        if (!isActiveAndEnabled)
        {
            State = TypewriterState.Completed;
            OnOutputEnd(true);
            return;
        }

        _outputCoroutine = FadeRange > 0
            ? StartCoroutine(OutputCharactersFading())
            : StartCoroutine(OutputCharactersNoFading());

    }

    [ContextMenu("完成")]
    /// <summary>
    /// 立即顯示所有文字。
    /// </summary>
    public void CompleteOutput()
    {
        if (State == TypewriterState.Outputting)
        {
            State = TypewriterState.Completed;
            StopCoroutine(_outputCoroutine);
            OnOutputEnd(true);
        }
    }

    private void OnValidate()
    {
        if (State == TypewriterState.Outputting)
        {
            OutputText(_textComponent.text);
        }
    }

    private void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (AutoPlay) StartOutput();
    }

    private void OnDisable()
    {
        if (State == TypewriterState.Outputting)
        {
            State = TypewriterState.Interrupted;
            StopCoroutine(_outputCoroutine);
            OnOutputEnd(true);
        }
    }

    /// <summary>
    /// 無淡入效果的輸出協程。
    /// </summary>
    private IEnumerator OutputCharactersNoFading(bool skipFirstCharacter = true)
    {
        _textComponent.ForceMeshUpdate();
        State = TypewriterState.Outputting;
        _textComponent.maxVisibleCharacters = skipFirstCharacter ? 1 : 0;

        var timer = 0f;
        var interval = 1.0f / OutputSpeed;
        var textInfo = _textComponent.textInfo;
        while (_textComponent.maxVisibleCharacters < textInfo.characterCount)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0;
                _textComponent.maxVisibleCharacters++;
            }
            yield return null;
        }

        State = TypewriterState.Completed;
        OnOutputEnd(false);
    }

 
    /// <summary>
    /// 帶淡入效果的輸出協程。
    /// </summary>
    private IEnumerator OutputCharactersFading()
    {
        _textComponent.ForceMeshUpdate();
        State = TypewriterState.Outputting;
        var textInfo = _textComponent.textInfo;
        _textComponent.maxVisibleCharacters = textInfo.characterCount;

        if (textInfo.characterCount == 0)
        {
            State = TypewriterState.Completed;
            OnOutputEnd(false);
            yield break;
        }

        SetCharacterAlphaAll();

        var timer = 0f - Delay;
        var interval = 1.0f / OutputSpeed;
        var headCharacterIndex = 0;
        while (State == TypewriterState.Outputting)
        {
            timer += Time.deltaTime;

            var isFadeCompleted = true;
            var tailIndex = headCharacterIndex - FadeRange;

            for (int i = tailIndex + FadeRange; i < textInfo.characterCount; i++)
            {
                SetCharacterAlpha(i, 0);
            }

            for (int i = headCharacterIndex; i > -1 && i >= tailIndex; i--)
            {
                if (!textInfo.characterInfo[i].isVisible)
                {
                    continue;
                }

                var step = headCharacterIndex - i;
                var alpha = (byte)Mathf.Clamp((timer / interval + step) / FadeRange * 255, 0, 255);

                isFadeCompleted &= alpha == 255;
                SetCharacterAlpha(i, alpha);
            }

            _textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (timer >= interval)
            {
                if (headCharacterIndex < textInfo.characterCount - 1)
                {
                    timer = 0;
                    headCharacterIndex++;
                } else if (isFadeCompleted)
                {
                    State = TypewriterState.Completed;
                    OnOutputEnd(false);
                    yield break;
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 設置字符的透明度。
    /// </summary>
    private void SetCharacterAlpha(int index, byte alpha)
    {
        var materialIndex = _textComponent.textInfo.characterInfo[index].materialReferenceIndex;
        var vertexColors = _textComponent.textInfo.meshInfo[materialIndex].colors32;
        var vertexIndex = _textComponent.textInfo.characterInfo[index].vertexIndex;

        vertexColors[vertexIndex + 0].a = alpha;
        vertexColors[vertexIndex + 1].a = alpha;
        vertexColors[vertexIndex + 2].a = alpha;
        vertexColors[vertexIndex + 3].a = alpha;
    }

    /// <summary>
    /// 字符全透明。
    /// </summary>
    private void SetCharacterAlphaAll()
    {
        int count = _textComponent.textInfo.characterCount;
        for (int i = 0; i < count; i++)
        {
            SetCharacterAlpha(i, 0);
        }
    }

    /// <summary>
    /// 處理輸出結束的邏輯。
    /// </summary>
    private void OnOutputEnd(bool isShowAllCharacters)
    {
        _outputCoroutine = null;

        if (isShowAllCharacters)
        {
            var textInfo = _textComponent.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                SetCharacterAlpha(i, 255);
            }

            _textComponent.maxVisibleCharacters = textInfo.characterCount;
            _textComponent.ForceMeshUpdate();
        }

        _outputEndCallback?.Invoke(State);
    }

}
