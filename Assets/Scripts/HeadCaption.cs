using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeadCaption : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CaptionText;
    [SerializeField] RectTransform CaptionRectTransform;

    void Start()
    {
        //CaptionSet("!");
    }

    private void Update()
    {
        // look at camera
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
        this.transform.LookAt(targetPostition);
    }

    public void CaptionSet(string caption)
    {        
        CaptionRectTransform.DOScale(0, 0.2f).From().SetEase(Ease.OutFlash);
        CaptionText.text = caption;
    }
    
}
