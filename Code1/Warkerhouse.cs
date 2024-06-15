using UnityEngine;
using UnityEngine.UI;

public class Warkerhouse : MonoBehaviour
{

    public GameObject instWarker;
    public int warkerNumber;
    public int reWarkerNumber;
    public int warkerMaxNumber;
    public int warkerMaxNumberUIText;
    public Transform housePos;
    public string houseString;

    //UI ONOFF
    public GameObject wakerGameObjectUI;
    public GameObject reWakerInstUI;
    public GameObject[] uiOff;

    public Image[] wakerUIImage;
    float wakerTreeUIImageTime = 1;
    float wakerMineUIImageTime = 1;
    float reWakerUIImageTime = 1;
    int countButton;

    public Button reWaketButton;
    public Text WakerMaxCounttext;
    public enum WakerActiveGroup { Null, Tree, Gold, Build }
    public WakerActiveGroup wakerActiveGroup;



    public GameObject buildKindGameObject;
    private void Awake()
    {
        housePos = GetComponent<Transform>();
        houseString = gameObject.name;
        BuildeMouseAndTouch = FindObjectOfType<buildeMouseAndTouch>();
        gameManager = FindObjectOfType<GameManager>();
        houseUIAnimator = GetComponent<Animator>();
    }
    public bool instTreeButtonClicked;
    public bool instMineButtonClicked;

    public bool instBuildButtonClicked;
    public bool instBuildHouseButtonClicked;
    public bool instBuildMineButtonClicked;
    public bool instBuildTowerButtonClicked;
    public bool reInstButtonClicked;

    public buildeMouseAndTouch BuildeMouseAndTouch;
    public GameManager gameManager;
    //����ư animator;
    public Animator buildanimator;
    public Animator houseUIAnimator;
    public GameObject[] priceListGameObject;
    public bool buyBuild;
    private void Update()
    {
        if (reWarkerNumber >= 1)
        {
            ActivationButton();
        }

        if (instTreeButtonClicked)
        {
            WakerTreeInstButton();
        }
        if (instMineButtonClicked)
        {
            WakerMainInstButton();
        }
        if (instBuildButtonClicked)
        {

            if (buildCount == 1)
            {
                buildanimator.SetBool("buttonFaselBool", false);
                buildanimator.enabled = true;
                uiOff[0].SetActive(false);
                uiOff[1].SetActive(false);
                uiOff[2].SetActive(false);
                priceListGameObject[3].SetActive(true);
                priceListGameObject[0].SetActive(false);
                priceListGameObject[1].SetActive(false);
                priceListGameObject[2].SetActive(false);
            }
            else if (buildCount == 2)
            {
                priceListGameObject[0].SetActive(true);
                priceListGameObject[1].SetActive(true);
                priceListGameObject[2].SetActive(true);
                priceListGameObject[3].SetActive(false);
                buildanimator.SetBool("buttonFaselBool", true);
                buildKindGameObject.SetActive(false);
                if (buyBuild)
                {
                    if (gameManager.foodCount > 49)
                    {
                        buyBuild = false;
                        gameManager.foodCount -= 50;
                    }
                }
                else
                {
                    gameManager.foodCount -= 0;
                }
            }

        }

        if (instBuildHouseButtonClicked)
        {
            WakerBuildHouseInstButton();
        }
        if (instBuildMineButtonClicked)
        {
            WakerBuildMineInstButton();
        }
        if (instBuildTowerButtonClicked)
        {
            WakerBuildTowerInstButton();
        }

        BuildWarker();

        if (reInstButtonClicked)
        {
            RegenerativeWakerButton();
        }
    }
    public void instButtonfalse()
    {
        uiOff[0].SetActive(false);
        uiOff[1].SetActive(false);
        uiOff[2].SetActive(false);
    }
    public void WakerTreeButton()
    {
        instTreeButtonClicked = true;
    }
    public void WakerMainButton()
    {
        instMineButtonClicked = true;
    }
    public int buildCount;
    public void WakerBuildButton()
    {

        instBuildButtonClicked = true;

        buildCount++;
        wakerActiveGroup = WakerActiveGroup.Build;

    }

    /********************************************/

    public void WakerBuildHouseButton()
    {
        if (gameManager.moneyCount >= 100)
        {
            BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.BuildHouse;
            BuildeMouseAndTouch.buildStart = true;
            instBuildHouseButtonClicked = true;
        }
    }
    public void WakerBuildMineButton()
    {
        if (gameManager.moneyCount >= 90)
        {
            BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.BuildMine;
            instBuildMineButtonClicked = true;
            BuildeMouseAndTouch.buildStart = true;
        }
    }
    public void WakerBuildTowerButton()
    {
        if (gameManager.moneyCount >= 85)
        {
            BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.BuildTower;
            instBuildTowerButtonClicked = true;
            BuildeMouseAndTouch.buildStart = true;
        }
    }
    public void WakerBuildCancellationButton()
    {
        BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
        instBuildTowerButtonClicked = false;
        BuildeMouseAndTouch.buildStart = false;
    }

