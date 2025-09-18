/********************************
---------------------------------
著作者：RoanAlen
用途：打字效果
---------------------------------
*********************************/
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Collections;

public class TypingEffect : MonoBehaviour, ITypingEffect
{
    private static List<TypingEffect> activeInstances = new List<TypingEffect>();

    [SerializeField,TextArea]
    private string Type_String;

    [SerializeField]
    public TextMeshProUGUI targetText;
    public float typingSpeed = 0.05f;
    public float jumpSpeedMultiplier = 0.8f;
    public float jumpHeight = 15f;

    public event Action OnTypingComplete;

    //[SerializeField]
    //private AudioClip typingSound;
    //private AudioSource audioSource;

    // 新增控制是否啟用波浪效果的變數
    public bool enableWaveEffect = true;
    private Tween tween;

    private List<Vector3[]> originalVertices = new List<Vector3[]>();

    private Coroutine coroutine;

    private void Awake()
    {
        //if (audioSource == null)
        //{
        //    audioSource = gameObject.AddComponent<AudioSource>();
        //}

        ////if (typingSound != null)
        ////{
        ////    audioSource.clip = typingSound;
        ////}

        //// 配置其他AudioSource屬性（可選）
        //audioSource.playOnAwake = false;
        //audioSource.loop = false;
    }

    public void EndText()
    {
        StopAllCoroutines();
        targetText.text = Type_String;
    }

    private void OnEnable()
    {
        activeInstances.Add(this);
    }

    private void OnDisable()
    {
        activeInstances.Remove(this);
    }

    public float TypingSpeed
    {
        get { return typingSpeed; }
        set { typingSpeed = value; }
    }


    public void StartTypeEffect_Event(int Delay)
    {
        StartTypingWithEvent(targetText.text, Delay);
    }
    public void StartTypingWithEvent(string text, float delay = 0f)
    {
        Debug.Log("Starting typing with delay: " + delay);

        StartTypingEffect(text, delay, () => TypingCompleted());
    }
    public void StartTypeEffect_Only(int Delay)
    {
        StartTypingEffectOnly(targetText.text, Delay);
    }
    public void StartTypingEffectOnly(string text, float delay = 0f)
    {
        StartTypingEffect(text, delay, null);
    }

    public void StartTypingEffect_SelfString(float delay = 0f)
    {
        StartTypingEffect(Type_String, delay,null);
    }

    private void StartTypingEffect(string text, float delay, Action onComplete)
    {
        Sequence typingSequence = DOTween.Sequence();

        targetText.text = "";
        text = text.Replace(@"\n", "\n");


        float totalTypingDuration = typingSpeed * text.Length;

        int charIndex = 0;

        //if (audioSource != null)
        //{
        //    audioSource.Stop();
        //    audioSource.time = 0f;
        //}

        tween= typingSequence.AppendInterval(delay)
                      .AppendCallback(() =>
                      {
                          //if (audioSource != null && typingSound != null)
                          //{
                          //    audioSource.time = UnityEngine.Random.Range(0, typingSound.length - 1);
                          //    audioSource.loop = true;
                          //    audioSource.Play();
                          //}
                      })
                      .Append(DOTween.To(() => 0, x =>
                      {
                          charIndex = Mathf.FloorToInt(x);
                          targetText.text = text.Substring(0, charIndex);
                          targetText.ForceMeshUpdate();

                          if (charIndex > 0 && charIndex <= text.Length)
                          {
                              int lastIndex = charIndex - 1;
                              TMP_TextInfo textInfo = targetText.textInfo;

                              if (textInfo.characterCount > lastIndex)
                              {
                                  TMP_CharacterInfo charInfo = textInfo.characterInfo[lastIndex];

                                  if (!charInfo.isVisible) return;

                                  int vertexIndex = charInfo.vertexIndex;
                                  TMP_MeshInfo[] cachedMeshInfo = targetText.textInfo.CopyMeshInfoVertexData();

                                  Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                                  if (vertices.Length <= vertexIndex + 3) return;

                                  // 如果啟用了波浪效果，則執行跳躍效果
                                  if (enableWaveEffect)
                                  {
                                      float jumpDuration = typingSpeed * jumpSpeedMultiplier;

                                      DOTween.To(() => 0f, v =>
                                      {
                                          float offsetY = Mathf.Sin(v * Mathf.PI) * jumpHeight;
                                          for (int i = 0; i < 4; i++)
                                          {
                                              vertices[vertexIndex + i] = cachedMeshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + i] + new Vector3(0, offsetY, 0);
                                          }
                                          targetText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                                      }, 1f, jumpDuration).SetEase(Ease.OutQuad);
                                  }
                              }
                          }

                      }, text.Length, totalTypingDuration)
                      .SetEase(Ease.Linear));

        typingSequence.OnComplete(() =>
        {
            //if (audioSource != null)
            //{
            //    audioSource.loop = false;
            //    audioSource.Stop();
            //}

            onComplete?.Invoke();
        });

