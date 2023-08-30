using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sence1
{
    [Serializable]
    public class QuestionData
    {
        public string question;
        public string answerA;
        public string answerB;
        public string answerC;
        public string answerD;
        public string correctAnswer;
    }

    public enum GameState
    {
        Home,
        GamePlay,
        GameOver
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private TextMeshProUGUI answerA;
        [SerializeField] private TextMeshProUGUI answerB;
        [SerializeField] private TextMeshProUGUI answerC;
        [SerializeField] private TextMeshProUGUI answerD;
        [SerializeField] private Image img_answerA;
        [SerializeField] private Image img_answerB;
        [SerializeField] private Image img_answerC;
        [SerializeField] private Image img_answerD;

        [SerializeField] private GameObject home_panel, game_play_panel, game_over_panel;

        [SerializeField] private QuestionData[] question_data;

        private GameState game_state;
        private int question_index;
        private int live;
        // Start is called before the first frame update
        void Start()
        {
            Set_game_state(GameState.Home);
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator ExampleCoroutine(bool traloiDung)
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSecondsRealtime(5);
            img_answerA.color = Color.white;
            if (traloiDung)
            {
                if (question_index >= question_data.Length - 1)
                {
                    Debug.Log("Xin chuc mung, ban da hoan thanh tat ca cac cau hoi");
                    Set_game_state(GameState.GameOver);
                }
                question_index++;
                InitQuestion(question_index);
            }

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }

        public void OnPress( string select_answer)
        {
            bool traloiDung = false;
            if(question_data[question_index].correctAnswer == select_answer)
            {
                traloiDung = true;
                Debug.Log("Ban da tra loi chinh xac");
            }
            else
            {
                traloiDung = false;
                live--;
                Debug.Log("Ban da tra loi sai");
            }

            if( live == 0)
            {
                Set_game_state(GameState.GameOver);
            }

            switch ( select_answer ) 
            {
                case "a":
                    img_answerA.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "b":
                    img_answerB.color = traloiDung ? Color.green: Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "c":
                    img_answerC.color = traloiDung ? Color.green:Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "d":
                    img_answerD.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
            }
        }

        private void InitQuestion( int index)
        {
            if( index>=question_data.Length || index<0)
            {
                return;
            }
            question.text = question_data[index].question;
            answerA.text = "A: " + question_data[index].answerA;
            answerB.text = "B: " + question_data[index].answerB;
            answerC.text = "C: " + question_data[index].answerC;
            answerD.text = "D: " + question_data[index].answerD;
            img_answerA.color = Color.white;
            img_answerB.color = Color.white;
            img_answerC.color = Color.white;
            img_answerD.color = Color.white;
        }

        public void Set_game_state( GameState state)
        {
            game_state = state;
            home_panel.SetActive(game_state == GameState.Home);
            game_play_panel.SetActive(game_state == GameState.GamePlay);
            game_over_panel.SetActive( game_state == GameState.GameOver);
        }

        public void BtnPlay_Pressed ()
        {
            Set_game_state(GameState.GamePlay);
            live = 3;
            question_index = 0;
            InitQuestion(question_index);
        }

        public void BtnHome_Presses()
        {
            Set_game_state(GameState.Home);
        }
    }
}