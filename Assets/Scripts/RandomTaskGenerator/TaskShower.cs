using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace RandomTaskGenerator
{
    public class TaskShower : MonoBehaviour
    {
        private struct AnswerBlock
        {
            public Text AnswerVariant { set; get; }
        }

        public float distanceToNextTask = 30;
        public int playerLivesCount;
        public int adsCooldownInSecs;
        public GameObject firstLosePanel;
        public GameObject adsTimer;
        public GameObject scoreText;
        public GameObject scoreAmount;
        public GameObject adsDoubleScoreImage;
        public GameObject doubleScoreText;
        public char[] actions = new char[4];
        private Generator _generator;
        
        private Text _leftAnswerText;
        private Text _centerAnswerText;
        private Text _rightAnswerText;
        private GameObject _answerContainer;
        private string _rightAnswer;
        private GameObject _taskPanel;
        private Image _adsTimerAmount;
        private Text _taskText;
        private Text _playerLivesCountText;
        private bool _swipeMode;
        private bool _isAdsCooldown;
        private int _score;
        private bool _toGetNewTask = true;

        private Difficulty[] _standardDifficulties = 
        {
            new Difficulty {DifficultAnswer = false, LargeNumbers = false, LongTasks = false},
            new Difficulty {DifficultAnswer = true, LargeNumbers = false, LongTasks = true},
            new Difficulty {DifficultAnswer = true, LargeNumbers = true, LongTasks = true}
        };
        private readonly ConcurrentQueue<(string, string)> _taskQueue = new ConcurrentQueue<(string, string)>(); 
        private readonly AnswerBlock[] _answerBlocks =
        {
            new AnswerBlock(),
            new AnswerBlock(),
            new AnswerBlock()
        };

        void Start()
        {
            actions[0] = PlayerPrefs.GetString("OperatorPlus", "0")[0];
            actions[1] = PlayerPrefs.GetString("OperatorMinus", "0")[0];
            actions[2] = PlayerPrefs.GetString("OperatorMultiply", "0")[0];
            actions[3] = PlayerPrefs.GetString("OperatorDivide", "0")[0];
            
            _generator = new Generator(
                _standardDifficulties[GetDifficulty(PlayerPrefs.GetString("CurrentDifficulty"))],
                SetPossibleActionsHashSet(actions)
            );

            FindAllObjects();
            
            _isAdsCooldown = true;
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
                var correctBlockNum = new Random().Next(0, 3);

                _answerBlocks[correctBlockNum].AnswerVariant.text = task.Item2;
                _rightAnswer = task.Item2;
                for (var i = 0; i < _answerBlocks.Length; i++)
                {
                    if (i == correctBlockNum)
                        continue;
                    var nextVariant = GetRandomAnswerVariant(task.Item2, 30, mainRandomGenerator);
                    _answerBlocks[i].AnswerVariant.text = nextVariant;
                }
                _toGetNewTask = false;
            }

            if (playerLivesCount == 0)
            {
                _playerLivesCountText.text = playerLivesCount.ToString();
                if (_isAdsCooldown)
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

                        _isAdsCooldown = false;
                    }
                }
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            //На релизе убрать PlayerMovement, оставить ТОЛЬКО SwipeManagement
            Debug.Log($"Collision object: {other.gameObject.name} Right answer is {_rightAnswer}");
            var otherAnswerVariant = other.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
            if (!otherAnswerVariant.Equals(_rightAnswer))
            {
                if (playerLivesCount == 1)
                {
                    AudioListener.pause = true;
                    //DebugCode
                    // if (_swipeMode)
                    //     transform.GetComponent<SwipeManagement>().playerSpeed = 0;
                    // else
                    //     transform.GetComponent<PlayerMovement>().playerSpeed = 0;
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
            else
                _score += 1;
            
            PlayerPrefs.SetInt("RightAnswerCount", _score);
            _toGetNewTask = true;
            _answerContainer.transform.Translate(new Vector3(distanceToNextTask, 0, 0));
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

        private HashSet<char> SetPossibleActionsHashSet(char[] actions)
        {
            HashSet<char> possibleActions = new HashSet<char>();

            foreach (var action in actions)
            {
                if (action.Equals('0') == false)
                {
                    possibleActions.Add(action);
                }
            }
            
            return possibleActions;
        }

        private int GetDifficulty(string currentDifficulty)
        {
            int result = -1;
            switch (currentDifficulty)
            {
                case "Легко":
                    result = 0;
                    break;
                case "Нормально":
                    result = 1;
                    break;
                case "Тяжело":
                    result = 2;
                    break;
            }
            return result;
        }
        #endregion
    }
}
