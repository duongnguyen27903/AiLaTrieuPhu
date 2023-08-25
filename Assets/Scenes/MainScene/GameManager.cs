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
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string correctAnswer;
    }
    public class GameManager : MonoBehaviour
    {
        [ SerializeField] private TextMeshProUGUI question;
        [SerializeField] private TextMeshProUGUI answer1;
        [SerializeField] private TextMeshProUGUI answer2;
        [SerializeField] private TextMeshProUGUI answer3;
        [SerializeField] private TextMeshProUGUI answer4;
        [SerializeField] private Image img_answer1;
        [SerializeField] private Image img_answer2;
        [SerializeField] private Image img_answer3;
        [SerializeField] private Image img_answer4;
        [SerializeField] private QuestionData question_data;
      
        // Start is called before the first frame update
        void Start()
        {
            question.text = question_data.question;
            answer1.text = "A: " + question_data.answer1;
            answer2.text = "B: " + question_data.answer2;
            answer3.text = "C: " + question_data.answer3;
            answer4.text = "D: " + question_data.answer4;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPress( string select_answer)
        {
            bool traloiDung = false;
            if( question_data.correctAnswer == select_answer)
            {
                traloiDung = true;
                Debug.Log("Ban da tra loi chinh xac");
            }
            else
            {
                traloiDung = false;
                Debug.Log("Ban da tra loi sai");
            }

            switch ( select_answer ) 
            {
                case "a":
                    img_answer1.color = traloiDung ? Color.green : Color.red;
                    break;
                case "b":
                    img_answer2.color = traloiDung ? Color.green: Color.red;
                    break;
                case "c":
                    img_answer3.color = traloiDung ? Color.green:Color.red;
                    break;
                case "d":
                    img_answer4.color = traloiDung ? Color.green : Color.red;
                    break;

            }
        }
    }

}