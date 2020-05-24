using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using SimpleJSON;
public class DPPage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text daily;
    public Text Diet;

    DateTime x=new DateTime(2020,4,4,12,0,0);
    
    User user=new User();
    public string theTime;
    public void OnButtonBack() {
        SceneManager.LoadScene("HPage");
    }
    void Start()
    {
        RetrieveFromDatabase();  
        StartCoroutine(RetrieveFromDatabase1());
    }

    public void RetrieveFromDatabase(){
        for(int i=0;i<60;i++){
            theTime= x.AddMinutes(-i).ToString("hh:mm");
            Debug.Log(theTime);
            RestClient.Get<User>("https://myfitlife-874f2.firebaseio.com/"+Lpage.username.Substring(0,Lpage.username.Length-10)+"/"+theTime+".json").Then(response =>
            {
                user=response;
                UpdateText();
            });
        }
}
public void UpdateText(){
    daily.text="You Eat "+ user.TotalCal +" Calories on average";
}
IEnumerator RetrieveFromDatabase1(){
    UnityWebRequest InfoRequest = UnityWebRequest.Get("https://myfitlife-874f2.firebaseio.com/chaitanya/.json");
        yield return InfoRequest.SendWebRequest();
        JSONNode FullInfo = JSON.Parse(InfoRequest.downloadHandler.text);
        Debug.Log(FullInfo[0]);
        for (int i = 0; i < FullInfo.Count; i++) {
            Diet.text=Diet.text+"\n"+FullInfo[i]["FoodName"];            
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
//