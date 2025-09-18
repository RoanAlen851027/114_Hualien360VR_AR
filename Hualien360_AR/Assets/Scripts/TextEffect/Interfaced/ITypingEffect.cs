using System;

public interface ITypingEffect
{
    float TypingSpeed { get; set; } // ���r�t���ݩ�
    event Action OnTypingComplete; // ���r�����ƥ�

    void StartTypingWithEvent(string text, float delay = 0f); // �Ұʥ��r�ĪG�æb������Ĳ�o�ƥ�A�a������Ѽ�
    void StartTypingEffectOnly(string text, float delay = 0f); // ��ª����r�ĪG�A�L����ʧ@�A�a������Ѽ�
}
