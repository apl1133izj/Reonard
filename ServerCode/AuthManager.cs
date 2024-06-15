using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public GameObject loginfalsetexGameObject;
    public Button signInButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser User;

    public void Start()
    {
        
        signInButton.interactable = false;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;
            if(result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                IsFirebaseReady = true;

                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }
            signInButton.interactable = IsFirebaseReady;
        });
    }

    public void SignIn()
    {
       if(!IsFirebaseReady || IsSignInOnProgress || User != null) 
        {
            return;
        }
        IsSignInOnProgress = true;
        signInButton.interactable = false;

        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text,passwordField.text).ContinueWithOnMainThread(
            task =>
            {
               
                Debug.Log(emailField.text); 
                Debug.Log(passwordField.text);
                Debug.Log($"Sings in status : {task.Status}");

                
                IsSignInOnProgress = false;
                signInButton.interactable = true;

                if(task.IsFaulted) 
                {
                    loginfalsetexGameObject.SetActive(true);
                    Debug.Log("로그인 실패");
                    //Debug.LogError(task.Exception);  
                }else if (task.IsCanceled)
                {
                    Debug.LogError("Sign-in canceled");
                }
                else
                {
                    User = task.Result.User;
                    Debug.Log(User.Email);
                    SceneManager.LoadScene("Lobby");
                }
            }
            );
    }

public void loginfalse()
    {
        loginfalsetexGameObject.SetActive(false);
    }
public void mainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }
        
   }