    /********************************************/
    public void RewakerButton()
    {
        reInstButtonClicked = true;
    }
    public Image[] activationButtonImage;
    void ActivationButton()
    {
        if (gameManager.foodCount < 35 && wakerUIImage[0])
        {
            activationButtonImage[0].color = Color.gray;
            activationButtonImage[1].color = Color.gray;
        }
        else
        {
            activationButtonImage[0].color = Color.white;
            activationButtonImage[1].color = Color.white;
        }

        if (gameManager.foodCount < 25 && wakerUIImage[1])
        {
            activationButtonImage[2].color = Color.gray;
            activationButtonImage[3].color = Color.gray;
        }
        else
        {
            activationButtonImage[2].color = Color.white;
            activationButtonImage[3].color = Color.white;
        }

        if (gameManager.foodCount < 50 && wakerUIImage[2])
        {
            activationButtonImage[4].color = Color.gray;
            activationButtonImage[5].color = Color.gray;
        }
        else
        {
            activationButtonImage[4].color = Color.white;
            activationButtonImage[5].color = Color.white;
        }

        if (gameManager.moneyCount < 100 && wakerUIImage[3])
        {
            activationButtonImage[6].color = Color.gray;
            activationButtonImage[7].color = Color.gray;
        }
        else
        {
            activationButtonImage[6].color = Color.white;
            activationButtonImage[7].color = Color.white;
        }

        if (gameManager.moneyCount < 90 && wakerUIImage[4])
        {
            activationButtonImage[8].color = Color.gray;
            activationButtonImage[9].color = Color.gray;
        }
        else
        {
            activationButtonImage[8].color = Color.white;
            activationButtonImage[9].color = Color.white;
        }

        if (gameManager.moneyCount < 85 && wakerUIImage[5])
        {
            activationButtonImage[10].color = Color.gray;
            activationButtonImage[11].color = Color.gray;
        }
        else
        {
            activationButtonImage[10].color = Color.white;
            activationButtonImage[11].color = Color.white;
        }
    }
    void WakerTreeInstButton()
    {
        if (gameManager.foodCount > 35 || reWarkerNumber >= 1)
        {
            wakerTreeUIImageTime -= Time.deltaTime;
            wakerUIImage[0].fillAmount = wakerTreeUIImageTime;
            if (warkerNumber < warkerMaxNumber && wakerTreeUIImageTime <= 0)
            {
                wakerActiveGroup = WakerActiveGroup.Tree;
                
                CreateWarker();
                if (reWarkerNumber == 0)
                {
                    gameManager.foodCount -= 35;
                }
                else
                {
                    reWarkerNumber--;
                }
                warkerNumber++;
                warkerMaxNumberUIText--;
                wakerTreeUIImageTime = 1;
                wakerUIImage[0].fillAmount = 1;
                instTreeButtonClicked = false;
            }
        }
        else
        {
            instTreeButtonClicked = false;
        }
    }
    void WakerMainInstButton()
    {

        if (gameManager.foodCount > 25 || reWarkerNumber >= 1)
        {
            wakerMineUIImageTime -= Time.deltaTime;
            wakerUIImage[2].fillAmount = wakerMineUIImageTime;
            if (warkerNumber < warkerMaxNumber && wakerMineUIImageTime <= 0)
            {
                wakerActiveGroup = WakerActiveGroup.Gold;
                
                CreateWarker();

                if (reWarkerNumber == 0)
                {
                    gameManager.foodCount -= 25;
                }
                else
                {
                    reWarkerNumber--;
                }

                warkerNumber++;
                warkerMaxNumberUIText--;
                wakerMineUIImageTime = 1;
                wakerUIImage[2].fillAmount = 1;

                instMineButtonClicked = false;
            }
        }
        else
        {
            instMineButtonClicked = false;
        }
    }
    /***************************************************************************************/

