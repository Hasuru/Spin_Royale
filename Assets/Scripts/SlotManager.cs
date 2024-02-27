using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private Slot[] slots;

    [Header("Multipliers")]
    [SerializeField] private int[] multipliers;

    [Header("Spin Status Text")]
    [SerializeField] private Text BetText;
    [SerializeField] private Text CreditText;
    [SerializeField] private Text JackpotText;

    // spin values
    private int bet;
    private int credit;
    private bool consecutive;
    private int currentMultiplier;

    public void Start()
    {
        Update_Credit(0);
        Update_Bet(0);
        currentMultiplier = multipliers[0];
        consecutive = false;
    }

    public void SpinSlots()
    {
        if (!ValidCredit() || !ValidBet()) return;

        int cnt = 0;
        
        foreach(Slot s in slots)
        {
            if (s.RunSlot() == 7) cnt++;
        }

        if (cnt == 0)
        {
            currentMultiplier = multipliers[cnt];
            consecutive = false;
            Update_Credit(-bet);
            JackpotText.text = "You Lost!\nMultiplier: " + currentMultiplier + "x";
            return;
        }

        if (consecutive) currentMultiplier += multipliers[cnt];
        else currentMultiplier = multipliers[cnt];
        consecutive = true;
        Update_Credit(currentMultiplier * bet);
        JackpotText.text = "You Won " + currentMultiplier * bet + "!\nMultiplier: " + currentMultiplier + "x";
    }

    public void Update_Credit(int value)
    {
        if (value == 0) credit = 0;
        else credit += value;
        CreditText.text = "" + credit;
    }

    public void Update_Bet(int value)
    {
        bet = value;
        BetText.text = "" + bet;
    } 

    public bool ValidBet()
    {
        if (bet <= 0)
        {
            JackpotText.text = "Invalid Bet Value!\nMultiplier: " + currentMultiplier + "x";
            return false;
        }

        return true;
    }

    public bool ValidCredit()
    {
        if (credit == 0 || credit - bet < 0)
        {
            JackpotText.text = "Insuficient Funds!\nMultiplier: " + currentMultiplier + "x";
            return false;
        }

        return true;
    }

}
