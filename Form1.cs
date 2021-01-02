/**************************************************************
 Name: Raveenth Maheswaran
 Date: 25/01/2017
 Title: rotatingShip

 Project Description:

 To develop a game similar to space invaders. The objective
 for this game is to shoot all the enemies before they touch
 you. You will lose if the enemies reach to the bottom.
 Controls are shown in the game. RUN GAME IN F5 NOT CTRL+F5
 ************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;//for the soundplayer

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SoundPlayer gameMusic = new SoundPlayer("Run 2 Theme Song.wav");

        struct missiles
        {
            public int x2; // x co-ordinate for the missiles
            public int y2; // y co-ordinate for the missiles 
            public bool showing; // Shows the image for the missiles
        };

        // All declared variables
        int x = 320;  // x co-ordinate
        int y = 480;  // y co-ordinate
        Graphics dc;
        int directionx = 1; // moves the enemies in an x direction
        int directiony = 0; // moves the enemies in a y direction
        Bitmap curBitmap;
        Bitmap backBitmap;
        Image character; // Declares the image for the character
        Image background; // Declares the image for the background
        Image shoot; // Declares the image for the missiles
        int xcord = 0; // The x co-ordinate for the bpoint
        int ycord = 0; // The y co-ordinate for the bpoint
        missiles[] missileArray = new missiles[20]; // Array for the missiles 
        int missilecounter = 0; // The amount of missiles being fired
        int score = 0; // The counter for the score
        int elapsedTIme = 0; //The value of the timer

        Image[] enemies = new Image[10]; //Array for the enemies on the first row
        int[] enemiesx = new int[10]; //The x co-ordinate for the enemies
        int[] enemiesy = new int[10]; //The y co-ordinates for the enemies

        Image[] enemies2 = new Image[10]; //Array for the enemies on the second row
        int[] enemiesx2 = new int[10]; //The x co-ordinate for the enemies
        int[] enemiesy2 = new int[10]; //The y co-ordinates for the enemies

        Image[] enemies3 = new Image[10]; //Array for the enemies on the third row
        int[] enemiesx3 = new int[10]; //The x co-ordinate for the enemies
        int[] enemiesy3 = new int[10]; //The y co-ordinates for the enemies

        Image[] enemies4 = new Image[10]; //Array for the enemies on the fourth row
        int[] enemiesx4 = new int[10]; //The x co-ordinate for the enemies
        int[] enemiesy4 = new int[10]; //The y co-ordinates for the enemies

        int DOWN = 10; //The y direction when the enemies hit the wall 
        int down2 = 20; //The y direction when the enemies hit the wall 

        //Boolean array for making enemies appear and disappear
        bool[] enemy = new bool[10]; 

        bool[] enemy2 = new bool[10];

        bool[] enemy3 = new bool[10];

        bool[] enemy4 = new bool[10];

        //Create a temporary offscreen Graphics object from the bitmap
        Graphics offscreen;

        /******************************************************
         * Name: Raveenth Maheswaran
         * Title: Form1
         * Date: 23/01/2017
         * Input: All the offscreen graphics from curBitmap
         * Output: Displaying the entire game
         *****************************************************/
        public Form1()
        {
            InitializeComponent();

            this.Show();

            //create the onscreen graphics
            dc = this.CreateGraphics();

            //Plays game music during game
            gameMusic.PlayLooping();

            character = Image.FromFile("Shooter.png"); //found in project folder 
            background = Image.FromFile("background.jpg"); //found in project folder
            shoot = Image.FromFile("Shoot copy.png"); //found in project folder

            //Generates 20 missiles
            for (int x = 0; x < 20; x++)
            {
                missileArray[x].showing = false;
            }
                       
            //Generates 10 enemies
            for (int b = 0; b < 10; b++)
            {
                enemies[b] = Image.FromFile("MainCharacter.png"); //found in project folder
                enemiesx[b] = 50 * b;
                enemy[b] = true;
            }

            //Generates 10 enemies
            for (int l = 0; l < 10; l++)
            {
                enemies2[l] = Image.FromFile("enemy2.png"); //found in project folder
                enemiesx2[l] = 50 * l;
                enemy2[l] = true;
            }

            //Generates 10 enemies
            for (int d = 0; d < 10; d++)
            {
                enemies3[d] = Image.FromFile("enemy3.png"); //found in project folder
                enemiesx3[d] = 50 * d;
                enemy3[d] = true;
            }

            //Generates 10 enemies
            for (int x = 0; x < 7; x++)
            {
                enemies4[x] = Image.FromFile("enemy4.png"); //found in project folder
                enemiesx4[x] = 68 * x;
                enemy4[x] = true;
            }

            curBitmap = new Bitmap(this.Width, this.Height);
            backBitmap = new Bitmap(background, background.Width, background.Height); //Bitmap with the background

            //Create a temporary Graphics object from the bitmap
            offscreen = Graphics.FromImage(curBitmap);
        }

        /*****************************************************************
         * Name: Raveenth Maheswaran
         * Title: timer1_click
         * Date: 23/01/2017
         * Input: EventArgs e
         * Output: All the images and strings in the offscreen per tick
         ****************************************************************/
        private void timer1_Tick(object sender, EventArgs e)
        {
            offscreen.Clear(Color.White);

            //starting point of background
            //top left corner
            //starts at 0,0
            Point bpoint = new Point(xcord, ycord);

            //write the image to the offscreengraphics
            offscreen.DrawImage(backBitmap, bpoint);

            //Declaring the color, font and font size
            SolidBrush white = new SolidBrush(Color.White);
            Font myFont = new Font("Times New Roman", 18);

            //Shows quick instructions for the game
            if (elapsedTIme < 250)
            {
                offscreen.DrawString("Press a to move left and d to move right", myFont, white, new Point(190, 270));
                offscreen.DrawString("Press m to shoot the enemies", myFont, white, new Point(240, 290));
            }


            //Display image of shooter
            offscreen.DrawImage(character, new Point(x, y));

            //Entire loop for generating 10 enemies
            for (int k = 0; k < 10; k++)
            {
                //Displays the enemies 
                if (enemy[k] == true)
                {
                    offscreen.DrawImage(enemies[k], enemiesx[k], enemiesy[k] + 80);
                }

                //Displays the 10 other enemies
                if (enemy2[k] == true)
                {
                    offscreen.DrawImage(enemies2[k], enemiesx2[k], enemiesy2[k] + 130);
                }

                //Displays the 10 other enemies
                if (enemy3[k] == true)
                {
                    offscreen.DrawImage(enemies3[k], enemiesx3[k], enemiesy3[k] + 180);
                }

                //Displays the 10 other enemies
                if (enemy4[k] == true)
                {
                    offscreen.DrawImage(enemies4[k], enemiesx4[k] + 10, enemiesy4[k] + 230);
                }

                //The enemies' x and y direction
                enemiesx[k] += directionx;
                enemiesy[k] += directiony;

                enemiesx2[k] += directionx;
                enemiesy2[k] += directiony;

                enemiesx3[k] += directionx;
                enemiesy3[k] += directiony;

                enemiesx4[k] += directionx;
                enemiesy4[k] += directiony;  

                //When the enemies hit the right border it moves down 20 pixels
                if (enemiesx[k] > this.Width - 70  && enemy[k] == true)
                {
                    directionx = 0;
                    directiony = 1;

                    if (enemiesy[k] > DOWN)
                    {
                        directiony = 0;
                        directionx = -1;
                        enemiesx[k] += directionx;
                        DOWN += 20;
                    }

                }

                //When the enemies hit the left border it moves down 20 pixels
                if (enemiesx[k] < 0 && enemy[k] == true)
                {
                    directionx = 0;
                    directiony = 1;

                    if (enemiesy[k] > down2)
                    {
                        directiony = 0;
                        directionx = 1;
                        enemiesx[k] += directionx;
                        down2 += 20;
                    }
                }

                //Shooting code for the main character
                if (missilecounter == 20)
                {
                    missilecounter = 0;
                }
                for (int c = 0; c < 20; c++)
                {
                    if (missileArray[c].showing)
                    {
                        offscreen.DrawImage(shoot, missileArray[c].x2, missileArray[c].y2);
                        missileArray[c].y2--;

                        //Collision code of missile and the first row of enemies
                        if (missileArray[c].y2 < enemiesy[k] + 100 && missileArray[c].x2 > enemiesx[k] && missileArray[c].x2 < enemiesx[k] + 50)
                        {
                            score++;
                            enemy[k] = false;
                            missileArray[c].showing = false;
                        }

                        //Collision code of missile and the second row of enemies
                        else if (missileArray[c].y2 < enemiesy2[k] + 150 && missileArray[c].x2 > enemiesx2[k] && missileArray[c].x2 < enemiesx2[k] + 50)
                        {
                            score++;
                            enemy2[k] = false;
                            missileArray[c].showing = false;
                        }

                        //Collision code of missile and the third row of enemies
                        else if (missileArray[c].y2 < enemiesy3[k] + 200 && missileArray[c].x2 > enemiesx3[k] && missileArray[c].x2 < enemiesx3[k] + 50)
                        {
                            score++;
                            enemy3[k] = false;
                            missileArray[c].showing = false;
                        }

                        //Collision code of missile and the fourth row of enemies
                        else if (missileArray[c].y2 < enemiesy4[k] + 250 && missileArray[c].x2 > enemiesx4[k] && missileArray[c].x2 < enemiesx4[k] + 69)
                        {
                            score++;
                            enemy4[k] = false;
                            missileArray[c].showing = false;
                        }


                        //If the missiles miss the enemies, it disappears and placed back into beginning coordinates
                        else if (missileArray[c].y2 < 4)
                        {
                            missileArray[c].showing = false;
                            missileArray[c].x2 = x + 42;
                            missileArray[c].y2 = y + 10;
                        }

                    }

                    //Missiles initially starts with these coordinates
                    else
                    {
                        missileArray[c].x2 = x + 42;
                        missileArray[c].y2 = y + 10;
                    }

                    //Moves enemies into a random location when the images disappears
                    if (enemy[k] == false)
                    {
                        enemiesx[k] = 1000;
                        enemiesy[k] = 3000;
                    }

                    //Moves enemies into a random location when the images disappears
                    if (enemy2[k] == false)
                    {
                        enemiesx2[k] = 1050;
                        enemiesy2[k] = 2700;
                    }

                    //Moves enemies into a random location when the images disappears
                    if (enemy3[k] == false)
                    {
                        enemiesx3[k] = 1150;
                        enemiesy3[k] = 2800;
                    }

                    //Moves enemies into a random location when the images disappears
                    if (enemy4[k] == false)
                    {
                        enemiesx4[k] = 1150;
                        enemiesy4[k] = 2800;
                    }

                }

                SolidBrush blue = new SolidBrush(Color.Blue); //Selects the color for the score
                offscreen.DrawString("Score: " + score.ToString(), myFont, blue, new Point(10, 10)); //Displays the score at the top left corner
            }

            //Displays message when character shoots all enemies
            if (score == 37)
            {
                offscreen.DrawString("Congratulations! You win!!", myFont, white, new Point(240,250));
            }    

            elapsedTIme++;

            dc.DrawImage(curBitmap, 0, 0);
        }

        /******************************************************
         * Name: Raveenth Maheswaran
         * Title: Form1_KeyPress
         * Date: 23/01/2017
         * Input: KeyPressEventArgs e
         * Output: The character's and missile movement
         *****************************************************/
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //The character moves right when d is pressed
            if (e.KeyChar == 'd')
            {
                x += 10;
            }

            //The character moves left when a is pressed
            if (e.KeyChar == 'a')
            {
                x -= 10;
            }

            //The character shoots missiles when m is pressed
            if (e.KeyChar == 'm')
            {
                missileArray[missilecounter].showing = true;
                missilecounter++;
            }

        }

    }
}

