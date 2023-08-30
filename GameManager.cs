using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing; //traceŬ���� ������ ������. �ڵ��� ������ �����ϴµ� ������ �Ǵ� �޼���� �Ӽ� ���� ����
using Unity.VisualScripting; //������ �������ϴ� ��ũ�÷� https://docs.unity3d.com/kr/2022.1/Manual/com.unity.visualscripting.html
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
    //�� ������Ʈ�� ���� �� �ְ�


    public class GameManager : Singleton<GameManager>
    {

        private DateTime startTime;
        private DateTime endTime;

        //���̷��� �ڵ�
        void Start()
        {

            //�÷��̾� ���̺� �ε�
            //���̺갡 ������ �÷��̾� ��� ������ �����̷���

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

        //�̱��� ���� Ȯ��
       /* void OnGUI()
        {
            if (GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }*/
       

    }
}