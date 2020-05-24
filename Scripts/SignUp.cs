using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;

public class SignUp : MonoBehaviour
{
    private FirebaseAuth auth;
    public InputField SEmail;
    public InputField SPassword;
    public InputField CSPassword;
    bool authorization=false;
    // Start is called before the first frame update

    public void OnButtonSignUp(){
        if(SPassword.text==CSPassword.text){
            Signup(SEmail.text,SPassword.text);
        }
        else{
            Debug.Log("Password Doesnt Match");
        }
    }
    public void OnButtonBack(){
        SceneManager.LoadScene("LPage");
    }
    
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
public void Signup(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            //Error handling
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser newUser = task.Result; // Firebase user has been created.
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
            UpdateErrorMessage("Signup Success");  
       authorization=true;
       if(authorization==true){
            SceneManager.LoadScene("HPage");
        }
        });
        
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
}
