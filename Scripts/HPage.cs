using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;
public class HPage : MonoBehaviour
{ 

    public Text Total;
    public Text FoodN;
    User user=new User();
    public string theTime;
    DateTime comb;
    public static float TotalCalories=0;

    // Start is called before the first frame update
    void Start()
    {
        
        TotalCalories = PlayerPrefs.GetFloat("userCal");
        if (PlayerPrefs.GetFloat("day") == 0 || (((float)System.DateTime.Now.Date.Month / 12) + ((float)System.DateTime.Now.Date.Day / 31) + (float)System.DateTime.Now.Year > (PlayerPrefs.GetFloat("day"))))
        {
            PlayerPrefs.SetFloat("day", ((float)System.DateTime.Now.Date.Month / 12) + ((float)System.DateTime.Now.Date.Day / 31) + (float)System.DateTime.Now.Year);
            PlayerPrefs.SetFloat("userCal", 0);
            TotalCalories = 0;
        }
        Total.text = "Today's Calories: " + TotalCalories.ToString();
        RetrieveFromDatabase(); 
    }
    public void OnButtonEnter(){
        SceneManager.LoadScene("CalPage");
    }
    public void OnButtonSignout(){
    SceneManager.LoadScene("LPage");
}
public void OnButtonQuickAdd(){
    TotalCalories+=user.calorie;
    Total.text = "Today's Calories: " + TotalCalories.ToString();
    PlayerPrefs.SetFloat("userCal", TotalCalories);
}

public void OnButtonShowHistory(){
    SceneManager.LoadScene("SHPage");
}

public void OnButtonDietPlanner(){
    SceneManager.LoadScene("DPPage");
}
public void RetrieveFromDatabase(){
    for(int i=0;i<60;i++){
    theTime= System.DateTime.Now.AddMinutes(-i).ToString("hh:mm");
    Debug.Log(theTime);
    RestClient.Get<User>("https://myfitlife-874f2.firebaseio.com/"+Lpage.username.Substring(0,Lpage.username.Length-10)+"/"+theTime+".json").Then(response =>
    {
        user=response;
        UpdateFoodName();
    }
    );
    }
}
public void UpdateFoodName(){
    FoodN.text=user.FoodName;
    //TotalCalories=user.TotalCal;
}

    // Update is called once per frame
    void Update(){
        Total.text = "Today's Calories: " + TotalCalories.ToString();
    }
}
