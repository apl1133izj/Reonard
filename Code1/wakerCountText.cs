using UnityEngine;
using UnityEngine.UI;
public class wakerCountText : MonoBehaviour
{
    public Text maxWarkertext;
    public Text[] warkertext; //0생성한 warker수 1:집에 들어간 warker수(재생성할수있는)
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
