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
        typingText.text = ""; // ������ �� �ؽ�Ʈ�� �����ݴϴ�.
        StartCoroutine(TypingTextCoroutine());
    }


    IEnumerator TypingTextCoroutine()
    {
       
        foreach (char letter in typingString)
        {
            typingText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // �� ���ھ� ����ϴ� �ð��� ������ �� �ֽ��ϴ�.
          
        }
        typingText.text = "";
        StartCoroutine(TypingTextCoroutine());
    }
}
