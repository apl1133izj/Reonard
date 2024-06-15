using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class typing : MonoBehaviour
{
    public Text typingText;
    private string typingString ;
    public float typingSpeed = 0.1f;
    void Start()
    {
        typingString = typingText.text;
        typingText.text = ""; // 시작할 때 텍스트를 지워줍니다.
        StartCoroutine(TypingTextCoroutine());
    }


    IEnumerator TypingTextCoroutine()
    {
       
        foreach (char letter in typingString)
        {
            typingText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 한 글자씩 출력하는 시간을 조절할 수 있습니다.
          
        }
        typingText.text = "";
        StartCoroutine(TypingTextCoroutine());
    }
}
