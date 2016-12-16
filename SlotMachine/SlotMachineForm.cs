/**
 * Jake Anstey
 * 200281238
 * Slot Machine - RAD
 * updated 2016-12-15
 */
using SlotMachine.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{
    public partial class SlotMachineForm : Form
    {
        private int playerMoney = 1000;
        private int winnings = 0;
        private int jackpot = 5000;
        private float turn = 0.0f;
        private int playerBet = 0;
        private float winNumber = 0.0f;
        private float lossNumber = 0.0f;
        private string[] spinResult;
        private string fruits = "";
        private float winRatio = 0.0f;
        private float lossRatio = 0.0f;
        private int grapes = 0;
        private int bananas = 0;
        private int oranges = 0;
        private int cherries = 0;
        private int bars = 0;
        private int bells = 0;
        private int sevens = 0;
        private int blanks = 0;

        private Random random = new Random();

        public SlotMachineForm()
        {
            InitializeComponent();
            playerBet = 10;
            UpdateMachine();
        }

        /* Utility function to show Player Stats */
        private void showPlayerStats()
        {
            winRatio = winNumber / turn;
            lossRatio = lossNumber / turn;
            string stats = "";
            stats += ("Jackpot: " + jackpot + "\n");
            stats += ("Player Money: " + playerMoney + "\n");
            stats += ("Turn: " + turn + "\n");
            stats += ("Wins: " + winNumber + "\n");
            stats += ("Losses: " + lossNumber + "\n");
            stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
            stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
            MessageBox.Show(stats, "Player Stats");
        }

        /* Utility function to reset all fruit tallies*/
        private void resetFruitTally()
        {
            grapes = 0;
            bananas = 0;
            oranges = 0;
            cherries = 0;
            bars = 0;
            bells = 0;
            sevens = 0;
            blanks = 0;
        }

        /* Utility function to reset the player stats */
        private void resetAll()
        {
            playerMoney = 1000;
            winnings = 0;
            jackpot = 5000;
            turn = 0;
            playerBet = 10;
            winNumber = 0;
            lossNumber = 0;
            winRatio = 0.0f;
            SpinPictureBox.Image = Resources.spin;
            UpdateMachine();
        }

        /* Check to see if the player won the jackpot */
        private void checkJackPot()
        {
            /* compare two random values */
            var jackPotTry = this.random.Next(51) + 1;
            var jackPotWin = this.random.Next(51) + 1;
            if (jackPotTry == jackPotWin)
            {
                MessageBox.Show("You Won the $" + jackpot + " Jackpot!!","Jackpot!!");
                playerMoney += jackpot;
                jackpot = 1000;
            }
        }

        /* Utility function to show a win message and increase player money */
        private void showWinMessage()
        {
            playerMoney += winnings;
            MessageBox.Show("You Won: $" + winnings, "Winner!");
            resetFruitTally();
            checkJackPot();
        }

        /* Utility function to show a loss message and reduce player money */
        private void showLossMessage()
        {
            playerMoney -= playerBet;
            MessageBox.Show("You Lost!", "Loss!");
            resetFruitTally();
        }

        /* Utility function to check if a value falls within a range of bounds */
        private bool checkRange(int value, int lowerBounds, int upperBounds)
        {
            return (value >= lowerBounds && value <= upperBounds) ? true : false;
            
        }

        /* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
        private string[] Reels()
        {
            string[] betLine = { " ", " ", " " };
            int[] outCome = { 0, 0, 0 };

            for (var spin = 0; spin < 3; spin++)
            {
                outCome[spin] = this.random.Next(65) + 1;

               if (checkRange(outCome[spin], 1, 27)) {  // 41.5% probability
                    betLine[spin] = "poo";
                    blanks++;
                    }
                else if (checkRange(outCome[spin], 28, 37)){ // 15.4% probability
                    betLine[spin] = "Grape";
                    grapes++;
                }
                else if (checkRange(outCome[spin], 38, 46)){ // 13.8% probability
                    betLine[spin] = "Banana";
                    bananas++;
                }
                else if (checkRange(outCome[spin], 47, 54)){ // 12.3% probability
                    betLine[spin] = "Orange";
                    oranges++;
                }
                else if (checkRange(outCome[spin], 55, 59)){ //  7.7% probability
                    betLine[spin] = "Cherry";
                    cherries++;
                }
                else if (checkRange(outCome[spin], 60, 62)){ //  4.6% probability
                    betLine[spin] = "Bar";
                    bars++;
                }
                else if (checkRange(outCome[spin], 63, 64)){ //  3.1% probability
                    betLine[spin] = "Bell";
                    bells++;
                }
                else if (checkRange(outCome[spin], 65, 65)){ //  1.5% probability
                    betLine[spin] = "Seven";
                    sevens++;
                }

            }
            return betLine;
        }

        /* This function calculates the player's winnings, if any */
        private void determineWinnings()
        {
            if (blanks == 0)
            {
                if (grapes == 3)
                {
                    winnings = playerBet * 10;
                }
                else if (bananas == 3)
                {
                    winnings = playerBet * 20;
                }
                else if (oranges == 3)
                {
                    winnings = playerBet * 30;
                }
                else if (cherries == 3)
                {
                    winnings = playerBet * 40;
                }
                else if (bars == 3)
                {
                    winnings = playerBet * 50;
                }
                else if (bells == 3)
                {
                    winnings = playerBet * 75;
                }
                else if (sevens == 3)
                {
                    winnings = playerBet * 100;
                }
                else if (grapes == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (bananas == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (oranges == 2)
                {
                    winnings = playerBet * 3;
                }
                else if (cherries == 2)
                {
                    winnings = playerBet * 4;
                }
                else if (bars == 2)
                {
                    winnings = playerBet * 5;
                }
                else if (bells == 2)
                {
                    winnings = playerBet * 10;
                }
                else if (sevens == 2)
                {
                    winnings = playerBet * 20;
                }
                else if (sevens == 1)
                {
                    winnings = playerBet * 5;
                }
                else
                {
                    winnings = playerBet * 1;
                }
                winNumber++;
                playerMoney += winnings;
                //showWinMessage();
            }
            else
            {
                lossNumber++;
                playerMoney -= playerBet;
                jackpot += playerBet / 10;
                //showLossMessage();
            }

        }

        /// <summary>
        /// Upates the UI for the player, called after every spin
        /// </summary>
        private void UpdateMachine()
        {
            JackpotLabel.Text = "$" + jackpot;
            CreditsLabel.Text = "$" + playerMoney;
            BetLabel.Text = "$" + playerBet;
            PaidLabel.Text = "$" + winnings;
            resetFruitTally();
        }

        /// <summary>
        /// Checks the player balance every spin and changes the spin button to disables
        /// returns false if out of funds
        /// </summary>
        /// <returns></returns>
        private Boolean CheckFunds()
        {
            if (playerBet > playerMoney)
            {
                SpinPictureBox.Image = Resources.spin_disabled;
                return false;
            }
            SpinPictureBox.Image = Resources.spin;
            return true;
        }

        private void SpinPictureBox_Click(object sender, EventArgs e)
        {
            //playerBet = 10; // default bet amount

            if (CheckFunds() == false)
                return;

            if (playerMoney == 0)
            {
                if (MessageBox.Show("You ran out of Money! \nDo you want to play again?","Out of Money!",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    resetAll();
                    showPlayerStats();
                }
            }
            else if (playerBet > playerMoney)
            {
                MessageBox.Show("You don't have enough Money to place that bet.", "Insufficient Funds");
            }
            else if (playerBet < 0)
            {
                MessageBox.Show("All bets must be a positive $ amount.", "Incorrect Bet");
            }
            else if (playerBet <= playerMoney)
            {
                spinResult = Reels();
                //fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
                //MessageBox.Show(fruits);
                FirstResultPictureBox.BackgroundImage = (Image)Resources.ResourceManager.GetObject(spinResult[0].ToLower());
                SecondResultPictureBox.BackgroundImage = (Image)Resources.ResourceManager.GetObject(spinResult[1].ToLower());
                ThirdResultPictureBox.BackgroundImage = (Image)Resources.ResourceManager.GetObject(spinResult[2].ToLower());
                determineWinnings();
                UpdateMachine();
                CheckFunds();
                winnings = 0;
                turn++;
                //showPlayerStats();
            }
            else
            {
                MessageBox.Show("Please enter a valid bet amount");
            }
        }

        /// <summary>
        /// prompt user for reset of game; resets the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Reset Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            resetAll();
        }

        /// <summary>
        /// Prompts the user to quit the game; cancels the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetOne_Click(object sender, EventArgs e)
        {
            playerBet = 1;
            CheckFunds();
            UpdateMachine();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetFivePictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 5;
            CheckFunds();
            UpdateMachine();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetTenPictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 10;
            CheckFunds();
            UpdateMachine();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetTwentyfivePictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 20;
            CheckFunds();
            UpdateMachine();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetFiftyPictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 50;
            CheckFunds();
            UpdateMachine();
        }

        /// <summary>
        /// changes bet to specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetHundredPictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 100;
            CheckFunds();
            UpdateMachine();
        }
    }

}
