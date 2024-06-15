using TMPro;
using UnityEngine;
public class Training : MonoBehaviour
{
    public GameManager gameManager;
    Warrior warrior;
    Archer archer;
    //Training

    public enum TrainingType { Null, Arrow_len, Arrow_DamageUP, Warrior_DamageUp, Warrior_HpUp };
    public TrainingType[] trainingTypeUI = new TrainingType[4]; // 초기화 추가
    //버튼을누르면 버튼 애니메이션 활성화
    public Animator[] level_Gauge_GameObject;
    //현재레벨{"0:arrow_Len", "1:arrow_Power","2:warrior_Damage", "3:Warrior_HpUp"}
    public int[] level = new int[4];
    public Animator trainingAnimatorPageOn_A;
    //애니메이션 활성화
    public GameObject[] arrowLenLevelGameObjects;
    public GameObject[] arrowPowerLevelGameObject;
    public GameObject[] warriorHpLevelGameObjects;
    public GameObject[] warriorPowerLevelGameObject;

    //훈련회수 세기{"0:arrow_Len", "1:arrow_Power","2:warrior_Damage", "3:Warrior_HpUp"}
    public int[] trainingCounts = new int[4];
    //훈련회수 세기{"0:arrow_Len", "1:arrow_Power","2:warrior_Damage", "3:Warrior_HpUp"}
    public int[][] trainingMaxCounts = new int[4][];

    public TextMeshProUGUI[] trainingLevelText;
    public TextMeshProUGUI[] trainingCountText;
    public TextMeshProUGUI[] trainingCostsText;

    public int[][] enhance = new int[4][];

    bool[] buttonBool = { true, true, true, true };

    private void Start()
    {
    
        enhance[0] = new int[] { 3, 4, 5, 6, 7 };//궁수 사거리
        enhance[1] = new int[] { 10, 12, 14, 16, 20 };//궁수 대미지
        enhance[2] = new int[] { 4, 5, 6, 7, 10 };//전사 대미지
        enhance[3] = new int[] { 200, 230, 260, 290, 320 };//전사 체력

        trainingMaxCounts[0] = new int[] { 30, 40, 50, 60, 70 };
        trainingMaxCounts[1] = new int[] { 40, 50, 60, 70, 80 };
        trainingMaxCounts[2] = new int[] { 35, 55, 65, 80, 100 };
        trainingMaxCounts[3] = new int[] { 30, 40, 50, 60, 70 };
    }
    private void Update()
    {
        warrior = FindObjectOfType<Warrior>();
        archer = FindObjectOfType<Archer>();
        if (warrior != null)
        {
            warrior.warriorHp = enhance[3][level[3]];
        }
        else if (archer != null)
        {
            archer.attackRange = enhance[0][level[0]];
        }

        //레벨이 올랐을경우 = 훈련종료 즉 애니메이션 종료
        FalseAnimation();
    }

