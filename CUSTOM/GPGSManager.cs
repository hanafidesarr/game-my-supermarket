// using UnityEngine;
// using System;
// using System.Text;

// #if UNITY_ANDROID
// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
// using GooglePlayGames.BasicApi.SavedGame;
// using UnityEngine.SocialPlatforms;
// #endif

// /// <summary>
// /// GPGSManager: Central manager for Google Play Games Services
// /// Handles login, cloud save/load, and PlayerPrefs migration
// /// </summary>
// public class GPGSManager : MonoBehaviour
// {
//     public static GPGSManager Instance;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
// #if UNITY_ANDROID
//             InitializeGPGS();
// #endif
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

// #if UNITY_ANDROID
//     #region Initialization
//     private void InitializeGPGS()
//     {
//         // Versi baru: cukup activate
//         PlayGamesPlatform.Activate();
//         SignIn();
//     }

//     private void SignIn()
//     {
//         Social.localUser.Authenticate((bool success) =>
//         {
//             if (success)
//                 Debug.Log("Google Play Games login successful!");
//             else
//                 Debug.LogWarning("Google Play Games login failed!");
//         });
//     }
//     #endregion

//     #region Cloud Save/Load
//     public void SaveStringToCloud(string key, string value)
//     {
//         ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
//         byte[] data = Encoding.UTF8.GetBytes(value);

//         savedGameClient.OpenWithAutomaticConflictResolution(
//             key,
//             DataSource.ReadCacheOrNetwork,
//             ConflictResolutionStrategy.UseLongestPlaytime,
//             (status, game) =>
//             {
//                 if (status == SavedGameRequestStatus.Success)
//                 {
//                     savedGameClient.CommitUpdate(game, new SavedGameMetadataUpdate.Builder().Build(), data, (s, g) =>
//                     {
//                         if (s == SavedGameRequestStatus.Success)
//                             Debug.Log($"Saved '{key}' to cloud!");
//                         else
//                             Debug.LogWarning($"Failed saving '{key}' to cloud!");
//                     });
//                 }
//             });
//     }

//     public void LoadStringFromCloud(string key, Action<string> callback)
//     {
//         ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

//         savedGameClient.OpenWithAutomaticConflictResolution(
//             key,
//             DataSource.ReadCacheOrNetwork,
//             ConflictResolutionStrategy.UseLongestPlaytime,
//             (status, game) =>
//             {
//                 if (status == SavedGameRequestStatus.Success)
//                 {
//                     savedGameClient.ReadBinaryData(game, (s, data) =>
//                     {
//                         if (s == SavedGameRequestStatus.Success && data.Length > 0)
//                         {
//                             string value = Encoding.UTF8.GetString(data);
//                             callback?.Invoke(value);
//                         }
//                         else
//                         {
//                             callback?.Invoke(null);
//                         }
//                     });
//                 }
//                 else
//                 {
//                     callback?.Invoke(null);
//                 }
//             });
//     }

//     // Helper overloads for int, float, bool
//     public void SaveIntToCloud(string key, int value) => SaveStringToCloud(key, value.ToString());
//     public void SaveFloatToCloud(string key, float value) => SaveStringToCloud(key, value.ToString("R"));
//     public void SaveBoolToCloud(string key, bool value) => SaveStringToCloud(key, value ? "1" : "0");

//     public void LoadIntFromCloud(string key, int defaultValue, Action<int> callback)
//     {
//         LoadStringFromCloud(key, str =>
//         {
//             if (int.TryParse(str, out int result))
//                 callback?.Invoke(result);
//             else
//                 callback?.Invoke(defaultValue);
//         });
//     }

//     public void LoadFloatFromCloud(string key, float defaultValue, Action<float> callback)
//     {
//         LoadStringFromCloud(key, str =>
//         {
//             if (float.TryParse(str, out float result))
//                 callback?.Invoke(result);
//             else
//                 callback?.Invoke(defaultValue);
//         });
//     }

//     public void LoadBoolFromCloud(string key, bool defaultValue, Action<bool> callback)
//     {
//         LoadStringFromCloud(key, str =>
//         {
//             if (str == "1") callback?.Invoke(true);
//             else if (str == "0") callback?.Invoke(false);
//             else callback?.Invoke(defaultValue);
//         });
//     }
//     #endregion
// #endif

//     #region PlayerPrefs Migration
//     public void MigrateKey(string key, int defaultValue = 0)
//     {
// #if UNITY_ANDROID
//         int localInt = PlayerPrefs.GetInt(key, defaultValue);
//         SaveIntToCloud(key, localInt);
// #endif
//     }

//     public void MigrateKey(string key, float defaultValue = 0f)
//     {
// #if UNITY_ANDROID
//         float localFloat = PlayerPrefs.GetFloat(key, defaultValue);
//         SaveFloatToCloud(key, localFloat);
// #endif
//     }

//     public void MigrateKey(string key, string defaultValue = "")
//     {
// #if UNITY_ANDROID
//         string localString = PlayerPrefs.GetString(key, defaultValue);
//         SaveStringToCloud(key, localString);
// #endif
//     }

//     public void MigrateKey(string key, bool defaultValue)
//     {
// #if UNITY_ANDROID
//         bool localBool = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
//         SaveBoolToCloud(key, localBool);
// #endif
//     }

//     public void MigrateAllPlayerPrefsToCloud()
//     {
// #if UNITY_ANDROID
//         MigrateKey("Money", 1000);
//         MigrateKey("Experience", 1);
//         MigrateKey("ProfileName", "");
//         MigrateKey("Health", 100f);
//         Debug.Log("All local PlayerPrefs migrated to cloud.");
// #endif
//     }
//     #endregion
// }
