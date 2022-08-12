using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button leftArrow = null;
    [SerializeField] private Button rightArrow = null;
    [SerializeField] private Text currentSpeedText = null;
    [SerializeField] private Text collectedCoinText = null;
    [SerializeField] private Transform coinImage = null;
    private float currentMoveSpeed = 8;
    private float maxMoveSpeed = 10;
    private float minMoveSpeed = 3;
    
    public static UiManager uiInstance;

    void Awake()
    {
        uiInstance = this;
    }
    void Start()
    {
        leftArrow.onClick.AddListener(IncreaseMovementSpeed);
        rightArrow.onClick.AddListener(DecreaseMovementSpeed);
        SetSpeedText();
        SetCollectedCoins(0);
    }

    private void IncreaseMovementSpeed()
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {
            currentMoveSpeed++;
        }

        SetSpeedText();
    }

    private void DecreaseMovementSpeed()
    {
        if (currentMoveSpeed > minMoveSpeed)
        {
            currentMoveSpeed--;
        }
        SetSpeedText();
    }

    public float GetCurrentMovementSpeed()
    {
        return currentMoveSpeed;
    }

    private void SetSpeedText()
    {
        currentSpeedText.DOText("Speed : " + currentMoveSpeed.ToString(),0.5f,true,ScrambleMode.All);
    }

    public void SetCollectedCoins(int coinAmount)
    {
        coinImage.DOShakeScale(1f,1f,10);
        collectedCoinText.DOText("Collected Coins : "  + coinAmount.ToString(),0.5f,true,ScrambleMode.All);
    }
}
