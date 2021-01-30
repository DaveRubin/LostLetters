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
    public GameObject envelopePrefab;
    public Transform letterInPos;
    public Transform letterOutPos;
    public Transform targetLetterSelect;
    public StackClickController stackController;
    public Mailbox[] mailboxes;
    public Transform activeLetterParent;

    private Stack<Envelope> envelopStack = new Stack<Envelope>();
    private Envelope currentStackEnvelope;

    public Timer timer;

    private int letterCount = 0;
    private OpenLetter currentLetter;
    private static string[] avaiableCharacters = new string[] { "Daniel", "Regina", "Roger"};
    private static int[] charecterPlace = new int[] { 1,1,1};
     
    void Start()
    {
        InvokeRepeating("StackNewLetter", 1, config.timePerEnvelopInSeconds);
        StartTimer();
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
        
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
        DOVirtual.Float(0, 1, config.timeForGameInSeconds, UpdateCallback).SetEase(Ease.Linear)
            .OnComplete(()=>FinishGame(true));
    }

    private void UpdateCallback(float value)
    {
        timer.UpdateTimer(value);
    }

    private void FinishGame(bool timer)
    {
        if (timer)
        {
            Debug.Log("Times Up");    
        }
        else
        {
            Debug.Log("Finished");
        }
        
        CancelInvoke();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void StackNewLetter()
    {
        if (avaiableCharacters.Length == 0)
        {
            CancelInvoke();
            return;
        }

        // TODO: Remove highlight from stack envelops
        letterCount++;
        Vector3 heightAddition = new Vector3(0, 0, -letterCount * 0.01f);
        GameObject newLetter = Instantiate(envelopePrefab, letterInPos.position+heightAddition, Quaternion.identity);
        Envelope envelope = newLetter.GetComponent<Envelope>();
        envelope.letterName = $"Letter#{letterCount}";
        
        Vector3 offset = new Vector3(Random.Range(-config.letterPositionOffset, config.letterPositionOffset),
            Random.Range(-config.letterPositionOffset, config.letterPositionOffset))+ heightAddition;
        newLetter.transform
            .DOMove(letterOutPos.transform.position + offset, config.letterEnterAnimationDuration)
            .OnComplete(() =>
            {
                envelopStack.Push(envelope);
                envelope.transform.SetParent(stackController.transform);
            });
        newLetter.transform.eulerAngles = new Vector3( 0, 0,Random.Range(-90, 90));
        newLetter.transform.DORotate(new Vector3( 0, 0,
            Random.Range(-config.letterRotationOffset, config.letterRotationOffset)), config.letterEnterAnimationDuration);
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
            letter.SetHighlight(b);
        }
    }

    private void OpenANewLetter()
    {
        currentStackEnvelope = envelopStack.Pop();
        currentStackEnvelope.transform.SetParent(activeLetterParent);
        var sequence = DOTween.Sequence();
        sequence.Append(currentStackEnvelope.transform.DOMove(targetLetterSelect.transform.position, config.letterSelectedAnimationDuration));
        sequence.onComplete = () =>
        {
            Destroy(currentStackEnvelope.gameObject);
            currentStackEnvelope = null;
        };
        Transform startingPoint = transform.Find("EnvelopInit");
        int charIndex = Random.Range(0, avaiableCharacters.Length);
        int charPhase = charecterPlace[charIndex];
        string characterName = avaiableCharacters[charIndex];
        string prefabName = $"Prefabs/{characterName}/{charPhase}";
        Debug.Log(prefabName);
        GameObject envelope = Instantiate(Resources.Load (prefabName)) as GameObject;
        startingPoint.transform.SetParent(transform);
        envelope.transform.position = startingPoint.position;
        currentLetter = envelope.GetComponent<OpenLetter>();
        charecterPlace[charIndex]++;
        SetMailboxesInteractive();
        HighlightLettersInStack(false);

        if ( charecterPlace[charIndex] == 6)
        {
            charecterPlace = charecterPlace.Where((source, index) => index != charIndex).ToArray();
            avaiableCharacters = avaiableCharacters.Where((source, index) => index != charIndex).ToArray();
        }
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
        if (!currentLetter) return;
        
        if ( currentLetter.rightAnswer == mailbox.character)
        {
            currentLetter.MoveLetterTo(mailbox.transform).onComplete = OnMailboxInsertComplete;
        }
        else
        {
            currentLetter.MoveLetterTo(mailbox.transform).onComplete = OnMailboxInsertFail;
        }
    }

    private void OnMailboxInsertFail()
    {
        SetStackInteractive();
    }

    private void OnMailboxInsertComplete()
    {
        Debug.Log("OnMailboxInsertComplete");
        // TODO: Distribute points or whatever 
        SetStackInteractive();
        if (avaiableCharacters.Length == 0)
        {
            FinishGame(false);
        }
    }
}