    void FalseAnimation()
    {
        UpdateTrainingUI();
    }
    public void ExitButton()
    {
        trainingAnimatorPageOn_A.SetBool("OnBool", false);
        trainingAnimatorPageOn_A.SetBool("OFFBool", true);
        trainingAnimatorPageOn_A.enabled = true;
    }
    void UpdateTrainingUI()
    {
        // 각 훈련 타입에 대한 훈련 정보 업데이트

        if (trainingCounts[0] == 0)
        {
            buttonBool[0] = true;
            if (level[0] == 1)
            {
                trainingLevelText[0].text = "LV:" + level[0].ToString();
                trainingCountText[0].text = "사거리\n 증가";
                trainingCostsText[0].text = "이 " + trainingMaxCounts[0][1] + "개 필요합니다.";

                arrowLenLevelGameObjects[level[0] - 1].SetActive(false);
            }
            else if (level[0] == 2)
            {
                trainingLevelText[0].text = "LV:" + level[0].ToString();
                trainingCountText[0].text = "사거리\n 증가";
                trainingCostsText[0].text = "이 " + trainingMaxCounts[0][2] + "개 필요합니다.";

                arrowLenLevelGameObjects[level[0] - 1].SetActive(false);
            }
            else if (level[0] == 3)
            {
                trainingLevelText[0].text = "LV:" + level[0].ToString();
                trainingCountText[0].text = "사거리\n 증가";
                trainingCostsText[0].text = "이 " + trainingMaxCounts[0][3] + "개 필요합니다.";

                arrowLenLevelGameObjects[level[0] - 1].SetActive(false);
            }
            else if (level[0] == 4)
            {
                trainingLevelText[0].text = "LV:" + level[0].ToString();
                trainingCountText[0].text = "사거리\n 증가";
                trainingCostsText[0].text = "이 " + trainingMaxCounts[0][4] + "개 필요합니다.";

                arrowLenLevelGameObjects[level[0] - 1].SetActive(false);
            }
        }



        if (trainingCounts[1] == 0)
        {
            buttonBool[1] = true;
            if (level[1] == 1)
            {
                trainingLevelText[1].text = "LV:" + level[1].ToString();
                trainingCountText[1].text = "공격력\n 증가";
                trainingCostsText[1].text = "이 " + trainingMaxCounts[1][1] + "개 필요합니다.";

                arrowPowerLevelGameObject[level[1] - 1].SetActive(false);
            }
            else if (level[1] == 2)
            {
                trainingLevelText[1].text = "LV:" + level[1].ToString();
                trainingCountText[1].text = "공격력\n 증가";
                trainingCostsText[1].text = "이 " + trainingMaxCounts[1][2] + "개 필요합니다.";

                arrowPowerLevelGameObject[level[1] - 1].SetActive(false);
            }
            else if (level[1] == 3)
            {
                trainingLevelText[1].text = "LV:" + level[1].ToString();
                trainingCountText[1].text = "공격력\n 증가";
                trainingCostsText[1].text = "이 " + trainingMaxCounts[1][3] + "개 필요합니다.";

                arrowPowerLevelGameObject[level[1] - 1].SetActive(false);
            }
            else if (level[1] == 4)
            {
                trainingLevelText[1].text = "LV:" + level[1].ToString();
                trainingCountText[1].text = "공격력\n 증가";
                trainingCostsText[1].text = "이 " + trainingMaxCounts[1][4] + "개 필요합니다.";

                arrowPowerLevelGameObject[level[1] - 1].SetActive(false);
            }
        }


        if (trainingCounts[2] == 0)
        {
            buttonBool[2] = true;
            if (level[2] == 1)
            {
                trainingLevelText[2].text = "LV:" + level[2].ToString();
                trainingCountText[2].text = "체력\n 증가";
                trainingCostsText[2].text = "이 " + trainingMaxCounts[2][1] + "개 필요합니다.";
                warriorPowerLevelGameObject[level[2] - 1].SetActive(false);
            }
            else if (level[2] == 2)
            {
                trainingLevelText[2].text = "LV:" + level[2].ToString();
                trainingCountText[2].text = "체력\n 증가";
                trainingCostsText[2].text = "이 " + trainingMaxCounts[2][2] + "개 필요합니다.";

                warriorPowerLevelGameObject[level[2] - 1].SetActive(false);
            }
            else if (level[2] == 3)
            {
                trainingLevelText[2].text = "LV:" + level[2].ToString();
                trainingCountText[2].text = "체력\n 증가";
                trainingCostsText[2].text = "이 " + trainingMaxCounts[2][3] + "개 필요합니다.";

                warriorPowerLevelGameObject[level[2] - 1].SetActive(false);
            }
            else if (level[2] == 4)
            {
                trainingLevelText[2].text = "LV:" + level[2].ToString();
                trainingCountText[2].text = "체력\n 증가";
                trainingCostsText[2].text = "이 " + trainingMaxCounts[2][4] + "개 필요합니다.";

                warriorPowerLevelGameObject[level[2] - 1].SetActive(false);
            }
        }


        if (trainingCounts[3] == 0)
        {
            buttonBool[3] = true;
            if (level[3] == 1)
            {
                trainingLevelText[3].text = "LV:" + level[3].ToString();
                trainingCountText[3].text = "공격력\n 증가";
                trainingCostsText[3].text = "이 " + trainingMaxCounts[3][1] + "개 필요합니다.";

                warriorHpLevelGameObjects[level[3] - 1].SetActive(false);
            }
            else if (level[3] == 2)
            {
                trainingLevelText[3].text = "LV:" + level[3].ToString();
                trainingCountText[3].text = "공격력\n 증가";
                trainingCostsText[3].text = "이 " + trainingMaxCounts[3][2] + "개 필요합니다.";

                warriorHpLevelGameObjects[level[3] - 1].SetActive(false);
            }
            else if (level[3] == 3)
            {
                trainingLevelText[3].text = "LV:" + level[3].ToString();
                trainingCountText[3].text = "공격력\n 증가";
                trainingCostsText[3].text = "이 " + trainingMaxCounts[3][3] + "개 필요합니다.";

                warriorHpLevelGameObjects[level[3] - 1].SetActive(false);
            }
            else if (level[3] == 4)
            {
                trainingLevelText[3].text = "LV:" + level[3].ToString();
                trainingCountText[3].text = "공격력\n 증가";
                trainingCostsText[3].text = "이 " + trainingMaxCounts[3][4] + "개 필요합니다.";

                warriorHpLevelGameObjects[level[3] - 1].SetActive(false);
            }
        }


    }

