using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        print(GetSymbolCount("Nice cock awesome balls and have a grate day", 'a'));
    }

    static int GetSymbolCount(string text, char symbol)
    {
        int count = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == symbol)
            {
                count++;
            }
        }

        return count;
    }
}
