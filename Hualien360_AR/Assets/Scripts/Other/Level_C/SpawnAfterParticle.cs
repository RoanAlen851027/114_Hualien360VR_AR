/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
public class SpawnAfterParticle : MonoBehaviour
{
    public List<ParticleSystem> particle;
    public UnityEvent particle_EndEvent;
    private Coroutine coroutine;

    //答對撥放特效
    public void ShowCorrect(int count)
    {
        coroutine=StartCoroutine(SpawnAfterEffect(count));
    }

    public void ShowPatileEffect(int count)
    {
        // 播放粒子
        particle[count].Play();
    }

    IEnumerator SpawnAfterEffect(int count)
    {
        // 播放粒子
        particle[count].Play();

        // 等待粒子播放結束
        yield return new WaitForSeconds(particle[count].main.duration + particle[count].main.startLifetime.constantMax);

        particle_EndEvent?.Invoke();
    }
}
