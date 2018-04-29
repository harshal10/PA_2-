/*
 * Harshal Vadnere
 * CPSC 5051, Seattle University
 * This is free and unencumbered software released into the public domain.
 */
// AUTHOR: harsh
// FILENAME: p1.cs
// DATE: April 15, 2018
// REVISION HISTORY: NA


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPSC_5051_PA2
{
    /**
     * Description:
     * Driver program for EncryptWord class to demonstrate instantiation, 
     * validations, functionlity and state transitions.
     * Initializes dependent classes (ShiftValGen). 
     * Intended Use: Demo of EncryptWord objects.
     * This program requires user input (int) for testing GuessShift method.
     * 
     * Assumptions:
     * Uses 4 or more character alphabets for encryption demo.
     * User is able to provide input for GuessShift test sequence.
     */
      
    class p1
    {
        /**
         * This method provides functionality to execute the driver.
         * Pre-conditions:
         *      None
         * Post-conditions:
         *      None
         */
        public void RunEncryptWordDriver()
        {

            Console.Write("Welcome the driver for EncryptWord class.\n" +
                "This program instantiates a bunch of EncryptWord objects\n"
               + "and runs a series of test sequences to demonstrate\n" +
               "functionality, input validations and state transitions.\n\n" +
               "The interface will print a test label, input and output \n" +
               "for each test. \n"+
               "<sample test sequence label>...\n"+
               "<input>\n"+
               "<sample function label > [expected output]: output\n" );

            Console.Write("\nInitializing ShiftValList...\n");
            ShiftValGen ListNums = new ShiftValGen();
             
            Console.Write("Instantiateing List/collection of " +
                "EncryptWord objects...\n");
            List<EncryptWord> collection = new List<EncryptWord>
            {
                new EncryptWord(ListNums){},
                new EncryptWord(ListNums){},
                new EncryptWord(ListNums){}
            };
            Console.Write("collection {0,1,2} initialized State ON\n");
            Console.Write("Running Encrypt function test sequences....\n");
            string text0 = "abcde";
            string encryptdText0 = "";
            Console.Write("Input Text : " + text0 + "\n");
            bool flag0 = collection[0].Encrypt(text0, ref encryptdText0);
            Console.Write("For collection[0] Encryption Success [True]: " + 
                flag0 + "\n");
            Console.Write("                             Encrypted Text: " 
                + encryptdText0 + "\n");
            encryptdText0 = "";
            flag0 = collection[1].Encrypt(text0, ref encryptdText0);
            Console.Write("For collection[1] Encryption Success [True]: " +
                flag0 + "\n");
            Console.Write("                             Encrypted Text: "
                + encryptdText0 + "\n");
            encryptdText0 = "";
            flag0 = collection[2].Encrypt(text0, ref encryptdText0);
            Console.Write("For collection[2] Encryption Success [True]: " +
                flag0 + "\n");
            Console.Write("                             Encrypted Text: "
                + encryptdText0 + "\n");
            
            Console.Write("\n----------------------\n");
            Console.Write("Running Input Validation test sequences... \n");
            Console.Write("Passing invalid arguments: \n");
            Console.Write("--passing text of invalid length " +
                "(less than 4 char)...\n");
            string text11 = "xzy";
            Console.Write("Input Text : " + text11 + "\n");
            string encryptdText11 = "";
            bool flag11 = collection[0].Encrypt(text11, ref encryptdText11);
            Console.Write("For collection[0] Encryption Success [False]: " + 
                flag11 + "\n");

            flag11 = collection[1].Encrypt(text11, ref encryptdText11);
            Console.Write("For collection[1] Encryption Success [False]: " +
                flag11 + "\n");

            flag11 = collection[2].Encrypt(text11, ref encryptdText11);
            Console.Write("For collection[2] Encryption Success [False]: " +
                flag11 + "\n\n");

            Console.Write("--passing text with invalid char (@,3,!,%)... \n");
            string text14 = "#$%A";
            Console.Write("Input Text : " + text14 + "\n");
            string encryptdWord14 = "";
            bool flag14 = collection[0].Encrypt(text14, ref encryptdWord14);
            Console.Write("For collection[0] Encryption Success [False]: " + 
                flag14 + "\n");

            flag14 = collection[1].Encrypt(text14, ref encryptdWord14);
            Console.Write("For collection[1] Encryption Success [False]: " +
                flag14 + "\n");

            flag14 = collection[2].Encrypt(text14, ref encryptdWord14);
            Console.Write("For collection[2] Encryption Success [False]: " +
                flag14 + "\n");

            Console.Write("\n----------------------\n");
            
            Console.Write("Instantiate EncryptWord objects...\n");
            Console.Write("Obj1 initialized State ON\n");
            EncryptWord obj1 = new EncryptWord(ListNums);
            
            Console.Write("Verify State ON by Running Encrypt test sequence..." 
                +"\n");
            
            string text33 = "ABCD";
            string encryptdText33 = "";
            Console.Write("Input Text : " + text33 + "\n");
            bool flag33 = obj1.Encrypt(text33, ref encryptdText33);
            Console.Write("Encryption Success [True]: " + flag33 + "\n");
            Console.Write("Encrypted Text :" + encryptdText33 + "\n");
            Console.Write("\n\n");
            Console.Write("Running GuessShift function test sequences...\n"+
                "Requires user input!\n");

            int UserGuess;
            int ShiftValue = 0; //Holds the correct shift value after 
                                    //RightGuess
            GuessPrompts feedback;
            bool repeat = true;
            
            while (repeat)
            {
                Console.Write("Enter guess value (int): ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    repeat = false;
                    break;
                }
                UserGuess = Convert.ToInt32(input);
                if (UserGuess <= 0)
                {
                    repeat = false;
                    break;
                }
                feedback = obj1.ShiftGuess(UserGuess);
                if (feedback == GuessPrompts.RightGuess)
                {
                    ShiftValue = UserGuess;
                    repeat = false;
                }
                Console.Write("Response from ShiftGuess, passing[" + UserGuess
                    + "]: " + feedback + " \n");
            }
            
            Console.Write("\n\n");
            Console.Write("Hit enter to continue...");
            Console.ReadLine();
            
            if (ShiftValue == 0)
            {
                Console.Write("GuessShift test sequence incomplete!\n");
                Console.Write("Cannot Test Decrypt. User must guess the \n" +
                    "shift value in GuessShift sequence.");
            }
            else
            {
                Console.Write("Verify state transition to OFF\n"
                    +"post RightGuess by Running Encrypt test sequence...\n");
                string text = "ABCD";
                string encryptdText = "null";
                Console.Write("Input Text : " + text + "\n");
                bool flag1 = obj1.Encrypt(text, ref encryptdText);
                Console.Write("Encryption Success [False]: " + flag1 + "\n");
                Console.Write("Encrypted Text [null]:" + encryptdText + "\n");
                Console.Write("\n\n");

                Console.Write("Decrypt function test sequence...\n");
                Console.Write("Input Text(encrypted in previous step):" +
                    encryptdText33 + "\n");
                string decryptText1 = "";
                bool flag6 = obj1.Decrypt(encryptdText33, ShiftValue,
                                                        ref decryptText1);
                Console.Write("Decrypt Success [True]: " + flag6 + "\n");
                Console.Write("Output Decrypted Text : " + decryptText1 + 
                    "\n");
            }
            Console.Write("\n----------------------\n");
            Console.Write("Printing Stats.... \n");
            Console.Write("obj1: \n" + obj1.getStats() + "\n");
            Console.Write("\n\n");
            Console.Write("Running Refresh function test sequence...\n");
            Console.Write("\n----------------------\n");
            if (ShiftValue == 0)
            {
                Console.Write("GuessShift test sequence incomplete.\n" +
                    "No State transition to OFF. Refresh not available.\n");
                bool flag20 = obj1.Refresh(ListNums);
                Console.Write("Refresh Success [False]: " + flag20 + "\n");
                Console.Write("\n\n");
            }
            else
            {
                bool flag20 = obj1.Refresh(ListNums);
                Console.Write("Refresh Success [True]: " + flag20 + "\n");
                Console.Write("Verify state transition to ON post Refresh by\n" 
                    +"Running Encrypt test sequence...\n");
                string text2 = "ZxYPqr";
                Console.Write("Input Text :" + text2 + "\n");
                string encryptdText2 = "";
                bool flag2 = obj1.Encrypt(text2, ref encryptdText2);
                Console.Write("Encryption Success [True]: " + flag2 + "\n");
                Console.Write("Encrypted Text: " + encryptdText2 + "\n");
                Console.Write("\n\n");

            }

            Console.Write("Printing Stats after Refresh.... \n");
            Console.Write("obj1: \n" + obj1.getStats() + "\n");
            Console.Write("\n\n");
            Console.Write("End of Program. Thank you\n");
        }

    }
}
