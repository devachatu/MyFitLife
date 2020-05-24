using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Proyecto26;
using SimpleJSON;
using UnityEngine.SceneManagement;

// public class Node{
//     string dat;
//     unsafe Node* next;
//     Node(string x=""){
//             dat=x;
//             next=null;
//         }
// }
// public class LinkedList:Node{
//     unsafe Node *lstptr;
//     unsafe Node *temp;
//     LinkedList(){
//             lstptr=null;
//             temp=null;
//         }
//     void clear(){lstptr=null;
//     temp=null;}
//     unsafe void add(string x){
//         Node *newnode=new Node(x);
//             if(lstptr==null){
//                 lstptr->next=newnode;
//                 temp=temp->next;
//             }
//             else{
//                 temp->next=newnode;
//             }
//         }

// }
public class CPage : MonoBehaviour
{
    public static float TotalCalories;
    public Text Total;
    public InputField EnterFood;
    public  Text output;
    public  Text output2;
    public static string cal_name="";
    public static float cal_amount;
    string s = "a";
    private static string query = "";
    private static string appId = "6daca0f1";
    private static string appKey = "eea9c5337db8be3cb7f9caa5df9b9e36";
    //private readonly string baseURL="https://api.nutritionix.com/v1_1/search/"+query+"?&fields=item_name,brand_name,item_id,nf_calories&appId="+appId+"&appKey="+appKey;
    private string baseURL1 = "https://api.nutritionix.com/v1_1/search/";
    private string baseURL2 = "?&fields=item_name,brand_name,item_id,nf_calories&appId=" + appId + "&appKey=" + appKey;

    public Dropdown myDropdown; // Make sure to assign this
    public List<string> data;
    public JSONNode HitInfo;
    string theTime; 
    // Start is called before the first frame update

    public void OnButtonGetCalories() {
        Debug.Log(EnterFood.text);
        query = EnterFood.text;

        StartCoroutine(GetCalories());
    }

    public void OnButtonAdd()
    {
        Debug.Log(float.Parse(output.text));
        float temp = float.Parse(output.text);
        Debug.Log(temp);
        TotalCalories += temp;
        Total.text = "Total: " + TotalCalories.ToString();
        PlayerPrefs.SetFloat("userCal", TotalCalories);
        PostToDatabase();
    }


    public void OnButtonBack() {
        SceneManager.LoadScene("HPage");
    }
    void Start()
    {

        myDropdown.GetComponent<Image>().enabled = false;
        myDropdown.enabled = false;
        TotalCalories = PlayerPrefs.GetFloat("userCal");
        Total.text = "Total: " + TotalCalories.ToString();
        output.enabled = false;
        output2.enabled = false;
    }
    IEnumerator GetCalories() {
        Debug.Log(baseURL1 + query + baseURL2);
        UnityWebRequest CalorieInfoRequest = UnityWebRequest.Get(baseURL1 + query + baseURL2);
        yield return CalorieInfoRequest.SendWebRequest();
        JSONNode FullInfo = JSON.Parse(CalorieInfoRequest.downloadHandler.text);
        HitInfo = FullInfo["hits"];
        data.Clear();
        Debug.Log(HitInfo.Count);
        for (int i = 0; i < HitInfo.Count; i++) {
            //Debug.Log(HitInfo[i]["fields"]["item_name"]);
            if(Exist(HitInfo[i]["fields"]["item_name"],data)==false){
                data.Add(HitInfo[i]["fields"]["item_name"]);
            }
            else{
                data.Add(HitInfo[i]["fields"]["item_name"]+","+HitInfo[i]["fields"]["brand_name"]);
            }
            
        }
        myDropdown.options.Clear();
        //myDropdown.captionText.name = data[0];
        foreach (string str in data)
        {
            myDropdown.options.Add(new Dropdown.OptionData(str));
        }
        showElements();
    }
    bool Exist(string s,List<string> data){
        foreach (string item in data){
            if(string.Compare(item,s)==0){
                return true;
            }
        }
        return false;
    }
    public void OnValueChangedDropdown(int ch) {
        string amount = HitInfo[ch]["fields"]["item_name"];
        //Debug.Log(amount);
        string amount1 = HitInfo[ch]["fields"]["nf_calories"];
        //Debug.Log(amount1);
        output.text = amount1;
        output2.text = amount;
        cal_name=amount;
        cal_amount=float.Parse(amount1);
    }
    //shows all the UI elements of the scene
    public void showElements() {
        myDropdown.GetComponent<Image>().enabled = true;
        myDropdown.enabled = true;
        output.enabled = true;
        output2.enabled = true;
    }

    public void showTotal()
    {
        Total.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        OnValueChangedDropdown(myDropdown.value);
    }

    public void PostToDatabase(){
        User user = new User();
        Debug.Log("just wrote");
        theTime= System.DateTime.Now.ToString("hh:mm");
        Debug.Log("https://myfitlife-874f2.firebaseio.com/"+Lpage.username.Substring(0,Lpage.username.Length-10)+"/"+theTime+".json");
        RestClient.Put("https://myfitlife-874f2.firebaseio.com/"+Lpage.username.Substring(0,Lpage.username.Length-10)+"/"+theTime+".json",user);
        //Lpage.username
    }
}