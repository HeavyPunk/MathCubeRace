using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace RandomTaskGenerator
{
    public class TaskShower : MonoBehaviour
    {
        struct AnswerBlock
        {
            public string Name { set; get; }
            public Text AnswerVariant { set; get; }
        }

        public float _distanceToNextTask = 30;
        public int playerLivesCount;
        public int adsCooldownInSecs;
        public GameObject firstLosePanel;
        public GameObject adsTimer;
        public GameObject scoreText;
        public GameObject scoreAmount;
        public GameObject adsDoubleScoreImage;
        public GameObject doubleScoreText;

        private Generator _generator;
        
        private Text _leftAnswerText;
        private Text _centerAnswerText;
        private Text _rightAnswerText;
        private GameObject _answerContainer;
        private GameObject _taskPanel;
        private Image _adsTimerAmount;
        private Text _taskText;
        private Text _playerLivesCountText;
        private bool _swipeMode;
        private bool isAdsCooldown;
        private float _fillAmount = 0;
        private int _correctBlockNum;
        private int _score = 0;
        private bool _toGetNewTask = true;
        private readonly ConcurrentQueue<(string, string)> _taskQueue = new ConcurrentQueue<(string, string)>(); 
        private readonly AnswerBlock[] _answerBlocks =
        {
            new AnswerBlock
            {
                Name = "LeftAnswerBlock"
            },
            new AnswerBlock
            {
                Name = "CenterAnswerBlock"
            },
            new AnswerBlock
            {
                Name = "RightAnswerBlock"
            }
        };

        void Start()
        {
            var difficulty = new Difficulty
            {
                DifficultAnswer = true,
                LargeNumbers = true,
                LongTasks = true
            };
            _generator = new Generator(difficulty);

            FindAllObjects();
            
            isAdsCooldown = true;
            _adsTimerAmount.fillAmount = 1;
            
            PlayerPrefs.SetInt("RightAnswerCount", _score);

            var generatorTask = new Thread(TaskGeneratorThread) {IsBackground = true};
            generatorTask.Start();
        }

        void Update()
        {
            if (_toGetNewTask && _taskQueue.TryDequeue(out var task))
            {
                var mainRandomGenerator = new Random();
                _taskText.text = task.Item1;
                _playerLivesCountText.text = playerLivesCount.ToString();
                _correctBlockNum = new Random().Next(0, 3);

                _answerBlocks[_correctBlockNum].AnswerVariant.text = task.Item2;
                for (var i = 0; i < _answerBlocks.Length; i++)
                {
                    if (i == _correctBlockNum)
                        continue;
                    _answerBlocks[i].AnswerVariant.text =
                        GetRandomAnswerVariant(task.Item2, 30, mainRandomGenerator);
                }
                _toGetNewTask = false;
            }

            if (playerLivesCount == 0)
            {
                _playerLivesCountText.text = playerLivesCount.ToString();
                if (isAdsCooldown)
                {
                    _adsTimerAmount.fillAmount -= 1 / (adsCooldownInSecs - 0.5f) * Time.deltaTime;
                    if (_adsTimerAmount.fillAmount <= 0)
                    {
                        //Off FirstLoseMenu
                        GameObject.Find("LoseText").SetActive(false);
                        GameObject.Find("AdsCheckText").SetActive(false);
                        GameObject.Find("AdsImage").SetActive(false);
                        GameObject.Find("AdsTimer").SetActive(false);

                        // //On SecondLoseMenu
                        scoreText.SetActive(true);
                        scoreAmount.SetActive(true);
                        scoreAmount.GetComponent<Text>().text = PlayerPrefs.GetInt("LastScore").ToString();
                        adsDoubleScoreImage.SetActive(true);
                        doubleScoreText.SetActive(true);

                        isAdsCooldown = false;
                    }
                }
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            //На релизе убрать PlayerMovement, оставить ТОЛЬКО SwipeManagement
            Debug.Log("Collision object: " + other.gameObject.name + ":" +  _answerBlocks[_correctBlockNum].Name);
            if (!other.gameObject.name.Equals(_answerBlocks[_correctBlockNum].Name))
            {
                if (playerLivesCount == 1)
                {
                    AudioListener.pause = true;
                    //DebugCode
                    if (_swipeMode)
                        transform.GetComponent<SwipeManagement>().playerSpeed = 0;
                    else
                        transform.GetComponent<PlayerMovement>().playerSpeed = 0;
                    //DebugCode
                    _playerLivesCountText.text = playerLivesCount.ToString();
                    _taskPanel.SetActive(false);
                    firstLosePanel.SetActive(true);
                    playerLivesCount--;
                    //RestartGame();
                    return;
                }

                playerLivesCount--;
            }

            if (other.gameObject.name.Equals(_answerBlocks[_correctBlockNum].Name))
                _score += 1;
            PlayerPrefs.SetInt("RightAnswerCount", _score);
            _toGetNewTask = true;
            _answerContainer.transform.Translate(new Vector3(_distanceToNextTask, 0, 0));
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        #region AdditionalMethods

        private void FindAllObjects()
        {
            _taskPanel = GameObject.FindWithTag("TaskPanel");
            _taskText = _taskPanel.GetComponent<Text>();
            _adsTimerAmount = adsTimer.GetComponent<Image>();
            _playerLivesCountText = GameObject.Find("HeartCount").GetComponent<Text>();
            
            _answerBlocks[0].AnswerVariant = GameObject.FindWithTag("LeftAnswer").GetComponent<Text>();
            _answerBlocks[1].AnswerVariant = GameObject.FindWithTag("CenterAnswer").GetComponent<Text>();
            _answerBlocks[2].AnswerVariant = GameObject.FindWithTag("RightAnswer").GetComponent<Text>();

            _answerContainer = GameObject.FindWithTag("AnswerContainer");

            _swipeMode = PlayerPrefs.GetString("SwipeMode").Equals("On");
        }
        
        private static string GetRandomAnswerVariant(string @base, double defectPercent, Random randomGenerator)
        {
            if (0 > defectPercent || defectPercent > 100)
                throw new ArgumentException("Percent must be in [0, 100]");
            var correctAnswer = int.Parse(@base);
            var defect = randomGenerator.Next((int)(correctAnswer * defectPercent / 100), (int)(correctAnswer * (defectPercent + 100) / 100));
            return defect.ToString();
        }

        private void TaskGeneratorThread()
        {
            var numberGenerator = new Random();
            while (true)
            {
                if(_taskQueue.Count >= 10)
                    continue;
                var primer = _generator.GenerateNewTask(numberGenerator);
                _taskQueue.Enqueue(primer);
            }
        }
        #endregion
    }
}
