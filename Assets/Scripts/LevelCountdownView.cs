using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelCountdownView : MonoBehaviour
{
    [SerializeField] private AudioClip readySound, goSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        EventBroker.Instance.OnLevelCountdownStart += TestAnimation;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnLevelCountdownStart -= TestAnimation;
    }

    [ContextMenu("Test anim")]
    public void TestAnimation(float value)
    {
        gameObject.SetActive(true);
        StartCoroutine(StartCountdownRoutine());
    }
    public IEnumerator StartCountdownRoutine()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.2f);
        Text.text = "Ready?!";
        Text.transform.localScale = Vector3.zero;
        PlayCountdownSound(readySound);
       
        Text.transform.localScale = Vector3.zero;
        yield return Text.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutQuint).WaitForCompletion();
        PlayCountdownSound(goSound);
        Text.text = "ROLL TO PARADICE!!";
        Text.transform.localScale = Vector3.zero;
        yield return Text.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutQuint).WaitForCompletion();
        float shakeTime = 0.4f;
        Text.rectTransform.DOShakeScale(shakeTime, 2, 1, 1,true).SetEase(Ease.InOutBack).OnComplete((() => Text.transform.localScale = Vector3.one)).Loops();
        yield return new WaitForSeconds(shakeTime*2);
        _canvasGroup.DOFade(0, 0.2f).OnComplete((() => gameObject.SetActive(false)));
    }

    private void PlayCountdownSound(AudioClip audioClip)
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}