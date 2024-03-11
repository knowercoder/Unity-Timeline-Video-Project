using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cuby : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] Transform EyeRoot;
    [SerializeField] Transform openEye;
    [SerializeField] Transform closedEye;

    Tween Sleeptwen;

    private void Start()
    {
        //SleepEyes();
    }

    public void SetEyeOpenState(bool state)
    {
        Sleeptwen.Complete();
        openEye.gameObject.SetActive(state);
        closedEye.gameObject.SetActive(!state);
    }

    public void BlinkEyes()
    {        
        Sleeptwen.Complete();
        StartCoroutine(BlinkCOR());  
    }

    IEnumerator BlinkCOR()
    {
        SetEyeOpenState(false);
        yield return new WaitForSeconds(0.25f);
        SetEyeOpenState(true);
    }

    public void SleepEyes()
    {
        SetEyeOpenState(false);
        Sleeptwen = EyeRoot.DOLocalMoveY(0.04f, 2).From().SetEase(Ease.OutQuint).SetLoops(7);
    }
   
}
