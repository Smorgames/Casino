using UnityEngine;
using TMPro;

public class Bet : MonoBehaviour
{
    public static Bet instance;

    private void Awake()
    {
        instance = this;
    }

    public int money = 1000;
    public TextMeshProUGUI moneyText;

    public int bet = 5;
    public TextMeshProUGUI betAmountText;


    public void Start()
    {
        SetBet(bet);
        SetMoney(1000);
    }

    public void ChangeBet()
    {
        if (bet == 5)
            SetBet(10);
        else if (bet == 10)
            SetBet(20);
        else if (bet == 20)
            SetBet(50);
        else if (bet == 50)
            SetBet(100);
        else if (bet == 100)
            SetBet(5);
    }

    private void SetBet(int betAmount)
    {
        bet = betAmount;
        betAmountText.text = bet.ToString() + "$";
    }

    public void SetMoney(int moneyAmount)
    {
        money = moneyAmount;
        moneyText.text = money.ToString() + "$";
    }

    public void SubtractBet()
    {
        money -= bet;
        SetMoney(money);
    }
}
