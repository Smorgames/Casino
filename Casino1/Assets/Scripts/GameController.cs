using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

[System.Serializable]
public class Offset
{
    public float spacing;

    public float x;
    public float y;
}

public class GameController : MonoBehaviour
{
    public int lenght;
    public int height;

    public GameObject empty;
    public Offset offset;

    private int[,] combination;
    //private GameObject[,] rollingResult;

    public Sprite[] sprites;

    public GameObject imageParentPrefab;

    public Button[] buttons;

    public GameObject rollAnimation;

    //public TextMeshProUGUI[] test; // test

    public TextMeshProUGUI winResult;

    public GameObject[] frames;
    private GameObject[,] framesMatrix;

    private void Start()
    {
        combination = new int[lenght,height];
        framesMatrix = new GameObject[lenght,height];
        AudioManager.instance.Play("BackgroundTheme");
        SetFramesMatrix();
    }

    public void Roll()
    {
        if (Bet.instance.money < Bet.instance.bet)
            return;

        AudioManager.instance.Play("Rolling");
        Bet.instance.SubtractBet(); // when start rolling subtract player's bet
        BlockButtons(buttons, true);
        winResult.text = ""; // clean up win result text

        for (int i = 0; i < frames.Length; i++)
        {
            frames[i].SetActive(false);
        }

        if (GameObject.FindWithTag("ImageParent") != null)
            CleanRollingREsults();

        StartCoroutine(SetRollingResult());

        rollAnimation.SetActive(true); // start rolling animation
    }

    private void ChooseSprite(int randomNumber, GameObject slot)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (randomNumber == i)
                slot.GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }

    private void CleanRollingREsults()
    {
        Destroy(GameObject.FindWithTag("ImageParent"));
    }

    IEnumerator SetRollingResult()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject imageParent = (GameObject)Instantiate(imageParentPrefab, transform.position, Quaternion.identity);
        for (int i = 0; i < combination.GetLength(0); i++)
        {
            for (int j = 0; j < combination.GetLength(1); j++)
            {
                combination[i, j] = Random.Range(0, sprites.Length); //fill matrix of combinations
                GameObject slot = (GameObject)Instantiate(empty, new Vector2(i * offset.spacing + offset.x, j * offset.spacing + offset.y), Quaternion.identity);
                ChooseSprite(combination[i, j], slot);
                slot.transform.SetParent(GameObject.FindWithTag("ImageParent").transform);
                yield return new WaitForSeconds(0.05f);
            }
        }
        rollAnimation.SetActive(false); // end rolling animation

        //Test();

        CheckResult();
        BlockButtons(buttons, false);
    }

    private void CheckResult()
    {
        int sumResult = 0;

        for (int i = 0; i < height; i++) // if horizontal lines 3 the same numbers => win
        {
            if (combination[0, i] == combination[1, i] && combination[1, i] == combination[2, i])
            {
                Bet.instance.SetMoney(Bet.instance.money + Bet.instance.bet * 4);
                sumResult += Bet.instance.bet * 4;
                framesMatrix[0, i].SetActive(true);
                framesMatrix[1, i].SetActive(true);
                framesMatrix[2, i].SetActive(true);
            }
        }

        if (combination[0, 0] == combination[1, 1] && combination[2, 2] == combination[1, 1]) //  if diagonal lines 3 the same numbers => win
        {
            Bet.instance.SetMoney(Bet.instance.money + Bet.instance.bet * 4);
            sumResult += Bet.instance.bet * 4;
            framesMatrix[0, 0].SetActive(true);
            framesMatrix[1, 1].SetActive(true);
            framesMatrix[2, 2].SetActive(true);
        }

        if (combination[0, 2] == combination[1, 1] && combination[2, 0] == combination[1, 1]) //  if diagonal lines 3 the same numbers => win
        {
            Bet.instance.SetMoney(Bet.instance.money + Bet.instance.bet * 4);
            sumResult += Bet.instance.bet * 4;
            framesMatrix[0, 2].SetActive(true);
            framesMatrix[1, 1].SetActive(true);
            framesMatrix[2, 0].SetActive(true);
        }

        if (sumResult > 0)
        {
            winResult.text = "+" + sumResult.ToString() + "$";
            AudioManager.instance.Play("Win");
            winResult.GetComponent<Animator>().SetTrigger("Win");
        }

        // if sumResult > 0 => play win animation and show win resault on the screen
        // show connection line between win images
    }

    private void BlockButtons(Button[] blockingButtons, bool needToBlock)
    {
        if (needToBlock)
        {
            for (int i = 0; i < blockingButtons.Length; i++)
            {
                blockingButtons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < blockingButtons.Length; i++)
            {
                blockingButtons[i].interactable = true;
            }
        }
    }


    private void SetFramesMatrix()
    {
        framesMatrix[0, 0] = frames[0];
        framesMatrix[0, 1] = frames[1];
        framesMatrix[0, 2] = frames[2];

        framesMatrix[1, 0] = frames[3];
        framesMatrix[1, 1] = frames[4];
        framesMatrix[1, 2] = frames[5];

        framesMatrix[2, 0] = frames[6];
        framesMatrix[2, 1] = frames[7];
        framesMatrix[2, 2] = frames[8];
    }

    public void Quit()
    {
        Application.Quit();
    }

    //private void Test()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            print(combination[i, j]);
    //        }
    //    }

    //    test[0].text = combination[0, 0].ToString();
    //    test[1].text = combination[0, 1].ToString();
    //    test[2].text = combination[0, 2].ToString();

    //    test[3].text = combination[1, 0].ToString();
    //    test[4].text = combination[1, 1].ToString();
    //    test[5].text = combination[1, 2].ToString();

    //    test[6].text = combination[2, 0].ToString();
    //    test[7].text = combination[2, 1].ToString();
    //    test[8].text = combination[2, 2].ToString();
    //}
}