    void Level(int levelIndex)
    {

        for (int i = 0; i < 4; i++)
        {
            switch (level[levelIndex])
            {
                case 1:
                    trainingCounts[levelIndex] = trainingMaxCounts[levelIndex][0];
                    break;
                case 2:
                    trainingCounts[levelIndex] = trainingMaxCounts[levelIndex][1];
                    break;
                case 3:
                    trainingCounts[levelIndex] = trainingMaxCounts[levelIndex][2];
                    break;
                case 4:
                    trainingCounts[levelIndex] = trainingMaxCounts[levelIndex][3];
                    break;
            }
        }
    }

    public void lenTraining()
    {
        if (gameManager.moneyCount > trainingMaxCounts[0][level[0]] + 1 && buttonBool[0])
        {
            buttonBool[0] = false;
            trainingTypeUI[0] = TrainingType.Arrow_len;
            trainingCountText[0].text = trainingCounts[0].ToString();
            gameManager.moneyCount -= trainingMaxCounts[0][level[0]];
            UpdateTraing(0);
            Level(0);
        }
    }

    // 화살 공격력 트레이닝 시작
    public void PowerTraining()
    {
        if (gameManager.moneyCount > trainingMaxCounts[1][level[1]] + 1 && buttonBool[1])
        {
            buttonBool[1] = false;
            trainingTypeUI[1] = TrainingType.Arrow_DamageUP;
            trainingCountText[1].text = trainingCounts[1].ToString();
            gameManager.moneyCount -= trainingMaxCounts[1][level[1]];
            UpdateTraing(1);
            Level(1);
        }
    }

    // 전사 공격력 트레이닝 시작
    public void WarriorPowerTraining()
    {
        if (gameManager.moneyCount > trainingMaxCounts[2][level[2]] + 1 && buttonBool[2])
        {
            buttonBool[2] = false;
            trainingTypeUI[2] = TrainingType.Warrior_DamageUp;
            trainingCountText[2].text = trainingCounts[2].ToString();
            gameManager.moneyCount -= trainingMaxCounts[2][level[2]];
            UpdateTraing(2);
            Level(2);
        }
    }

    // 전사 스태미나 트레이닝 시작
    public void staminaTraining()
    {
        if (gameManager.moneyCount > trainingMaxCounts[3][level[3]] + 1 && buttonBool[3])
        {
            buttonBool[3] = false;
            trainingTypeUI[3] = TrainingType.Warrior_HpUp;
            trainingCountText[3].text = trainingCounts[3].ToString();
            gameManager.moneyCount -= trainingMaxCounts[3][level[3]];
            UpdateTraing(3);
            Level(3);
        }
    }

    //0:arrow_Len", "1:arrow_Power","2:warrior_Hp", "3:warrior_Damage"
    void UpdateTraing(int trainingTypeBools)
    {
        //궁수 사거리 훈련
        if (trainingTypeUI[trainingTypeBools] == TrainingType.Arrow_len)
        {
            for (int lenCount = 0; lenCount < 5; lenCount++)
            {
                if (lenCount == level[0])
                {
                    arrowLenLevelGameObjects[lenCount].SetActive(true);
                    level_Gauge_GameObject[0].enabled = true;
                }
                else
                {
                    arrowLenLevelGameObjects[lenCount].SetActive(false);
                }
            }
        }

        if (trainingTypeUI[trainingTypeBools] == TrainingType.Arrow_DamageUP)
        {
            for (int damageCount = 0; damageCount < 5; damageCount++)
            {
                if (damageCount == level[1])
                {
                    arrowPowerLevelGameObject[damageCount].SetActive(true);
                    level_Gauge_GameObject[1].enabled = true;
                }
                else
                {
                    arrowPowerLevelGameObject[damageCount].SetActive(false);
                }
            }
        }

        if (trainingTypeUI[trainingTypeBools] == TrainingType.Warrior_DamageUp)
        {
            for (int damageUpCount = 0; damageUpCount < 5; damageUpCount++)
            {
                if (damageUpCount == level[2])
                {
                    warriorPowerLevelGameObject[damageUpCount].SetActive(true);
                    level_Gauge_GameObject[2].enabled = true;
                }
                else
                {
                    warriorPowerLevelGameObject[damageUpCount].SetActive(false);
                }
            }
        }

        if (trainingTypeUI[trainingTypeBools] == TrainingType.Warrior_HpUp)
        {
            for (int hpUPCount = 0; hpUPCount < 5; hpUPCount++)
            {
                if (hpUPCount == level[3])
                {
                    warriorHpLevelGameObjects[hpUPCount].SetActive(true);
                    level_Gauge_GameObject[3].enabled = true;
                }
                else
                {
                    warriorHpLevelGameObjects[hpUPCount].SetActive(false);
                }
            }
        }
    }
}