using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sence1
{
    [System.Serializable]// thành phần quan trọng, dùng để đánh dấu một thành phần có thể tuần tự hóa
    //một thành phần được đánh dấu tuần tự hóa có nhiều tác dụng khác nhau
    //dùng để chuyển đổi cấu trúc dữ liệu chạy được trong chương trình thành các cấu trúc khác có thể lưu trữ hoặc gửi qua mạng hoặc ngược lại

    public class QuestionData
    {
        public QuestionData(string gen_question,
            string gen_answerA,
            string gen_answerB,
            string gen_answerC,
            string gen_answerD,
            string gen_correctAnswer
        )
        {
            question = gen_question;
            answerA = gen_answerA;
            answerB = gen_answerB;
            answerC = gen_answerC;
            answerD = gen_answerD;
            correctAnswer = gen_correctAnswer;
        }
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

        //[SerializeField]
        //    private QuestionData[] question_data = {
        //        new QuestionData(
        //        "Thủ đô của Nhật Bản là gì?",
        //        "A. Tokyo",
        //        "B. Bắc Kinh",
        //        "C. Seoul",
        //        "D. Bangkok",
        //        "A"
        //    ),
        //        new QuestionData(
        //        "Thực vật phát ra khí gì trong quá trình quang hợp?",
        //        "A. Oxy",
        //        "B. Carbon Dioxide",
        //        "C. Nitơ",
        //        "D. Hydrogen",
        //        "A"
        //    ),
        //        new QuestionData(
        //        "Hành tinh nào được gọi là 'Hành tinh Đỏ'?",
        //        "A. Sao Thổ",
        //        "B. Sao Hoả",
        //        "C. Sao Kim",
        //        "D. Sao Mộc",
        //        "B"
        //    ),
        //        new QuestionData(
        //        "Ngôn ngữ nào được cho là ngôn ngữ của máy tính?",
        //        "A. Tiếng Anh",
        //        "B. Tiếng Máy",
        //        "C. Tiếng Lập Trình",
        //        "D. Tiếng Máy Số",
        //        "D"
        //    ),
        //        new QuestionData(
        //        "Ai là tác giả của tác phẩm 'Sự kiện tại làng'?",
        //        "A. Nguyễn Du",
        //        "B. Nam Cao",
        //        "C. Hồ Chí Minh",
        //        "D. Ngô Tất Tố",
        //        "B"
        //    ),
        //        new QuestionData(
        //        "Kích thước nào sau đây là lớn nhất?",
        //        "A. 1 KB",
        //        "B. 1 MB",
        //        "C. 1 GB",
        //        "D. 1 TB",
        //        "D"
        //    ),
        //        new QuestionData(
        //        "Ngày nào trong năm được kỷ niệm là Ngày Quốc khánh Việt Nam?",
        //        "A. 1/5",
        //        "B. 2/9",
        //        "C. 30/4",
        //        "D. 20/11",
        //        "B"
        //    ),
        //        new QuestionData(
        //        "Cụm từ 'Tự do - Bình đẳng - Sáng tạo' thuộc tên gọi nước nào?",
        //        "A. Hoa Kỳ",
        //        "B. Anh",
        //        "C. Pháp",
        //        "D. Đức",
        //        "A"
        //    ),
        //        new QuestionData(
        //        "Ngôn ngữ lập trình nào phổ biến dùng để phát triển ứng dụng di động?",
        //        "A. Java",
        //        "B. C++",
        //        "C. Python",
        //        "D. Swift",
        //        "D"
        //    ),
        //        new QuestionData(
        //        "Thành phố nào được gọi là 'Thành phố cảng Hòn Ngọc'?",
        //        "A. Vũng Tàu",
        //        "B. Nha Trang",
        //        "C. Đà Nẵng",
        //        "D. Phú Quốc",
        //        "D"
        //    )
        //};
        List<QuestionData> question_data = new List<QuestionData> { };

        [System.Serializable]
        public class Question_List_Raw
        {
            public List<QuestionData> data;
        }
        
        private GameState game_state;
        private int question_index;
        private int live;
        // Start is called before the first frame update
        void Start()
        {
            
            StartCoroutine(GetQuestions("http://localhost:3000/getAll"));
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator GetQuestions(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;


                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:

                        //List<QuestionData> data = new List<QuestionData> {
                        //    new QuestionData(
                        //        "Thành phố nào được gọi là 'Thành phố cảng Hòn Ngọc'?",
                        //        "A. Vũng Tàu",
                        //        "B. Nha Trang",
                        //        "C. Đà Nẵng",
                        //        "D. Phú Quốc",
                        //        "D"
                        //    ),
                        //    new QuestionData(
                        //        "Ngôn ngữ lập trình nào phổ biến dùng để phát triển ứng dụng di động?",
                        //        "A. Java",
                        //        "B. C++",
                        //        "C. Python",
                        //        "D. Swift",
                        //        "D"
                        //    )
                        //};

                        //Question_List_Raw raw = new Question_List_Raw();
                        //raw.data = data;
                        //Debug.Log( raw.data.Count);

                        //var test_json = JsonUtility.ToJson(test_data);
                        ////Debug.Log(test_json);
                        //var test = JsonUtility.FromJson<QuestionData>(test_json);
                        //Debug.Log(JsonUtility.ToJson(test));
                        
                        var raw_data = webRequest.downloadHandler.text;
                        var data = JsonUtility.FromJson<Question_List_Raw>(raw_data);
                        foreach (var item in data.data)
                        {
                            question_data.Add(item);
                        }
                        Debug.Log(question_data.ToString());
                        Set_game_state(GameState.Home);
                        break;
                }
            }
        }

        IEnumerator ExampleCoroutine(bool traloiDung)
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSecondsRealtime(0.5f);
            img_answerA.color = Color.white;
            if (traloiDung)
            {
                if (question_index >= question_data.Count - 1)
                {
                    Debug.Log("Xin chuc mung, ban da hoan thanh tat ca cac cau hoi");
                    Set_game_state(GameState.GameOver);
                }
                question_index++;
                InitQuestion(question_index);
            }
        }

        public void OnPress(string select_answer)
        {
            bool traloiDung = false;
            if (question_data[question_index].correctAnswer == select_answer)
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

            if (live == 0)
            {
                Set_game_state(GameState.GameOver);
            }

            switch (select_answer)
            {
                case "A":
                    img_answerA.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "B":
                    img_answerB.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "C":
                    img_answerC.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
                case "D":
                    img_answerD.color = traloiDung ? Color.green : Color.red;
                    StartCoroutine(ExampleCoroutine(traloiDung));
                    break;
            }
        }

        private void InitQuestion(int index)
        {
            if (index >= question_data.Count || index < 0)
            {
                return;
            }
            question.text = question_data[index].question;
            answerA.text = question_data[index].answerA;
            answerB.text = question_data[index].answerB;
            answerC.text = question_data[index].answerC;
            answerD.text = question_data[index].answerD;
            img_answerA.color = Color.white;
            img_answerB.color = Color.white;
            img_answerC.color = Color.white;
            img_answerD.color = Color.white;
        }

        public void Set_game_state(GameState state)
        {
            game_state = state;
            home_panel.SetActive(game_state == GameState.Home);
            game_play_panel.SetActive(game_state == GameState.GamePlay);
            game_over_panel.SetActive(game_state == GameState.GameOver);
        }

        public void BtnPlay_Pressed()
        {
            Set_game_state(GameState.GamePlay);
            live = 20;
            question_index = 0;
            InitQuestion(question_index);
        }

        public void BtnHome_Presses()
        {
            Set_game_state(GameState.Home);
        }
    }
}