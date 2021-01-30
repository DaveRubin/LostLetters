using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OpenLetter : MonoBehaviour
{
    public bool isFlippable = true;

    public ECharacters rightAnswer = ECharacters.Daniel;
    private Transform paper;
    private GameObject back;
    private GameObject front;
    void Start()
    {
        paper = transform.Find("Base/Paper");
        back = transform.Find("Base/Paper/PaperBack").gameObject;
        front = transform.Find("Base/Paper/PaperFront").gameObject;
    }

    private void OnMouseDown()
    {
        FlipPaper();
    }

    void FlipPaper()
    {
        float duration = 1.5f;
        if (isFlippable)
        {
            float targetY = paper.transform.eulerAngles.y == 0 ? 180 : 0;
            paper.DORotate(new Vector3(0, targetY,0),duration).SetEase(Ease.InOutCirc);
            DOVirtual.DelayedCall(duration/2f, () =>
            {
                back.SetActive(targetY != 0);
                front.SetActive(targetY == 0);
            }).SetEase(Ease.Linear);
        }
    }

    public Tween MoveLetterTo(Transform mailboxTransform)
    {
        float duration = 1;
        Destroy(GetComponent<Animator>());
        paper.SetParent(mailboxTransform.parent.parent.parent);
        Debug.Log(mailboxTransform.position);
        Debug.Log(paper.position);
        paper.DOScale(Vector3.zero, duration).OnComplete(() =>
        {
            Destroy(paper.gameObject);
            Destroy(gameObject);
        });
        transform.DOLocalMove(new Vector3(0, -10, 0), duration);
        return paper.DOMove(mailboxTransform.position, duration);
    }
}