    public Transform insparentPos;
    void WakerBuildHouseInstButton()
    {

        if (gameManager.moneyCount >= 100 || reWarkerNumber >= 1)
        {
            if (warkerNumber < warkerMaxNumber)
            {

                // warker ����
                CreateWarker();
                buyBuild = true;
                if (reWarkerNumber == 0)
                {
                    gameManager.moneyCount -= 100;
                }
                else
                {
                    reWarkerNumber--;
                }

                warkerNumber++;
                warkerMaxNumberUIText--;

                instBuildHouseButtonClicked = false;
            }
        }
        else
        {
            instBuildHouseButtonClicked = false;
        }
    }
    void WakerBuildMineInstButton()
    {
        if (gameManager.moneyCount >= 90 || reWarkerNumber >= 1)
        {


            if (warkerNumber < warkerMaxNumber)
            {
              
                CreateWarker();

                warkerNumber++;
                warkerMaxNumberUIText--;
                if (reWarkerNumber == 0)
                {
                    gameManager.moneyCount -= 90;
                }
                else
                {
                    reWarkerNumber--;
                }
                instBuildMineButtonClicked = false;
            }
        }
        else
        {
            instBuildMineButtonClicked = false;
        }
    }
    void WakerBuildTowerInstButton()
    {
        if (gameManager.moneyCount >= 85 || reWarkerNumber >= 1)
        {


            if (warkerNumber < warkerMaxNumber)
            {
               
                CreateWarker();
                warkerNumber++;
                warkerMaxNumberUIText--;
                if (reWarkerNumber == 0)
                {
                    gameManager.moneyCount -= 85;
                }
                else
                {
                    reWarkerNumber--;
                }
                instBuildTowerButtonClicked = false;
            }
        }
        else
        {
            instBuildTowerButtonClicked = false;
        }
    }

    void RegenerativeWakerButton()
    {
        if (reWarkerNumber > 0)
        {
            reWakerUIImageTime -= Time.deltaTime;
            wakerUIImage[1].fillAmount = reWakerUIImageTime;
        }

        if (reWakerUIImageTime <= 0 && reWarkerNumber > 0)
        {
           
            CreateWarker();
            reWarkerNumber--;
            warkerMaxNumberUIText--;
            reWakerUIImageTime = 1;
            wakerUIImage[1].fillAmount = 1;
            reInstButtonClicked = false;
        }
    }
    /***************************************************************************************/
    void CreateWarker()
    {
        if (wakerActiveGroup == WakerActiveGroup.Tree)
        {

            GameObject warkerGameObject = Instantiate(instWarker, housePos.position, Quaternion.identity);
            warkerGameObject.transform.parent = insparentPos;
            Warker warkerScript = warkerGameObject.GetComponent<Warker>();
            warkerScript.houseName = gameObject.name;
            warkerScript.tagName = "Tree";
        }
        if (wakerActiveGroup == WakerActiveGroup.Gold)
        {

            GameObject warkerGameObject = Instantiate(instWarker, housePos.position, Quaternion.identity);
            warkerGameObject.transform.parent = insparentPos;
            Warker warkerScript = warkerGameObject.GetComponent<Warker>();
            warkerScript.houseName = gameObject.name;
            warkerScript.tagName = "Mine";
        }
    }
    public void BuildWarker()
    {
        if (BuildeMouseAndTouch.buildWarkerInstBool)
        {
            if (BuildeMouseAndTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildHouse)
            {
                GameObject warkerGameObject = Instantiate(instWarker, housePos.position, Quaternion.identity);
                warkerGameObject.transform.parent = insparentPos;
                Warker warkerScript = warkerGameObject.GetComponent<Warker>();
                warkerScript.houseName = gameObject.name;
                warkerScript.tagName = "BuildHouse";
                BuildeMouseAndTouch.buildEnd = false;
                buyBuild = true;
                BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
                BuildeMouseAndTouch.buildWarkerInstBool = false;

            }
            if (BuildeMouseAndTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildMine)
            {
                GameObject warkerGameObject = Instantiate(instWarker, housePos.position, Quaternion.identity);
                warkerGameObject.transform.parent = insparentPos;
                Warker warkerScript = warkerGameObject.GetComponent<Warker>();
                warkerScript.houseName = gameObject.name;
                warkerScript.tagName = "BuildMine";
                BuildeMouseAndTouch.buildEnd = false;
                buyBuild = true;
                BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
                BuildeMouseAndTouch.buildWarkerInstBool = false;
            }
            if (BuildeMouseAndTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildTower)
            {
                GameObject warkerGameObject = Instantiate(instWarker, housePos.position, Quaternion.identity);
                warkerGameObject.transform.parent = insparentPos;
                Warker warkerScript = warkerGameObject.GetComponent<Warker>();
                warkerScript.houseName = gameObject.name;
                warkerScript.tagName = "BuildTower";
                BuildeMouseAndTouch.buildEnd = false;
                buyBuild = true;
                BuildeMouseAndTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
                BuildeMouseAndTouch.buildWarkerInstBool = false;
            }
        }
    }
    public void InstWarker()
    {

        if (countButton == 0)
        {
            countButton++;
            houseUIAnimator.enabled = true;
            wakerGameObjectUI.SetActive(true);
            reWakerInstUI.SetActive(true);
        }
        else if (countButton == 1)
        {
            countButton -= 1;
            houseUIAnimator.SetBool("OffUIBool", true);
        }
    }
    public void HouseUIFalse()
    {
        houseUIAnimator.SetBool("OffUIBool", false);
        houseUIAnimator.enabled = false;
    }
}

