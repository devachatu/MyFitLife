using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;

public class CounterScreentest : MonoBehaviour
{
    public InputField EnterFood;
    public Text output;
    public Text output2;
    private static string query="";
    private static string appId="6daca0f1";
    private static string appKey="eea9c5337db8be3cb7f9caa5df9b9e36";
    //private readonly string baseURL="https://api.nutritionix.com/v1_1/search/"+query+"?&fields=item_name,brand_name,item_id,nf_calories&appId="+appId+"&appKey="+appKey;
    private string baseURL1="https://api.nutritionix.com/v1_1/search/";
    private string baseURL2="?&fields=item_name,brand_name,item_id,nf_calories&appId="+appId+"&appKey="+appKey;
    // Start is called before the first frame update
    
    public void OnButtonGetCalories(){
        Debug.Log(EnterFood.text);
        query=EnterFood.text;
    
        StartCoroutine(GetCalories());
    }
    void Start()
    {
        
    }
    IEnumerator GetCalories(){
        Debug.Log(baseURL1+query+baseURL2);
        UnityWebRequest CalorieInfoRequest= UnityWebRequest.Get(baseURL1+query+baseURL2);
        yield return CalorieInfoRequest.SendWebRequest();
        JSONNode FullInfo=JSON.Parse(CalorieInfoRequest.downloadHandler.text);
        JSONNode HitInfo=FullInfo["hits"];
        string amount=HitInfo[0]["fields"]["item_name"];
        Debug.Log(amount);
        string amount1=HitInfo[0]["fields"]["nf_calories"];
        Debug.Log(amount1);
        output.text=amount1;
        output2.text=amount;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
