using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Main : MonoBehaviour
{
    public GameConfigurations config;
    public GameObject myPrefab;
    public Transform letterInPos;
    public Transform letterOutPos;
    public Transform targetLetterPosition;
    public StackClickController stackController;
    public Mailbox[] mailboxes;
    public Transform activeLetterParent;

    private Stack<Envelope> envelopStack = new Stack<Envelope>();
    private Envelope currentEnvelope;

    public Timer timer;

    private int letterCount = 0;
    void Start()
    {
        InvokeRepeating("StackNewLetter", 1, config.timePerEnvelopInSeconds);
        StartTimer();

        SetStackInteractive();
        RegisterToMailboxes();
        RegisterToStack();
    }

    private void RegisterToMailboxes()
    {
        foreach (var mailbox in mailboxes)
        {
            mailbox.mouseClicked += () => OnMailboxOnMouseClicked(mailbox);
        }
    }

    private void RegisterToStack()
    {
        stackController.mouseClick += DOOnStackMouse;
        stackController.mouseEnter += DOOnStackMouseEnter;
        stackController.mouseLeave += DOOnStackMouseLeave;
    }

    private void StartTimer()
    {
        DOVirtual.Float(0, 1, config.timeForGameInSeconds, UpdateCallback).SetEase(Ease.Linear).OnComplete(FinishGame);
    }

    private void UpdateCallback(float value)
    {
        timer.UpdateTimer(1-value);
    }

    private void FinishGame()
    {
        Debug.Log("Finish Game");
        CancelInvoke();
    }

    void StackNewLetter()
    {
        // TODO: Remove highlight from stack envelops
        letterCount++;
        Vector3 heightAddition = new Vector3(0, 0, -letterCount * 0.01f);
        GameObject newLetter = (GameObject)Instantiate(myPrefab, letterInPos.position+heightAddition, Quaternion.identity);
        Envelope envelope = newLetter.GetComponent<Envelope>();
        envelope.transform.SetParent(stackController.transform);
        envelopStack.Push(envelope);
        envelope.letterName = $"Letter#{letterCount}";
        
        Vector3 offset = new Vector3(Random.Range(-config.letterPositionOffset, config.letterPositionOffset),
            Random.Range(-config.letterPositionOffset, config.letterPositionOffset))+ heightAddition;
        newLetter.transform
            .DOMove(letterOutPos.transform.position + offset, config.letterEnterAnimationDuration);
        newLetter.transform.eulerAngles = new Vector3( 0, 0,Random.Range(-90, 90));
        newLetter.transform.DORotate(new Vector3( 0, 0,Random.Range(-config.letterRotationOffset, config.letterRotationOffset)), config.letterEnterAnimationDuration);
    }

    private void DOOnStackMouseEnter()
    {
        HighlightLettersInStack(true);
    }
    
    private void DOOnStackMouseLeave()
    {
        HighlightLettersInStack(false);
    }
    
    private void DOOnStackMouse()
    {
        Debug.Log("Stack clicked with envelop");
        OpenANewLetter();
    }

    private void HighlightLettersInStack(bool b)
    {
        foreach (var letter in envelopStack)
        {
            // TODO: Highlight the letters.
        }
    }

    private void OpenANewLetter()
    {
        currentEnvelope = envelopStack.Pop();
        currentEnvelope.transform.SetParent(activeLetterParent);
        currentEnvelope.transform.DORotate(new Vector3(0, 0, 0), config.letterUseAnimationDuration);
        currentEnvelope.transform.DOMove(targetLetterPosition.transform.position, config.letterUseAnimationDuration);
        SetMailboxesInteractive();
    }

    private void SetMailboxesInteractive()
    {
        stackController.SetInteractive(false);
        foreach (var mailbox in mailboxes)
        {
            mailbox.SetInteractive(true);
        }
    }
    
    private void SetStackInteractive()
    {
        stackController.SetInteractive(true);
        foreach (var mailbox in mailboxes)
        {
            mailbox.SetInteractive(false);
        }
    }
    
    private void OnMailboxOnMouseClicked(Mailbox mailbox)
    {
        Debug.Log("OnMailboxOnMouseClicked");
        if (currentEnvelope == null)
        {
            return;
        }
        // Show close envelop animation
        // TODO: Add animation
        // Send envelop to mailbox
        var sequence = DOTween.Sequence();
        sequence.Append(currentEnvelope.transform.DOMove(mailbox.targetEnvelopFront.position, 
            config.envelopMoveFrontMailboxAnimationDuration));
        sequence.Insert(0, 
            currentEnvelope.transform.DOScale(0.5f, config.envelopMoveFrontMailboxAnimationDuration));
        sequence.Append(currentEnvelope.transform.DOMove(mailbox.targetEnvelopInside.position,
            config.envelopMoveInsideMailboxAnimationDuration).SetEase(Ease.InBack));
        sequence.onComplete = OnMailboxInsertComplete;
    }

    private void OnMailboxInsertComplete()
    {
        Debug.Log("OnMailboxInsertComplete");
        // TODO: Distribute points or whatever 
        currentEnvelope = null;
        SetStackInteractive();
    }
}