        typingSequence.Restart();
    }

    private void TypingCompleted()
    {
        OnTypingComplete?.Invoke();
    }

    public void TypingCompletedWithDelay(float postTypingDelay)
    {
        DOTween.Sequence()
               .AppendInterval(postTypingDelay)
               .AppendCallback(() => OnTypingComplete?.Invoke());
    }


    public static void KillAll()
    {
        foreach (TypingEffect instance in activeInstances)
        {
            instance.KillSequence();
        }
    }

    public void ClearText()
    {
        StopAllCoroutines();
        tween =null;
        targetText.text = "";
    }

    private void KillSequence()
    {
        //if (typingSequence != null)
        //{
        //    typingSequence.Kill();
        //    Debug.Log("Sequence killed");
        //}
    }


    public void StartRichTextTyping()
    {
        StopAllCoroutines();
        StartCoroutine(TypeRichText(Type_String));
    }

    public void StartTypingSmoothJump()
    {
        targetText.text = "";
        StopAllCoroutines();
        originalVertices.Clear(); // 清空之前的基準
        StartCoroutine(TypeRichText_SmoothJump_NoShaking(Type_String));
    }

    private List<(string text, bool isRichTag)> ParseRichText(string input)
    {
        var result = new List<(string, bool)>();
        int i = 0;

        while (i < input.Length)
        {
            if (input[i] == '<')
            {
                int end = input.IndexOf('>', i);
                if (end != -1)
                {
                    result.Add((input.Substring(i, end - i + 1), true));
                    i = end + 1;
                }
                else
                {
                    result.Add((input[i].ToString(), false));
                    i++;
                }
            }
            else
            {
                result.Add((input[i].ToString(), false));
                i++;
            }
        }

        return result;
    }

    private IEnumerator TypeRichText(string fullText)
    {
        var parsedList = ParseRichText(fullText);
        string currentDisplay = "";
        int currentVisibleCharIndex = 0;

        foreach (var (segment, isTag) in parsedList)
        {
            currentDisplay += segment;
            targetText.text = currentDisplay;
            targetText.ForceMeshUpdate();

            if (!isTag)
            {
                TMP_TextInfo textInfo = targetText.textInfo;
                int lastIndex = currentVisibleCharIndex;

                if (lastIndex >= 0 && lastIndex < textInfo.characterCount)
                {
                    TMP_CharacterInfo charInfo = textInfo.characterInfo[lastIndex];
                    if (charInfo.isVisible)
                    {
                        int vertexIndex = charInfo.vertexIndex;
                        int matIndex = charInfo.materialReferenceIndex;

                        Vector3[] vertices = textInfo.meshInfo[matIndex].vertices;
                        Vector3[] originVertices = new Vector3[4];

                        for (int i = 0; i < 4; i++)
                            originVertices[i] = vertices[vertexIndex + i];

                        DOTween.To(() => 0f, v =>
                        {
                            float offsetY = Mathf.Sin(v * Mathf.PI) * jumpHeight;
                            for (int i = 0; i < 4; i++)
                                vertices[vertexIndex + i] = originVertices[i] + new Vector3(0, offsetY, 0);
                            targetText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                        }, 1f, typingSpeed * jumpSpeedMultiplier).SetEase(Ease.OutQuad);
                    }
                }

                currentVisibleCharIndex++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        TypingCompleted();
    }

    private IEnumerator TypeRichText_SmoothJump_NoShaking(string fullText)
    {
        targetText.text = "";
        var parsedList = ParseRichText(fullText);
        string currentDisplay = "";

        targetText.ForceMeshUpdate();

        // 清空並準備原始頂點陣列
        originalVertices.Clear();

        foreach (var (segment, isTag) in parsedList)
        {
            currentDisplay += segment;
            targetText.text = currentDisplay;
            targetText.ForceMeshUpdate();

            // 只保存「最新字元剛加入時的原始頂點」
            SaveOriginalVertices();

            if (!isTag)
            {
                int lastIndex = targetText.textInfo.characterCount - 1;
                if (lastIndex >= 0)
                {
                    TMP_CharacterInfo charInfo = targetText.textInfo.characterInfo[lastIndex];
                    if (charInfo.isVisible)
                    {
                        int vertexIndex = charInfo.vertexIndex;
                        int matIndex = charInfo.materialReferenceIndex;

                        // 將原始頂點複製出來作為基準（避免每次都用被修改的頂點）
                        Vector3[] baseVerts = originalVertices[matIndex];

                        DOTween.To(() => 0f, v =>
                        {
                            float offsetY = Mathf.Sin(v * Mathf.PI) * jumpHeight;
                            var verts = targetText.textInfo.meshInfo[matIndex].vertices;

                            for (int i = 0; i < 4; i++)
                            {
                                // 注意：這裡使用 baseVerts 作為基準
                                verts[vertexIndex + i] = baseVerts[vertexIndex + i] + new Vector3(0, offsetY, 0);
                            }

                            targetText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                        }, 1f, typingSpeed * jumpSpeedMultiplier).SetEase(Ease.OutQuad);
                    }
                }
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        TypingCompleted();
    }
    private void SaveOriginalVertices()
    {
        originalVertices.Clear();
        for (int i = 0; i < targetText.textInfo.meshInfo.Length; i++)
        {
            Vector3[] verts = targetText.textInfo.meshInfo[i].vertices;
            Vector3[] copy = new Vector3[verts.Length];
            Array.Copy(verts, copy, verts.Length);
            originalVertices.Add(copy);
        }
    }
}
