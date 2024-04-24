using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google;
using Firebase.Auth;

public class bu : MonoBehaviour
{
    // El ID de cliente de tu servidor, que puedes obtener en la consola de la API de Google
    public const string webClientId = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com";

    // El objeto GoogleSignInOptions que te permite solicitar el token de ID de Google
    private GoogleSignInConfiguration configuration;

    // El botón Text Mesh Pro que llama al método SignIn cuando el usuario lo presiona
    public UnityEngine.UI.Button button;

    // El método Start se ejecuta cuando el juego empieza
    void Start()
    {
        
        // Configurar el objeto GoogleSignInOptions con el ID de cliente de tu servidor
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestIdToken = true };

        // Asignarle un texto y un color al botón usando la propiedad text
       // button.GetComponent<TMPro.TextMeshProUGUI>().text = "Acceder con Google";
       // button.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;

        // Agregar un evento al botón para que llame al método SignIn cuando el usuario lo presiona
        button.onClick.AddListener(SignIn);
    }

    // El método Update se ejecuta cada vez que se actualiza la escena
    void Update()
    {
        // No hay nada que hacer aquí por ahora
    }

    // El método SignIn inicia el proceso de acceso con Google y devuelve un objeto GoogleSignInUser si el usuario se autentica correctamente
    public void SignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignIn was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignIn encountered an error: " + task.Exception);
                return;
            }

            // Obtener el objeto GoogleSignInUser del resultado de la tarea
            GoogleSignInUser user = task.Result;
            Debug.Log("User signed in successfully: " + user.DisplayName + " (" + user.Email + ")");

            // Obtener el token de ID del usuario
            string idToken = user.IdToken;
            Debug.Log("User's ID token: " + idToken);

            // Crear una credencial de Firebase con el token de ID
            Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(idToken, null);
            Debug.Log("Firebase credential created.");

            // Iniciar sesión con Firebase con la credencial
            FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
            {
                if (authTask.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (authTask.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + authTask.Exception);
                    return;
                }

                // Obtener el objeto FirebaseUser del resultado de la tarea
                FirebaseUser firebaseUser = authTask.Result;
                Debug.Log("User signed in with Firebase successfully: " + firebaseUser.DisplayName + " (" + firebaseUser.Email + ")");
            });
        });
    }
}
