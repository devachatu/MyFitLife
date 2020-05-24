using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class Lpage : MonoBehaviour
{
    private FirebaseAuth auth;
    bool authorization=false;
    public InputField Email;
    public InputField Password;
    public static string username;
    
    public void OnButtonLogin(){
        Debug.Log(Email.text);
        Debug.Log(Password.text);
        Login1(Email.text,Password.text);
    }
    public void OnButtonSignup(){
        SceneManager.LoadScene("SPage");
    }
            

    private void UpdateErrorMessage(string message)
    {
        //ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
        //ErrorText.text = "";
    }

    public void Login1(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",user.DisplayName, user.UserId);   
            //PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
            authorization=true;   
            username=email;
            
        });
                
        
    }
    void Update(){
        if(authorization==true){
            SceneManager.LoadScene("HPage");
            Debug.Log("hey");
        }
    }    
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        authorization=false;
        
    }
    // Update is called once per frame
}
