using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class Main : MonoBehaviour
{
    private static float letterAnimationDuration = 2;
    private static float letterPositionOffset = 0.5f;
    private static float letterRotationOffset = 10;
    public GameObject myPrefab;
    public Transform letterInPos;
    public Transform letterOutPos;

    public Timer timer;

    private int letterCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StackNewLetter", 2, 2);
        DOVirtual.Float(0, 1, 60*5,UpdateCallback).SetEase(Ease.Linear).OnComplete(FinishGame);
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
        letterCount++;
        Vector3 heightAddition = new Vector3(0, 0, -letterCount * 0.01f);
        GameObject newLetter = (GameObject)Instantiate(myPrefab, letterInPos.position+heightAddition, Quaternion.identity);
        Envelope envelope = newLetter.GetComponent<Envelope>();
        envelope.clicked += OnNewLetterClicked;
        envelope.letterName = String.Format("{0}#{1}", "Letter", letterCount);
        
        Vector3 offset = new Vector3(Random.Range(-letterPositionOffset, letterPositionOffset),
            Random.Range(-letterPositionOffset, letterPositionOffset)) + heightAddition;
        newLetter.transform
            .DOMove(letterOutPos.transform.position + offset, letterAnimationDuration);
        newLetter.transform.eulerAngles = new Vector3( 0, 0,Random.Range(-90, 90));
        newLetter.transform.DORotate(new Vector3( 0, 0,Random.Range(-letterRotationOffset, letterRotationOffset)), letterAnimationDuration);
    }

    private void OnNewLetterClicked(string letterName)
    {
        Debug.Log(letterName);
    }
}
