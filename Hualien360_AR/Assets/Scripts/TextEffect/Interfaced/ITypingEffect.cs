using System;

public interface ITypingEffect
{
    float TypingSpeed { get; set; } // 打字速度屬性
    event Action OnTypingComplete; // 打字完成事件

    void StartTypingWithEvent(string text, float delay = 0f); // 啟動打字效果並在完成後觸發事件，帶有延遲參數
    void StartTypingEffectOnly(string text, float delay = 0f); // 單純的打字效果，無後續動作，帶有延遲參數
}
