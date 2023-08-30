using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing; //trace클래스 실행을 추적함. 코드의 실행을 추적하는데 도움이 되는 메서드와 속성 집합 제공
using Unity.VisualScripting; //동작을 디자인하는 워크플로 https://docs.unity3d.com/kr/2022.1/Manual/com.unity.visualscripting.html
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
//using UnityEngine.SceneManagement;

namespace Singleton
{
    public class Singleton<T>: MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    //한 오브젝트만 있을 수 있게


    public class GameManager : Singleton<GameManager>
    {

        private DateTime startTime;
        private DateTime endTime;

        //스켈레톤 코드
        void Start()
        {

            //플레이어 세이브 로드
            //세이브가 없으면 플레이어 등록 씬으로 리다이렉션

            startTime = DateTime.Now;
            Debug.Log("Game session start @: " + DateTime.Now);
        }
        void OnApplicationQuit()
        {
            endTime = DateTime.Now;
            TimeSpan timeDifference = endTime.Subtract(startTime);

            Debug.Log("Game session ended @: " + DateTime.Now);
            Debug.Log("Game session lasted: " + timeDifference);
        }

        //싱글턴 구현 확인
       /* void OnGUI()
        {
            if (GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }*/
       

    }
}