using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Variablen
        static Boolean help = false;
        static int varLoad = 2;
        static int varCounter = 0;
        static int varPoints = 0;
        static int varSkillLevel = 1; //Schwierigkeitsgrad

        static int[,] varPlayerNo = new int[2, 2]; //Spielerpunkte + Fehler
        static string varPlayerTurn = "1"; // Welcher Spieler an der Reihe ist
        static int varPlayers = 2; //Anzahl Spieler

        static string varPuffer;
        static string varCurrentClick;
        static string varBeforeClick;

        ImageButton[] buttonArray = new ImageButton[28]; //ButtonArry erstellen (26 Stück)
        Button[] menuebutton = new Button[2];

        static string[] randomString = new string[28];
        static int[] random1 = new int[28]; //Erster Array
        static int[] random2 = new int[28]; //Zweiter Array

        protected void Page_Load(object sender, EventArgs e)
        {
            //divOverlay.Visible = false;

            this.Title = "Memory by JanSch";
            if (varLoad == 0) //Legt fest welche "Ansicht" beim jeweiligen Neuladen der Seite angzeigt wird
            {
                CreatingOrder();
                CreatingButtons();
            }
            else if (varLoad == 1)
            {
                CreatingButtons();
                if (varSkillLevel == 1 && help == true)
                {
                    overLabel.Text = "Wer ist auf dem Bild zu sehen?";
                    divOverlay.Visible = true;
                    skill2();
                }
            }
            else
            {
                CreatingMenue();
            }
        }

        private void CreatingMenue() //Menü mit: Spielerauswahl (1-2) FEHLER
        {
            Textline1.Text = "Herzlich Willkommen";
            Textline2.Text = "Bitte wählen Sie Anzahl der Spieler:";
            int n = 1;
            
            for (int m = 0; m < menuebutton.Length; m++)
            {
                menuebutton[m] = new Button();
                menuebutton[m].Width = 100;
                menuebutton[m].Height = 100;
                menuebutton[m].Click += new EventHandler(Mbutton_Click); // HIER ist das Problem, trotz Zuweisung des EventHandlers wird dieser beim Klicken nicht ausgeführt
                menuebutton[m].Click += this.Mbutton_Click;
                menuebutton[m].ID = n.ToString();
                menuebutton[m].Text = n.ToString();
                this.myPanel.Controls.Add(menuebutton[m]);
               
                n++;
            }
            
        }

        private void CreatingOrder() //Hier wird die Reihenfolge der Memory Karten festgelegt
        {
            Textline1.Text = "";
            Textline2.Text = "";
            int i = 0;
            Random random = new Random();
            int randomInhalt = random.Next(buttonArray.Length / 2); //Zufallszahl

            //HIER gibt es noch den Fehler, dass er eine Karte (die mit ID 0) nur einmal anzeigt und eine beliebige andere dafür dreimal
            for (int z = 0; z < buttonArray.Length; z++)
            {
                randomInhalt = random.Next(buttonArray.Length / 2);

                for (int x = 0; x < random1.Length / 2; x++) //Hier geht er den ersten Array durch und prüft ob die Zufallszahl schon dadrin gespeichert ist
                {
                    if (random1[x] == randomInhalt) //Ist sie dadrin gespeichert erstellt er eine neue Zufallszahl und fängt wieder bei 0 an.
                    {
                        randomInhalt = random.Next(buttonArray.Length / 2);
                        x = 0;
                    }
                }

                randomString[z] = randomInhalt.ToString();

                for (int y = 0; y < random2.Length; y++) //Hier prüfe ich ob die Zufallszahl schon im zweiten Array gespeicherrt ist
                {
                    if (random2[y] == randomInhalt) //Wenn dem so ist schreibe ich diese Zahl in den ersten Array (weil ich brauche jede Karte zweimal)
                    {
                        random1[i] = randomInhalt;
                        i++;
                        y = random2.Length;
                    }
                }

                random2[z] = randomInhalt; //Hier schreibe ich die Zufallszahl in den zweiten Array
            }
            varLoad = 1;
        }

        private void CreatingButtons() //Hier werden die Karten anhand der zuvor festgelegten Zufallsreihe erzeugt
        {
            
            for (int z = 0; z < buttonArray.Length;z++)
            {
                //Ab hier wird der Button mit seinen Eigenschaften erstellt
                buttonArray[z] = new ImageButton();
                buttonArray[z].Width = 100;
                buttonArray[z].Height = 100;
                buttonArray[z].ForeColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                buttonArray[z].BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                buttonArray[z].ImageUrl = "pics/back.png";
                buttonArray[z].Click += new ImageClickEventHandler(buttonArray_Click);
                buttonArray[z].ID = z.ToString();
                buttonArray[z].AlternateText = randomString[z]; //Den AlternativText nutze ich später zum vergleichen

                this.myPanel.Controls.Add(buttonArray[z]);
            }
            Textline1.Text = "Am Zug:";
            Textline2.Text = "Spieler " + varPlayerTurn;

            if(varPlayers == 1) // Je nach Anzahl der Spieler werden wie Zählerfelder anders beschrieben
            {
                Points00.Visible = true;
                Points01.Visible = true;
                lblPoints00.Visible = true;
                lblPoints01.Visible = true;

                Points10.Visible = false;
                Points11.Visible = false;
                lblPoints10.Visible = false;
                lblPoints11.Visible = false;
            }
            else
            {
                Points00.Visible = true;
                Points01.Visible = true;
                lblPoints00.Visible = true;
                lblPoints01.Visible = true;

                Points10.Visible = true;
                Points11.Visible = true;
                lblPoints10.Visible = true;
                lblPoints11.Visible = true;
            }
            this.reset.Visible = true;
        } //

        private void Punktevergabe() //Einfache Funktion, die nur die Verteilung der Punkte regelt
        {
            if (varPlayerTurn == "1")
            {
                varPlayerNo[0, 0]++;
                lblPoints00.Text = varPlayerNo[0, 0].ToString();
            }
            else
            {
                varPlayerNo[1, 0]++;
                lblPoints10.Text = varPlayerNo[1, 0].ToString();
            }      
        }

        private void Errorvergabe() //Einfache Funktion, die nur die Verteilung der Fehlerpunkte regelt
        {
            if (varPlayerTurn == "1")
            {
                varPlayerNo[0, 1]++;
                lblPoints01.Text = varPlayerNo[0, 1].ToString();
            }
            else
            {
                varPlayerNo[1, 1]++;
                lblPoints11.Text = varPlayerNo[1, 1].ToString();
            }
        } 

        private void CheckWinner() // Prüft die Anzahl der Punkte um den Sieger zu bestimmen. HAT NOCH BEKANNTE FEHLER
        {
            if (varPlayers == 2)
            {
                if (varPoints == buttonArray.Length/2)
                {
                    varLoad = 0;
                    lblMessage2.Text = "Spieler " + varPlayerTurn + " gewinnt. Herzlichen Glückunsch";
                }
            }
            else
            {
                if (varPoints == buttonArray.Length)
                {
                    varLoad = 0;
                    lblMessage2.Text = "Spieler 1 gewinnt. Herzlichen Glückunsch";
                }
            }
        }

        private void ChangePlayer() // Ändert im Multiplayer den Spieler, welcher an der Reihe ist.
         {
            if (varPlayers == 2) //Spielerwechsel
            {
                if (varPlayerTurn == "1")
                    varPlayerTurn = "2";
                else
                    varPlayerTurn = "1";
            }
            else
            {

            }
        }

        private void RotateCard() //Dreht die gewählte Karte um und disabled sie
        {
            buttonArray[Convert.ToInt32(varCurrentClick)].ImageUrl = "pics/" + buttonArray[Convert.ToInt32(varCurrentClick)].AlternateText + ".png";
            buttonArray[Convert.ToInt32(varCurrentClick)].Enabled = false;
        }

        protected void Timer1_Tick(object sender, EventArgs e) //Wird genutzt um die zweite aufgedeckte Karte dem Spieler kurz zu zeigen
        {
            Timer1.Enabled = false;
            buttonArray[Convert.ToInt32(varBeforeClick)].Enabled = true;
            buttonArray[Convert.ToInt32(varCurrentClick)].Enabled = true;
            buttonArray[Convert.ToInt32(varBeforeClick)].ImageUrl = "pics/back.png";
            buttonArray[Convert.ToInt32(varCurrentClick)].ImageUrl = "pics/back.png";
            this.myPanel.Enabled = true;
            divOverlay.Visible = false;
            overLabel.Text = "";
        }

        void Mbutton_Click(object sender, EventArgs e) // Diese Funktion sollen die menuebuttons aufrufen, doch das passiert nicht. FEHLER
        {
            varLoad = 0;

            var current = sender as Button;
            if (current.ID == "1")
            {
                varPlayers = 1;
            }
            else
            {
                varPlayers = 2;
            }
        }

        protected void ResetBtn_Click(object sender, EventArgs e) // Zurücksetzen / Reset - Alles auf Anfang
        {
            varLoad = 2;
            int y = 0;

            for (int z = 0; z < buttonArray.Length; z++) //Alle Karten werden wieder "umgedreht" und Variablen geleert
            {
                buttonArray[z].Enabled = true;
                buttonArray[z].ImageUrl = "pics/back.png";
                Array.Clear(random1, 0, random1.Length);
                Array.Clear(random2, 0, random2.Length);
                Array.Clear(randomString, 0, randomString.Length);
            }

            for (int p = 0; p == 1; p++)
            {
                varPlayerNo[p, y] = 0;

                for (; y == 1; y++)
                {
                    varPlayerNo[p, y] = 0;
                }
            }

            lblPoints00.Text = varPlayerNo[0, 0].ToString();
            lblPoints01.Text = varPlayerNo[0, 1].ToString();
            lblPoints10.Text = varPlayerNo[1, 0].ToString();
            lblPoints11.Text = varPlayerNo[1, 1].ToString();
            
        }

        void buttonArray_Click(object sender, ImageClickEventArgs e) // Hauptfunktion, das passiert alles wenn auf eine der Memory Karten geklickt wird
        {
            var current = sender as ImageButton;
            varCurrentClick = current.ID;
            
            if (varCounter == 0) // Wenn 0 dann ist es die erste aufgedeckte Karte
            {
                varCounter = 1;
                varPuffer = buttonArray[Convert.ToInt32(varCurrentClick)].AlternateText;
                varBeforeClick = current.ID; //Die ID der ersten Karte zwischenspeichern
                RotateCard();
            }
            else // Sonst ist es die zweite Karte
            {

                if (varPuffer == buttonArray[Convert.ToInt32(varCurrentClick)].AlternateText) //Wenn die beiden Karten gleich sind
                {
                    varCounter = 0;
                    RotateCard();

                    if (varSkillLevel == 0)
                    {
                        //Schwierigkeitsgrad 1 - Spieler bekommt bei Pärchen Punkte
                        Punktevergabe();
                    }
                    else if (varSkillLevel == 1)
                    {
                        //Schwierigkeitsgrad 2 - Spieler muss noch den Namen erraten
                        help = true;
                    }
                    else
                    {
                        //Schwierigkeitsgrad 3 - Spieler muss noch den Namen eintippen
                        TextBox txtName = new TextBox();
                        txtName.ID = "name";
                        txtName.Text = "";
                        txtName.ForeColor = System.Drawing.Color.LightGray;
                        txtName.Width = 400;
                        txtName.Height = 50;
                        this.overPanelBottom.Controls.Add(txtName);

                        Button btnOK = new Button();
                        btnOK.Text = "OK";
                        btnOK.Width = 50;
                        btnOK.Height = 50;
                        btnOK.ID = "ok";
                        this.overPanelBottom.Controls.Add(btnOK);

                        overLabel.Text = "Wer ist auf dem Bild zu sehen?";
                        divOverlay.Visible = true;
                        Punktevergabe();
                    }

                    CheckWinner();

                }
                else //Wenn unterschiedliche Karten gewählt wurden
                {
                    Timer1.Enabled = true;
                    divOverlay.Visible = true;
                    overLabel.Text = "Leider falsch";
                    this.myPanel.Enabled = false;
                    varCounter = 0;
                    RotateCard();
                    Errorvergabe();
                    ChangePlayer();
                }

                //SIEG
                //divOverlay.Visible = false;
            }
        }

        void skill2()
        {
            int n = Convert.ToInt32(buttonArray[Convert.ToInt32(varCurrentClick)].AlternateText);
            Button[] skill2button = new Button[4];
            for (int m = 0; m < skill2button.Length; m++)
            {
                skill2button[m] = new Button();
                skill2button[m].Width = 100;
                skill2button[m].Height = 100;
                skill2button[m].ID = n.ToString();
                skill2button[m].Text = n.ToString();
                skill2button[m].Click += skill2button_Click;
                this.overPanelBottom.Controls.Add(skill2button[m]);
                n++;
            }
            
        }

        void skill2button_Click(object sender, EventArgs e) // Die Buttons für Skill 2
        {
            var current = sender as ImageButton;

            if(current.AlternateText == varCurrentClick)
            {
                Punktevergabe();
                divOverlay.Visible = false;
            }
            else
            {
                Timer1.Enabled = true;
                divOverlay.Visible = true;
                overLabel.Text = "Leider falsch";
                this.myPanel.Enabled = false;
                varCounter = 0;
                RotateCard();
                Errorvergabe();
                ChangePlayer();
            }

        }

    }
}