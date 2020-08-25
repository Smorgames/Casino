using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private void Awake()
    {
        instance = this;
    }

    public int money;
    public TextMeshProUGUI moneyText;

    [HideInInspector]
    public int bet;

    public TextMeshProUGUI betText;

    public GameObject[] slots;
    public Sprite[] slotSprites;

    private int[] slotArray;

    public GameObject leverArm;
    private LeverArm leverArmComponent;

    public GameObject[] winEffects;

    public Button[] buttons;

    private void Start()
    {
        AudioManager.instance.Play("BackgroundTheme");
        slotArray = new int[3];
        leverArmComponent = leverArm.GetComponent<LeverArm>();
        bet = 10;
        betText.text = bet.ToString() + " $";
        SetMoney(money);
    }

    public void StartRoll()
    {
        GenerateRandomIntArray();
        StartCoroutine(RollingAnimation());
        AudioManager.instance.Play("Rolling");
    }

    IEnumerator RollingAnimation()
    {
        SubtractMoney(bet);
        ChangeButtonsEnabled(false);

        for (int i = 0; i < slots.Length; i++)
            slots[i].GetComponent<Animator>().SetTrigger("Roll");

        for (int i = 0; i < slots.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            slots[i].GetComponent<Animator>().SetTrigger(slotArray[i].ToString());
        }

        if (slotArray[0] == slotArray[1] && slotArray[1] == slotArray[2])
        {
            SetMoney(money + 3 * bet);
            AudioManager.instance.Play("Win");
        }

        ChangeButtonsEnabled(true);

        yield return new WaitForSeconds(0.2f);
        leverArmComponent.ChangeLeverArm(1, true); // pull up lever arm and activate it's collider
    }

    private void GenerateRandomIntArray()
    {
        for (int i = 0; i < slotArray.Length; i++)
            slotArray[i] = Random.Range(0, 4);
    }

    public void ChangeBet()
    {
        if (bet == 10)
            bet = 50;
        else if (bet == 50)
            bet = 100;
        else if (bet == 100)
            bet = 200;
        else if (bet == 200)
            bet = 500;
        else if (bet == 500)
            bet = 1000;
        else if (bet == 1000)
            bet = 10;

        betText.text = bet.ToString() + " $";
    }

    private void SetMoney(int moneyAmount)
    {
        money = moneyAmount;
        moneyText.text = money.ToString() + " $";
    }

    private void SubtractMoney(int moneyAmount)
    {
        money -= moneyAmount;
        SetMoney(money);
    }

    private void ChangeButtonsEnabled(bool boolVariable)
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].interactable = boolVariable;
    }
}
