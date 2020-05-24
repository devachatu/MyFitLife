using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    // Start is called before the first frame update
    public string FoodName;
    public float TotalCal;
    public float calorie;

    public User(){
        TotalCal=CPage.TotalCalories;
        FoodName=CPage.cal_name;
        calorie=CPage.cal_amount;
    }
}
