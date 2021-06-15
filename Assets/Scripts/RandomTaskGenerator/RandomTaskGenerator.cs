using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Random = System.Random;

namespace RandomTaskGenerator
{
    public class Generator
    {
        private readonly char[] _operations;
        private double _doubleDelta = 1e-10;
        private Difficulty Difficulty { get; }
        private HashSet<char> PossibleOperators { get; }
        private int DifficultMaxNumber { get; } = 200;
        private int SimpleMaxNumber { get; } = 20;
        private int LongMaxOperandLenght { get; } = 4;
        private int ShortMaxOperandLenght { get; } = 2;
        private const int DifficultMaxAnswer = 200;
        private const int SimpleMaxAnswer = 50;

        public Generator(Difficulty difficulty, HashSet<char> possibleOperators)
        {
            Difficulty = difficulty;
            PossibleOperators = possibleOperators;
        }

        public (string, string) GenerateNewTask(Random numberGenerator)
        {
            var (maxNumber, _, maxAnswer) = GetOptionsFromRules();

            var operationNum = numberGenerator.Next(0, PossibleOperators.Count);

            var (stringBuilder, item2) = GenerateOneTask(PossibleOperators.ElementAt(operationNum), maxNumber, maxAnswer, numberGenerator);
            
            return (stringBuilder, item2);
        }

        private (string, string) GenerateOneTask(char @operator, int maxNumber, int maxAnswer, Random numberGenerator)
        {
            var (firstOperand, secondOperand, operationResult) = GetOperandsAndResult(1, maxNumber, @operator, numberGenerator);
            
            while (operationResult - Math.Floor(operationResult) > _doubleDelta || operationResult > maxAnswer || operationResult <= 0)
            {
                (firstOperand, secondOperand, operationResult) = GetOperandsAndResult(1, maxNumber, @operator, numberGenerator);
            }

            return ($"{firstOperand}{@operator}{secondOperand}", operationResult.ToString(CultureInfo.InvariantCulture));
        }

        private (int, int, double) GetOperandsAndResult(int minNumber, int maxNumber, char @operator, Random numberGenerator)
        {
            var firstOperand = numberGenerator.Next(minNumber,maxNumber); 
            var secondOperand = numberGenerator.Next(minNumber, maxNumber);
            var operationResult = GetOperationResult(firstOperand, secondOperand, @operator);
            return (firstOperand, secondOperand, operationResult);
        }

        private double GetOperationResult(int firstOperand, int secondOperand, char @operator)
        { 
            switch(@operator)
            {
                case '+':
                    return firstOperand + secondOperand;
                case '-':
                    return firstOperand - secondOperand;
                case 'x':
                    return firstOperand * secondOperand;
                case '÷':
                    return secondOperand == 0 ? 1e10 : (double) firstOperand / secondOperand;
                default:
                    throw new Exception("No match operator: " + @operator);
            };
        }

        private (int, int, int) GetOptionsFromRules()
        {
            var maxNumber = Difficulty.LargeNumbers ? DifficultMaxNumber : SimpleMaxNumber;
            var maxOperandCount = Difficulty.LongTasks ? LongMaxOperandLenght : ShortMaxOperandLenght;
            var maxAnswer = Difficulty.DifficultAnswer ? DifficultMaxAnswer : SimpleMaxAnswer;
            return (maxNumber, maxOperandCount, maxAnswer);
        }
    }
    
    public class Difficulty
    {
        public bool LargeNumbers { set; get; }
        public bool LongTasks { set; get; }
        public bool DifficultAnswer { set; get; }
    }
}

