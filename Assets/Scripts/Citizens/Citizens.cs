using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class Citizens : GAgent
{
    private float maxWorkTime = 8f;
    private float startWorkingTime = 0f;
    private GameObject ChosenFood;
    private GameObject store;
    public bool checkWorkTime = false;
    public bool active = false;
    public static GetTime OnGetTime;
    public int money = 0;
    public Day[] workingDays;
    public TMP_Text moneyUI;

    new void Start()
    {
        base.Start();
        goals.Add(new SubGoal("foodBought", 1, false), 3);
        goals.Add(new SubGoal("stomachFull", 1, false), 3);
        goals.Add(new SubGoal("throwedTrash", 1, false), 3);
        SubGoal s1 = new SubGoal("isWorking", 1, false);
        goals.Add(s1, 3);
        goals.Add(new SubGoal("atHome", 1, false), 3);
        goals.Add(new SubGoal("rested", 1, false), 3);
        LightingManager.OnSevenOClock += ProgramDay;
        Invoke("Hungry", Random.Range(0, 5));
    }

    public void SetChosenFood(GameObject food) {
        ChosenFood = food;
    }

    public GameObject GetChosenFood() {
        return ChosenFood;
    }

    private void OnDestroy()
    {
       LightingManager.OnSevenOClock -= ProgramDay; 
    }

    public void SetStartingWorkingTime(float time) {
        startWorkingTime = time;
    }

    private void ProgramDay() {
        Day currentDay = GWorld.Instance.GetDay();
            if (workingDays.Contains(currentDay)) {
                if (beliefs.HasState("atWeekend")) {
                    beliefs.RemoveState("atWeekend");
                }
                beliefs.ModifyState("notWorked", 0);
                beliefs.ModifyState("atWorkingDay", 0);
            } else {
                if (beliefs.HasState("atWorkingDay")) {
                    beliefs.RemoveState("atWorkingDay");
                }
                beliefs.ModifyState("atWeekend", 0);
            }
    }

    private void WorkTime() {
        if (OnGetTime != null) {
            float currentTime = OnGetTime();
            if (startWorkingTime + maxWorkTime < currentTime) {
                beliefs.ModifyState("finishWork", 0);
            }
        }
    }

    private void Hungry() {
        beliefs.ModifyState("needEat", 0);
        Invoke("Hungry", Random.Range(20, 40));
    }

    private void Update() {
        if (checkWorkTime)
            WorkTime();
        moneyUI.text = money.ToString() + "$";
    }

    public int GetMoney() {
        return money;
    }

    public void SetMoney(int deltaMoney) {
        money += deltaMoney;
    }
    

}
