using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelCountdownView : MonoBehaviour
{
    [SerializeField] private AudioSource countdownSound;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private CanvasGroup _canvasGroup;
    [ContextMenu("Test anim")]
    public void TestAnimation()
    {
        StartCoroutine(StartCountdownRoutine());
    }
    public IEnumerator StartCountdownRoutine()
    {
        _canvasGroup.DOFade(1, 0.2f);
        Text.text = 3.ToString();
        Text.transform.localScale = Vector3.zero;
        PlayCountdownSound();
        yield return Text.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutQuint).WaitForCompletion();
        PlayCountdownSound();
        Text.text = 2.ToString();
        Text.transform.localScale = Vector3.zero;
        yield return Text.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutQuint).WaitForCompletion();
        PlayCountdownSound();
        Text.text = "ROLL TO PARADICE!!";
        Text.transform.localScale = Vector3.zero;
        yield return Text.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutQuint).WaitForCompletion();
        float shakeTime = 0.4f;
        Text.rectTransform.DOShakeRotation(shakeTime, 100, 100, 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(shakeTime*2);
        _canvasGroup.DOFade(0, 0.2f);
    }

    private void PlayCountdownSound()
    {
        if(countdownSound)
            countdownSound.Play();
    }
}
