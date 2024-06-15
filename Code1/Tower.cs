using UnityEngine;
using UnityEngine.UI;
public class Tower : MonoBehaviour
{
    public Training training;
    public int warriorPointCount = 0;
    public Animator trainingAnimatorPageOn_A;
    public Animator towerIOffAnimator;
    public int towerButtonCount;
    public GameObject[] instSoldiers;
    public int[] SoldiersNumber;
    public int[] reSoldiersNumber;
    public int[] soldiersMaxNumber;
    public int[] soldiersMaxNumberUIText;
    public Transform towerPos;
    public string houseString;

    //UI ONOFF
    public GameObject[] uiOff;

    public Image[] wakerUIImage;
    float archerImageTime = 1;
    float warriorImageTime = 1;
    public Button reWaketButton;
    Button tranningexitButton;
    public Text WakerMaxCounttext;
    public enum SoldierActiveGroup { Null, Warrior, Archer }
    public SoldierActiveGroup soldierActiveGroupActiveGroup;

    private void Start()
    {
        GameObject trainingAnimatorPageOn_AGameObject = GameObject.Find("TrainingPage");
        GameObject towerIOffAnimatorGameObject = GameObject.Find("TrainingPage");
        GameObject tranningexitButtonGameObject = GameObject.Find("Exit Button");
        training = FindObjectOfType<Training>();
        trainingAnimatorPageOn_A = trainingAnimatorPageOn_AGameObject.GetComponent<Animator>();
        towerIOffAnimator = towerIOffAnimatorGameObject.GetComponent<Animator>();
        tranningexitButton = tranningexitButtonGameObject.GetComponent<Button>();
    }

    private void Awake()
    {
        towerPos = GetComponent<Transform>();
        houseString = gameObject.name;
        gameManager = FindObjectOfType<GameManager>();
    }
    public bool instWarriorButtonClicked;
    public bool instArcherButtonClicked;

    public GameManager gameManager;
    //����ư animator;
    public GameObject[] priceListGameObject;
    private void Update()
    {

        ActivationButton();
        if (instWarriorButtonClicked)
        {
            WarriorInstButton();
        }
        if (instArcherButtonClicked)
        {
            ArcherInstButton();
        }
    }
    public void instButtonfalse()
    {
        uiOff[0].SetActive(false);
        uiOff[1].SetActive(false);
        uiOff[2].SetActive(false);
    }
    public void WarriorButton()
    {
        instWarriorButtonClicked = true;
    }
    public void ArcherButton()
    {
        instArcherButtonClicked = true;
    }
    /********************************************/

    public Image[] activationButtonImage;//0~1:����,2~3:����,4~5:Ÿ��,6~7:������,8~9:���� ����,10~11:Ÿ�� ����
    void ActivationButton()
    {
        if (gameManager.moneyCount < 100 && wakerUIImage[0])
        {
            activationButtonImage[0].color = Color.gray;
            activationButtonImage[1].color = Color.gray;
        }
        else
        {
            activationButtonImage[0].color = Color.white;
            activationButtonImage[1].color = Color.white;
        }

        if (gameManager.moneyCount < 80 && wakerUIImage[1])
        {
            activationButtonImage[2].color = Color.gray;
            activationButtonImage[3].color = Color.gray;
        }
        else
        {
            activationButtonImage[2].color = Color.white;
            activationButtonImage[3].color = Color.white;
        }
    }

    void ArcherInstButton()
    {

        if (gameManager.moneyCount > 100)
        {
            archerImageTime -= Time.deltaTime;
            wakerUIImage[0].fillAmount = archerImageTime;
            if (SoldiersNumber[0] < soldiersMaxNumber[0] && archerImageTime <= 0)
            {
                Debug.Log("ArcherInstButton");
                soldierActiveGroupActiveGroup = SoldierActiveGroup.Archer;
                // Archer 생성
                CreateSoldier();

                gameManager.moneyCount -= 100;

                SoldiersNumber[0]++;
                soldiersMaxNumberUIText[0]--;
                archerImageTime = 1;
                wakerUIImage[0].fillAmount = 1;

                instArcherButtonClicked = false;
            }
        }
        else
        {
            instArcherButtonClicked = false;
        }
    }
    void WarriorInstButton()
    {
        
        if (gameManager.moneyCount > 80)
        {
            warriorImageTime -= Time.deltaTime;
            wakerUIImage[2].fillAmount = warriorImageTime;
            if (SoldiersNumber[1] < soldiersMaxNumber[1] && warriorImageTime <= 0)
            {Debug.Log("WarriorInstButton");
                soldierActiveGroupActiveGroup = SoldierActiveGroup.Warrior;
                // warker ����
                CreateSoldier();
                gameManager.foodCount -= 80;


                SoldiersNumber[1]++;
                soldiersMaxNumberUIText[1]--;
                warriorImageTime = 1;
                wakerUIImage[2].fillAmount = 1;
                instWarriorButtonClicked = false;
            }
        }
        else
        {
            instWarriorButtonClicked = false;
        } 
    }
    /***************************************************************************************/
    public Transform insparentPos;
    void CreateSoldier()
    {
        if (soldierActiveGroupActiveGroup == SoldierActiveGroup.Warrior )
        {

            GameObject WarriorGameObject = Instantiate(instSoldiers[0], towerPos.position, Quaternion.identity);
            WarriorGameObject.transform.parent = insparentPos;
            //대미지 관련 코드
            Warrior warriorScript = WarriorGameObject.GetComponent<Warrior>();
            warriorScript.houseName = gameObject.name;
            warriorScript.tagName = "GoblinWarrior";
        }
        if (soldierActiveGroupActiveGroup == SoldierActiveGroup.Archer)
        {

            GameObject archerGameObject = Instantiate(instSoldiers[1], towerPos.position, Quaternion.identity);
            archerGameObject.transform.parent = insparentPos;
            Archer archerScript = archerGameObject.GetComponent<Archer>();         
            archerScript.houseName = gameObject.name;
            archerScript.tagName = "Goblin Archer";
        }
    }
    public void TowerUIOn()
    {

        if (towerButtonCount == 0)
        {
            towerButtonCount++;
            towerIOffAnimator.enabled = true;
            towerIOffAnimator.SetBool("OffUIBool", false);
        }
        else if (towerButtonCount == 1)
        {
            towerButtonCount -= 1;

            towerIOffAnimator.SetBool("OffUIBool", true);
        }

    }

    public void TrainingButton()
    {
        trainingAnimatorPageOn_A.enabled = true;
        towerIOffAnimator.SetBool("OffUIBool", true);
        trainingAnimatorPageOn_A.SetBool("OnBool", true);
        trainingAnimatorPageOn_A.SetBool("OFFBool", false);
    }
    public void TrainingButtonOFF()
    {
        trainingAnimatorPageOn_A.SetBool("OnBool", false);
        trainingAnimatorPageOn_A.SetBool("OFFBool", true);
        towerIOffAnimator.enabled = true;
        towerIOffAnimator.SetBool("OffUIBool", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior"))
        {
            warriorPointCount += 1;
        }
    }
}
