using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MirillaAnimation : MonoBehaviour
{
    public PlayerController playerController;
    public Image Icon_L;
    public Image Icon_R;
    private Tween animationButonL;
    private Tween animationButonR;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        animationButonL = Icon_L.rectTransform.DOAnchorPosX(-50, 1f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
        animationButonR = Icon_R.rectTransform.DOAnchorPosX(50, 1f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDisable()
    {
        //Reset positions
        Icon_L.rectTransform.localPosition = new Vector3(-30, 3, 1);
        Icon_R.rectTransform.localPosition = new Vector3(30, 3, 1);

        animationButonL.Kill();
        animationButonR.Kill();
    }
}
