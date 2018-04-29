/*
 * Harshal Vadnere
 * CPSC 5051, Seattle University
 * This is free and unencumbered software released into the public domain.
 */
// AUTHOR: harsh
// FILENAME: encryptWord.cs
// DATE: April 15, 2018
// REVISION HISTORY: NA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPSC_5051_PA2
{
    /**
     * Supporting enum class for EncrytpWord objects.
     */ 
    enum State { OFF, ON }; //States on the EncryptWord object
    enum GuessPrompts { HighGuess, LowGuess, RightGuess, Error };
    //prompts for guess shift method.

    /**
     * Supporting class for EncrytpWord objects to generate unique shift values
     * within specfied range.
     * Provides a method to get the next available from the list.
     *
     */
    class ShiftValGen
    {
        private int maxRange = 25; //Shift value cannot be greater than total 
        //num of alphabets
        private int minRange = 1;
        private int totalRandomCount = 20;
        public List<int> ShiftValList; //Data members that holds the values

        /**
         * Public constructor.
         * Pre-conditions:
         *      none
         * Post-conditions:
         *      List of random unique int.
         */
        public ShiftValGen()
        {
            this.ShiftValList = 
                this.UniqueRandomNoList(maxRange, totalRandomCount);
        }

        /**
         * This methods gets the next value from list.
         * Pre-conditions:
         *  Initialized ShiftValGen object.
         * Post-conditions:
         *  totalRandomCount reduced by 1.
         *  @param refernece to int
         *  @return true if value updated, false othewise
         */
        public bool getNext(ref int val)
        {
            if (!ShiftValList.Any())
            {
                return false;
            }

            val = ShiftValList[totalRandomCount-1];
            totalRandomCount--;

            return true;
        }

        /**
         * This methods generates a list of unique random values.
         * Pre-conditions:
         *  Initialized ShiftValGen object.
         * Post-conditions:
         *  totalRandomCount reduced by 1.
         *  @param maxRange of random num, totalRandomCount - number of random
         *  values
         *  @return list of randome unique values
         */
        private List<int> UniqueRandomNoList(int maxRange,
            int totalRandomnoCount)
        {

            List<int> noList = new List<int>();
            int count = 0;
            Random r = new Random();
            List<int> listRange = new List<int>();
            for (int i = 0; i < totalRandomnoCount; i++)
            {
                listRange.Add(i);
            }
            while (listRange.Count > 0)
            {
                int item = r.Next(maxRange) + minRange;   
                if (!noList.Contains(item) && listRange.Count > 0)
                {
                    noList.Add(item);
                    listRange.Remove(count);
                    count++;
                }
            }
            return noList;
        }
    }

    /**
     * Description:
     * This file describes the interface for the EncryptWord class. Users can 
     * create EncryptWord objects that provide basic Ceaser cipher 
     * functionality.For example: with a shift of ‘3’ letter ‘a’ will be 
     * encrypted as ‘d’; ‘b’ encrypted as ‘e’, … ,‘w’ as ‘z’, ‘x’ as ‘a’, ‘y’ 
     * as ‘b’ and ‘z’ as 'c'. 
     * User can query an EncryptWord object using GuessShift as to whether 
     * a passed integer is, in fact, the correct shift value. 
     * After guessing the shift value user can utilize Decrypt funcionality to
     * decrypt a word. User can Refresh a EncryptWord object which is in a OFF 
     * State.
     * EncryptWord object also yields statistics -- number of queries, 
     * high guesses, low guesses and average ‘guess value’.
     * 
     * Intended Use: For creating simple guessing game.
     * 
     * Interface Invariants:
     * Only alphabet characters (Upper and/or Lower) are accepted for 
     * encryption.
     * Only integer values can be passed for guessing the shift value.
     * No getter method for querying shift value or State of EncryptWord object
     * 
     * Implmentation Invariants:
     * Defensive programming methodology is used. Input word validations are 
     * made in by the Encrypt and Decrypt methods.
     * Dependencies: The EncryptWord class has critical dependencies on the 
     * enum classes - State and GuessPrompts and ShiftValGen.
     * 
     * State: 
     * EncryptWord objects have two states ON and OFF. Reset is not a state. It
     * is implemented as a step to transition from OFF to ON.
     * State Transitions: 
     * All EncryptWord objects are initialized with State ON.
     * If user guesses the shift value State transition to OFF.
     * Refresh transitions State to ON.
     * Exception: If ValidShift flag is false, State is OFF.
     * 
     * Assumptions:
     * Application programmer will need to implement functionality ensure 
     * ShiftValGen list is not empty.
     * Application programmer will need to use State to daignose issues.
     * This program will be used for demo purposes only and not for security 
     * related encryption.
     * EncryptWord object is initialized with a shift value - random integer
     * between 1 and 25.
     * User will need to figure out he shift value (use GuessShift function)
     * in order to use the Decrypt method.
     * 
     */
    class EncryptWord
    {

        private int MIN = 4; //Minimum length of the word processed by object
        private int ASCII_UPPER = 65; //Decimal value of first upper case 
                                      //alphabet 'A'
        private int ASCII_LOWER = 97; //Decimal value of first lower case 
                                      //alphabet 'a'
        private int ALPHABETS = 26; //Number of alphabets
        private int ASCII_UPPER_MAX = 90; //Decimal value of first upper case 
                                          //alphabet 'Z'
        private int ASCII_LOWER_MAX = 122; //Decimal value of first lower case 
                                      //alphabet 'z'
        
        private int shift; //Holds the shift value of cipher
        private bool ValidShift; //flag to indicate valide shift value set.
        private State currentState;//Holds the state of the object
        private int queries, Num_highGuess, Num_lowGuess, SumGuessValue, 
            Num_Guess;
        //Stats 
        /**
         * Private method to reset the EncryptWord object. It makes check to 
         * ensure ValidShift is true before setting State to ON.
         * Pre-conditions:
         *  Initialized EncryptWord object.
         * Post-conditions:
         *  Stat counters reset, shift value updated, State set to ON
         *  @param ShiftValGen list 
         *  
         */
        private void Reset(ShiftValGen list)
        {
            
            this.queries = 0;
            this.Num_highGuess = 0;
            this.Num_lowGuess = 0;
            this.SumGuessValue = 0;
            this.Num_Guess = 0;
            this.ValidShift = list.getNext(ref this.shift);
            if(!this.ValidShift)
            {
               currentState = State.OFF;
            }
            currentState = State.ON;
            
        }

        /**
         *  Suppressing default construtor.
         */
        private EncryptWord()
        {
           
        }

        /**
        * Construtor for EncryptWord object.
        * Pre-conditions:
        *  Initialized ShiftValGen object.
        * Post-conditions:
        *  Initialized EncryptWord object, State ON
        *  @param ShiftValGen list 
        *  
        */
        public EncryptWord(ShiftValGen list)
        {
            this.Reset(list);
        }

        /**
         * This utility method provides the stats of the EncryptWord object.
         * Stats: total successful queries, number of high guesses, number of 
         * low guesses and average guess value.
         * Pre-conditions:
         *      Initialized EncryptWord object.
         * Post-conditions:
         *      none
         */
        public string getStats()
        {
            
            StringBuilder stats = new StringBuilder();
            stats.AppendLine("Successful Queries : " + this.queries);
            stats.AppendLine("Number of HighGuess: " +
                this.Num_highGuess);
            stats.AppendLine("Number of LowGuess : " + 
                this.Num_lowGuess);
            int AvgGuess;
            if(this.Num_Guess != 0)
            {
                AvgGuess = this.SumGuessValue / this.Num_Guess;
            }
            else
            {
                AvgGuess = 0;
            }
            stats.AppendLine("Average guess value: " + AvgGuess);

            return stats.ToString();
        }
        /**
         * This method provides functionality to refresh an EncryptWord object
         * which is in OFF state.
         * Note that if Refresh does not change the State to ON indicates that
         * ValidShift is false, caused when ShiftValGen list is empty.
         * Pre-conditions:
         *      EncryptWord object State OFF
         * Post-conditions:
         *      State ON
         * @param ShiftValGen list
         * @return true if reset success, false otherwise
         */
        public bool Refresh(ShiftValGen list)
        {
            if (this.currentState == State.ON)
            {
                return false;
            }
            if(this.currentState == State.OFF)
            {
                this.Reset(list);
                if (this.currentState == State.OFF)
                {
                    return false;
                }
            }
            return true;        
        }

        /**
         * This method encrypts a word. Makes input validations.
         * Pre-conditions:
         *      EncryptWord object State ON
         * Post-conditions:
         *      none
         * @param word to be encrypted, ref to where encrypted word needs to 
         * be updated
         * @return true if encrypted word ref updated, false otherwise
         */
        public bool Encrypt(string word, ref string encryptedWord)
        {

            if (word.Length < MIN || currentState == State.OFF)
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if ((int)word[i] > ASCII_UPPER_MAX && 
                    (int)word[i] < ASCII_LOWER ||
                     (int)word[i] < ASCII_UPPER || 
                     (int)word[i] > ASCII_LOWER_MAX)
                {
                    return false;
                }
            }
            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsUpper(word[i]))
                {
                    encryptedWord += (char)(((int)word[i] + shift
                                                        - ASCII_UPPER)
                                                   % ALPHABETS + ASCII_UPPER);
                }
                else
                {
                    encryptedWord += (char)(((int)word[i] + shift
                                                          - ASCII_LOWER)
                                                   % ALPHABETS + ASCII_LOWER);
                }
            }
            this.queries++;
            return true;
        }
        public bool Decrypt(string word, int shiftValue, 
                                                    ref string decryptText)
        {
            
            if (word.Length < MIN ||
                this.shift != shiftValue || this.currentState == State.ON)
            {
                return false;
            }
            
            for (int i = 0; i < word.Length; i++)
            {
                if ((int)word[i] > ASCII_UPPER_MAX &&
                    (int)word[i] < ASCII_LOWER ||
                     (int)word[i] < ASCII_UPPER ||
                     (int)word[i] > ASCII_LOWER_MAX)
                {
                    return false;
                }
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsUpper(word[i]))
                {
                    decryptText += (char)(((int)word[i] +(ALPHABETS - 
                        shiftValue) - ASCII_UPPER) % ALPHABETS + ASCII_UPPER);
                }
                else
                {
                    decryptText += (char)(((int)word[i] + (ALPHABETS -
                        shiftValue) - ASCII_LOWER) % ALPHABETS + ASCII_LOWER);
                }
            }
          
            return true;
        }

        /**
         * This method enables user to guess the shift value. Provides feedback
         * in form of GuessPrompts.
         * Pre-conditions:
         *      EncryptWord object State ON
         * Post-conditions:
         *      After RightGuess State OFF
         * @param guess value
         * @return GuessPormpts
         */
        public GuessPrompts ShiftGuess(int GuessValue)
        {
            if (currentState != State.ON)
            {
                return GuessPrompts.Error;
            }
            this.Num_Guess++;
            this.SumGuessValue += GuessValue;

            if(GuessValue < this.shift)
            {
                this.Num_lowGuess++;
                return GuessPrompts.LowGuess;
            }
            else if (GuessValue > this.shift)
            {
                this.Num_highGuess++;
                return GuessPrompts.HighGuess;
            }
            this.currentState = State.OFF;
            return GuessPrompts.RightGuess;
        }

        
    }
}
