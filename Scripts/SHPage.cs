using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
public class SHPage : MonoBehaviour
{
    public Text hist;
    User user=new User();
    
    public string theTime;
    // Start is called before the first frame update
    public void OnButtonBack()
    {
        SceneManager.LoadScene("HPage");
    }
    void Start()
    {
        RetrieveFromDatabase();
    }

    public void RetrieveFromDatabase(){
    for(int i=0;i<1440;i++){
    theTime= System.DateTime.Now.AddMinutes(-i).ToString("hh:mm");
    Debug.Log(theTime);
    if(string.Compare(theTime,"12:00")==0) break;
    RestClient.Get<User>("https://myfitlife-874f2.firebaseio.com/"+Lpage.username.Substring(0,Lpage.username.Length-10)+"/"+theTime+".json").Then(response =>
    {
        user=response;
        UpdateFoodName();
    }
    );
    }
}

public void UpdateFoodName(){
    Debug.Log("\n"+user.FoodName+"-"+user.calorie.ToString());
    hist.text=hist.text+"\n"+user.FoodName+"-"+user.calorie.ToString();
}

}
