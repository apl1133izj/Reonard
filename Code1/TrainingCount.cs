using UnityEngine;

public class TrainingCount : MonoBehaviour
{
    public Training training;
    public void TrainingLenCounts()
    {
        if (training.trainingCounts[0] != 0)
        {
            training.trainingCounts[0] --;
            training.trainingCountText[0].text = training.trainingCounts[0].ToString();
        }
    }
    public void TrainingPowerCounts()
    {
        if (training.trainingCounts[1] != 0)
        {
            training.trainingCountText[1].text = training.trainingCounts[1].ToString();
            training.trainingCounts[1] --;
        }
    }
    public void TrainingWarriorPowerCounts()
    {
        if (training.trainingCounts[2] != 0)
        {
            training.trainingCountText[2].text = training.trainingCounts[2].ToString();
            training.trainingCounts[2]--;
        }
    }
    public void TrainingStaminaCounts()
    {
        if (training.trainingCounts[3] != 0)
        {
            training.trainingCountText[3].text = training.trainingCounts[3].ToString();
            training.trainingCounts[3] --;
        }
    }
   

    public void TrainingLenGaugeCounts()
    {
        if (training.trainingCounts[0] == 0)
        {
           training.level[0] += 1;
            training.level_Gauge_GameObject[0].enabled = false;
        }
    }
    public void TrainingPowerGaugeCounts()
    {
        if (training.trainingCounts[1] == 0)
        {
            training.level[1] += 1;
            training.level_Gauge_GameObject[1].enabled = false;
        }
    }
    public void TrainingWarriorPowerGaugeCounts()
    {
        if (training.trainingCounts[2] == 0)
        {
            training.level[2] += 1;
            training.level_Gauge_GameObject[2].enabled = false;
        }
    }
    public void TrainingStaminaGaugeCounts()
    {
        if (training.trainingCounts[3] == 0)
        {
            training.level[3] += 1;
            training.level_Gauge_GameObject[3].enabled = false;
        }
    }
    
}
