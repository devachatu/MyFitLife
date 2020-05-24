// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Firebase.Auth;

// public class Logintest : MonoBehaviour
// {
//     #region variables
//     //static variables
    
//     //private variables
//     private string CreateAccountUrl="";
//     private string LoginUrl="";
//     private FirebaseAuth auth;
//     //public variables
//     public static string Email="";
//     public static string Password="";
//     public string CurrentMenu="Login";
    
//     //GUI test
//     public float X;
//     public float Y;
//     public float Width;
//     public float Height;

//     #endregion 
//     // Start is called before the first frame update
//     void Start(){
//      auth = FirebaseAuth.DefaultInstance;
//     }
//     void OnGUI()
//     {
//         if (CurrentMenu == "Login")
//         {
//             LoginGUI();
//         }
//         else if (CurrentMenu == "CreateAccount")
//         {
//             CreateAccountGUI();
//         }
//     }
    
//     #region Custom Methods
//     void LoginGUI(){
//         GUI.Box(new Rect((Screen.width/2)- (Screen.width / 4)/2, (Screen.height/2) -160, Screen.width/4, 30), "Login");
//         if(GUI.Button(new Rect((Screen.width/10), (Screen.height/2)+100, (Screen.width / 10), (Screen.height/20)),"Create Account")){
//             CurrentMenu="CreateAccount";
//         }
//         if(GUI.Button(new Rect(2* (Screen.width / 10) +30, (Screen.height / 2) + 100, (Screen.width / 10), (Screen.height / 20)), "Log In")){
//             Login1(Email,Password);
//         }
//         GUI.Label(new Rect((Screen.width / 10), (Screen.height / 2) - 80, (Screen.width / 5), (Screen.height / 20)),"Email:");
//         Email=GUI.TextField(new Rect((Screen.width / 10), (Screen.height / 2) - 60, (Screen.width / 4), (Screen.height / 25)), Email);
//         GUI.Label(new Rect((Screen.width / 10), (Screen.height / 2), (Screen.width / 5), (Screen.height / 20)), "Password:");
//         Password=GUI.TextField(new Rect((Screen.width / 10), (Screen.height / 2) +20, (Screen.width / 4), (Screen.height / 25)), Password);
//     }

//     void CreateAccountGUI(){
//         GUI.Box(new Rect((Screen.width/2)- (Screen.width / 4)/2, (Screen.height/2) -160, Screen.width/4, 30), "Create Account");
//         if(GUI.Button(new Rect((Screen.width/10), (Screen.height/2)+100, (Screen.width / 10), (Screen.height/20)),"Create Account")){
//             Signup(Email,Password);
//         }
//         if(GUI.Button(new Rect(2* (Screen.width / 10) +30, (Screen.height / 2) + 100, (Screen.width / 10), (Screen.height / 20)), "Back")){
//             CurrentMenu = "Login";
//         }
//         GUI.Label(new Rect((Screen.width / 10), (Screen.height / 2) - 80, (Screen.width / 5), (Screen.height / 20)),"Email:");
//         Email=GUI.TextField(new Rect((Screen.width / 10), (Screen.height / 2) - 60, (Screen.width / 4), (Screen.height / 25)), Email);
//         GUI.Label(new Rect((Screen.width / 10), (Screen.height / 2), (Screen.width / 5), (Screen.height / 20)), "Password:");
//         Password=GUI.TextField(new Rect((Screen.width / 10), (Screen.height / 2) +20, (Screen.width / 4), (Screen.height / 25)), Password);

//     }

//      private void UpdateErrorMessage(string message)
//     {
//         //ErrorText.text = message;
//         //Invoke("ClearErrorMessage", 3);
//     }

//     void ClearErrorMessage()
//     {
//         //ErrorText.text = "";
//     }

//      public void Signup(string email, string password)
//     {
//         if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//         {
//             //Error handling
//             return;
//         }

//         auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
//         {
//             if (task.IsCanceled)
//             {
//                 Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
//                 return;
//             }
//             if (task.IsFaulted)
//             {
//                 Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
//                 if (task.Exception.InnerExceptions.Count > 0)
//                     UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
//                 return;
//             }

//             FirebaseUser newUser = task.Result; // Firebase user has been created.
//             Debug.LogFormat("Firebase user created successfully: {0} ({1})",
//                 newUser.DisplayName, newUser.UserId);
//             UpdateErrorMessage("Signup Success");
//         });
//     }

//     public void Login1(string email, string password)
//     {
//         auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
//         {
//             if (task.IsCanceled)
//             {
//                 Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
//                 return;
//             }
//             if (task.IsFaulted)
//             {
//                 Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
//                 if (task.Exception.InnerExceptions.Count > 0)
//                     UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
//                 return;
//             }

//             FirebaseUser user = task.Result;
//             Debug.LogFormat("User signed in successfully: {0} ({1})",
//                 user.DisplayName, user.UserId);

//             PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
//             //SceneManager.LoadScene("LoginResults");
//         });
//     }

//     #endregion
//     //end


// }
