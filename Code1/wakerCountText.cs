using UnityEngine;
using UnityEngine.UI;
public class wakerCountText : MonoBehaviour
{
    public Text maxWarkertext;
    public Text[] warkertext; //0������ warker�� 1:���� �� warker��(������Ҽ��ִ�)
    Warkerhouse warkerhouse;
    private void Awake()
    {
        warkerhouse = GetComponentInParent<Warkerhouse>();
    }
    void Update()
    {
            maxWarkertext.text = warkerhouse.warkerMaxNumberUIText + "/";
            warkertext[1].text = warkerhouse.reWarkerNumber + "/";
            warkertext[0].text = "" + warkerhouse.warkerNumber;        
    }
}
