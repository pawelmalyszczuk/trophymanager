using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrophyManagerNew
{
    public partial class Form1 : Form
    {
        List<PlayerDB> listPlayersFrom;
        List<PlayerDB> listPlayersTo;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ModelTrophy"].ConnectionString;

            using (var context = new ModelTrophy(connectionString))
            {
                // Age
                new ContextBroker().saveSkillInDictionary(1, "age", "Age", "Wiek");

                // ASI
                new ContextBroker().saveSkillInDictionary(2, "asi", "ASI", "ASI");

                // Routine
                new ContextBroker().saveSkillInDictionary(3, "routine", "Routine", "Doświadczenie");

                // Strength
                new ContextBroker().saveSkillInDictionary(4, "str", "Strength", "Siła");

                // Stamina
                new ContextBroker().saveSkillInDictionary(5, "sta", "Stamina", "Wytrzymałość");

                // Pace
                new ContextBroker().saveSkillInDictionary(6, "pac", "Pace", "Szybkość");

                // Marking
                new ContextBroker().saveSkillInDictionary(7, "mar", "Marking", "Krycie");

                // Tacking
                new ContextBroker().saveSkillInDictionary(8, "tac", "Tacking", "Odbiór");

                // Workrate
                new ContextBroker().saveSkillInDictionary(9, "wor", "Workrate", "Pracowitość");

                // Positioning
                new ContextBroker().saveSkillInDictionary(10, "pos", "Positioning", "Ustawianie się");

                // Passing
                new ContextBroker().saveSkillInDictionary(11, "pas", "Passing", "Podania");

                // Crossing
                new ContextBroker().saveSkillInDictionary(12, "cro", "Crossing", "Dośrodkowania");

                // Technique
                new ContextBroker().saveSkillInDictionary(13, "tec", "Technique", "Technika");

                // Heading
                new ContextBroker().saveSkillInDictionary(14, "hea", "Heading", "Główkowanie");

                // Finishing
                new ContextBroker().saveSkillInDictionary(15, "fin", "Finishing", "Wykańczanie");

                // Longshots
                new ContextBroker().saveSkillInDictionary(16, "log", "Longshots", "Strzały z dystansu");

                // Set Pieces
                new ContextBroker().saveSkillInDictionary(17, "set", "Set Pieces", "Stałe fragmenty");

                // Handing
                new ContextBroker().saveSkillInDictionary(18, "han", "Handing", "Łapanie");

                // One-on-ones
                new ContextBroker().saveSkillInDictionary(19, "one", "One-on-ones", "Jeden na jednego");

                // Reflexes
                new ContextBroker().saveSkillInDictionary(20, "ref", "Reflexes", "Refleks");

                // Aerial Ability
                new ContextBroker().saveSkillInDictionary(21, "ari", "Aerial Ability", "Gra w powietrzu");

                // Jumping
                new ContextBroker().saveSkillInDictionary(22, "jum", "Jumping", "Skoczność");

                // Communication
                new ContextBroker().saveSkillInDictionary(23, "com", "Communication", "Komunikacja");

                // Kicking
                new ContextBroker().saveSkillInDictionary(24, "kic", "Kicking", "Wykop");

                // Throwing
                new ContextBroker().saveSkillInDictionary(25, "thr", "Throwing", "Rzucanie");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            listPlayersFrom = getPlayersFromHTML(txtFile.Text);

            if (listPlayersFrom.Any())
            {
                loadDateToGrid(listPlayersFrom);
            }

            /*
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            
            string filePath = "";

            if (txtFile.Text != "")
            {
                filePath = txtFile.Text;

                StreamReader streamReader = new StreamReader(filePath);
                string html = streamReader.ReadToEnd();
                streamReader.Close();

                doc.LoadHtml(html);
                var script = doc.DocumentNode.Descendants()
                                             .Where(n => n.Name == "script")
                                             .First().InnerText;

                string line = doc.DocumentNode.SelectSingleNode("//script[contains(text(), 'players_ar')]").InnerHtml;
                
                int start = line.IndexOf("players_ar") + 14;
                int stop = line.IndexOf("];", line.IndexOf("players_ar")) - 1;

                string playersLine = line.Substring(start, stop - start);

                string[] platersList = playersLine.Split('{');

                if (platersList != null)
                {
                    List<PlayerDB> listPlayers = new List<PlayerDB>();

                    int numPlayer = 0;
                    int countPlayer = platersList.Count();

                    foreach (string playerString in platersList)
                    {
                        numPlayer++;
                        progressBar.Value = (int)((numPlayer * 100) / countPlayer);

                        string[] paramList = playerString.Split(',');

                        if ((paramList != null) && (paramList.Length > 1))
                        {
                            // Get version
                            //Version versionSet = new ContextBroker().getVersionByFileName(Path.GetFileName(filePath));
                            //if (versionSet == null)
                            //{
                            //    // Get week of year
                            //    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                            //    Calendar cal = dfi.Calendar;

                            //    int numerWeekOfYear = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                            //    new ContextBroker().saveVersion(DateTime.Now, Path.GetFileName(filePath), cal.GetYear(DateTime.Now) + "." + numerWeekOfYear);

                            //    versionSet = new ContextBroker().getVersionByFileName(Path.GetFileName(filePath));
                            //}

                            Hashtable paramHash = new Hashtable();

                            foreach (string param in paramList)
                            {
                                if (param.IndexOf(':') > -1)
                                {
                                    string[] tabParam = param.Split(':');

                                    string atribName = tabParam[0].Substring(1, tabParam[0].Length - 2);

                                    string atribValue = tabParam[1].Replace('\"', ' ').Trim().ToString();

                                    paramHash[atribName] = atribValue;
                                }
                            }

                            // Get id player
                            int playerId = int.Parse((string)paramHash["id"]);

                            // Get player from db
                            //Player playerSet = new ContextBroker().getPlayerById(playerId.ToString());
                            //if (playerSet == null)
                            //{
                            //    // Create new player
                            //    new ContextBroker().savePlayer(playerId, (string)paramHash["name"], (string)paramHash["fp"], playerId.ToString());

                            //    playerSet = new ContextBroker().getPlayerById(playerId.ToString());
                            //}

                            // Get list skills from dictionary
                            //var skills = new ContextBroker().getSkillsInDictionary();

                            //foreach (SkillDictionary skillDict in skills)
                            //{
                            //    // Check skill in file
                            //    if (paramHash.ContainsKey(skillDict.SkillCode))
                            //    {
                            //        // Save skill
                            //        new ContextBroker().saveSkill(playerSet.Id, versionSet.Id, skillDict.SkillCode, double.Parse((string)paramHash[skillDict.SkillCode], CultureInfo.InvariantCulture));
                            //    }
                            //}

                            PlayerDB player = new PlayerDB();
                            player.Id = playerId;
                            player.Name = (string)paramHash["name"];
                            player.Position = (string)paramHash["fp"];
                            player.ASI = Double.Parse(((string)paramHash["asi"]).Replace('.', ','));
                            player.Age = Double.Parse(((string)paramHash["age"]).Replace('.', ','));
                            player.Crossing = Double.Parse(((string)paramHash["cro"]).Replace('.', ','));
                            player.Finishing = Double.Parse(((string)paramHash["fin"]).Replace('.', ','));
                            player.Heading = Double.Parse(((string)paramHash["hea"]).Replace('.', ','));
                            player.Longshots = Double.Parse(((string)paramHash["lon"]).Replace('.', ','));
                            player.Marking = Double.Parse(((string)paramHash["mar"]).Replace('.', ','));
                            player.Pace = Double.Parse((string)paramHash["pac"]);
                            player.Passing = Double.Parse(((string)paramHash["pas"]).Replace('.', ','));
                            player.Positioning = Double.Parse(((string)paramHash["pos"]).Replace('.', ','));
                            player.SetPieces = Double.Parse(((string)paramHash["set"]).Replace('.', ','));
                            player.Stamina = Double.Parse(((string)paramHash["sta"]).Replace('.', ','));
                            player.Strength = Double.Parse((string)paramHash["str"]);
                            player.Tacking = Double.Parse(((string)paramHash["tac"]).Replace('.', ','));
                            player.Technique = Double.Parse(((string)paramHash["tec"]).Replace('.', ','));
                            player.Workrate = Double.Parse(((string)paramHash["wor"]).Replace('.', ','));
                            

                            listPlayers.Add(player);

                        }
                    }

                    if (listPlayers.Any())
                    {
                        dgData.DataSource = listPlayers;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please set file *.htm.");
            }
            */

        }

        private void button3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            
            List<PlayerDB> listPlayers = new List<PlayerDB>();

            List<DataGridColors> listColors = new List<DataGridColors>();

            // Get last version
            var version = new ContextBroker().getVersionLast();

            // Get before last version
            var versionBefore = new ContextBroker().getVersionBeforeLast();

            // Get players
            var players = new ContextBroker().getPlayers();

            if (players != null)
            {
                int numPlayer = 0;
                int countPlayer = players.Count();

                foreach (Player player in players)
                {
                    numPlayer++;
                    progressBar.Value = (int)((numPlayer * 100) / countPlayer);

                    PlayerDB playerDB = new PlayerDB();
                    playerDB.Name = player.Name;
                    playerDB.Position = player.PositionCode;
                    playerDB.Code = player.TrophyCode;

                    // Get list skills from dictionary
                    var skills = new ContextBroker().getSkillsInDictionary();
                    
                    bool deletedPlayer = false;

                    foreach (SkillDictionary skillDict in skills)
                    {     
                                       
                        // Get last version skill value
                        var skillValue = new ContextBroker().getSkillValue(player.Id, version.Id, skillDict.SkillCode);

                        if (skillDict.SkillCode.Equals("age"))
                            if (skillValue == 0)
                                deletedPlayer = true;

                        PropertyInfo property = playerDB.GetType().GetProperty(skillDict.SkillName);
                        if (property != null)
                        {
                            property.SetValue(playerDB, skillValue);

                            // Get before last version skill value
                            if (versionBefore != null)
                            {
                                var skillValueBefore = new ContextBroker().getSkillValue(player.Id, versionBefore.Id, skillDict.SkillCode);

                                PropertyInfo propertyBefore = playerDB.GetType().GetProperty(skillDict.SkillName + "Change");
                                if (propertyBefore != null)
                                {
                                    double skillDiv = skillValue - skillValueBefore;
                                    if (skillDiv == 1)
                                        propertyBefore.SetValue(playerDB, "+");
                                    if (skillDiv == -1)
                                        propertyBefore.SetValue(playerDB, "-");
                                }
                            }
                        }

                    }

                    // Add player to result list
                    if (!deletedPlayer)
                        listPlayers.Add(playerDB);
                }
            }

            dgData.DataSource = listPlayers;

            // Painting data
            int rowsCount = dgData.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                var row = dgData.Rows[i];

                int cellsCount = row.Cells.Count;
                for (int j = 0; j < cellsCount; j++)
                {
                    var cellValue = dgData.Rows[i].Cells[j].Value;
                    if (cellValue != null)
                    {
                        if (cellValue.Equals("+"))
                            dgData.Rows[i].Cells[j - 1].Style.BackColor = Color.Green;
                        if (cellValue.Equals("-"))
                            dgData.Rows[i].Cells[j - 1].Style.BackColor = Color.Red;
                    }
                }
            }

            //dgData.Rows[5].Cells[7].Style.BackColor = Color.Red;

            // Columns width
            dgData.Columns[0].Width = 200;
            dgData.Columns[1].Width = 35;
            dgData.Columns[2].Width = 75;
            dgData.Columns[3].Width = 75;
            dgData.Columns[4].Width = 50;
            dgData.Columns[5].Width = 15;
            dgData.Columns[6].Width = 50;
            dgData.Columns[7].Width = 15;
            dgData.Columns[8].Width = 50;
            dgData.Columns[9].Width = 15;
            dgData.Columns[10].Width = 50;
            dgData.Columns[11].Width = 15;
            dgData.Columns[12].Width = 50;
            dgData.Columns[13].Width = 15;
            dgData.Columns[14].Width = 50;
            dgData.Columns[15].Width = 15;
            dgData.Columns[16].Width = 50;
            dgData.Columns[17].Width = 15;
            dgData.Columns[18].Width = 50;
            dgData.Columns[19].Width = 15;
            dgData.Columns[20].Width = 50;
            dgData.Columns[21].Width = 15;
            dgData.Columns[22].Width = 50;
            dgData.Columns[23].Width = 15;
            dgData.Columns[24].Width = 50;
            dgData.Columns[25].Width = 15;
            dgData.Columns[26].Width = 50;
            dgData.Columns[27].Width = 15;
            dgData.Columns[28].Width = 50;
            dgData.Columns[29].Width = 15;
            dgData.Columns[30].Width = 50;
            dgData.Columns[31].Width = 15;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog.FileName;
                try
                {
                    txtFile.Text = file;
                }
                catch (IOException)
                {
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog.FileName;
                try
                {
                    txtFileTo.Text = file;
                }
                catch (IOException)
                {
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listPlayersTo = getPlayersFromHTML(txtFileTo.Text);

            if (listPlayersTo.Any())
            {
                loadDateToGrid(listPlayersTo);
            }
        }

        private List<PlayerDB> getPlayersFromHTML(string file)
        {
            List<PlayerDB> listPlayers = new List<PlayerDB>();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            string filePath = "";

            if (file != "")
            {
                filePath = file;

                StreamReader streamReader = new StreamReader(filePath);
                string html = streamReader.ReadToEnd();
                streamReader.Close();

                doc.LoadHtml(html);
                var script = doc.DocumentNode.Descendants()
                                             .Where(n => n.Name == "script")
                                             .First().InnerText;

                string line = doc.DocumentNode.SelectSingleNode("//script[contains(text(), 'players_ar')]").InnerHtml;

                int start = line.IndexOf("players_ar") + 14;
                int stop = line.IndexOf("];", line.IndexOf("players_ar")) - 1;

                string playersLine = line.Substring(start, stop - start);

                string[] platersList = playersLine.Split('{');

                if (platersList != null)
                {
                    int numPlayer = 0;
                    int countPlayer = platersList.Count();

                    foreach (string playerString in platersList)
                    {
                        numPlayer++;
                        progressBar.Value = (int)((numPlayer * 100) / countPlayer);

                        string[] paramList = playerString.Split(',');

                        if ((paramList != null) && (paramList.Length > 1))
                        {
                            Hashtable paramHash = new Hashtable();

                            foreach (string param in paramList)
                            {
                                if (param.IndexOf(':') > -1)
                                {
                                    string[] tabParam = param.Split(':');
                             
                                    string atribName = tabParam[0].Substring(1, tabParam[0].Length - 2);

                                    string atribValue = tabParam[1].Replace('\"', ' ').Trim().ToString();
                                    if (param.IndexOf("plot") > -1)
                                    {
                                        atribValue = atribValue.Replace("[ ", "").Replace(" ]", "");
                                    }

                                    paramHash[atribName] = atribValue;
                                }
                            }

                            // Get id player
                            int playerId = int.Parse((string)paramHash["id"]);

                            PlayerDB player = new PlayerDB();
                            player.Id = playerId;
                            player.Name = (string)paramHash["name"];
                            player.Position = (string)paramHash["fp"];
                            player.ASI = Double.Parse(((string)paramHash["asi"]).Replace('.', ','));
                            player.Age = Double.Parse(((string)paramHash["age"]).Replace('.', ','));
                            player.Crossing = Double.Parse(((string)paramHash["cro"]).Replace('.', ','));
                            player.Finishing = Double.Parse(((string)paramHash["fin"]).Replace('.', ','));
                            player.Heading = Double.Parse(((string)paramHash["hea"]).Replace('.', ','));
                            player.Longshots = Double.Parse(((string)paramHash["lon"]).Replace('.', ','));
                            player.Marking = Double.Parse(((string)paramHash["mar"]).Replace('.', ','));
                            player.Pace = Double.Parse((string)paramHash["pac"]);
                            player.Passing = Double.Parse(((string)paramHash["pas"]).Replace('.', ','));
                            player.Positioning = Double.Parse(((string)paramHash["pos"]).Replace('.', ','));
                            player.SetPieces = Double.Parse(((string)paramHash["set"]).Replace('.', ','));
                            player.Stamina = Double.Parse(((string)paramHash["sta"]).Replace('.', ','));
                            player.Strength = Double.Parse((string)paramHash["str"]);
                            player.Tacking = Double.Parse(((string)paramHash["tac"]).Replace('.', ','));
                            player.Technique = Double.Parse(((string)paramHash["tec"]).Replace('.', ','));
                            player.Workrate = Double.Parse(((string)paramHash["wor"]).Replace('.', ','));
                            player.IntensivityLastTraning = Int32.Parse(((string)paramHash["plot"]).Replace('.', ','));
                            listPlayers.Add(player);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please set file *.htm.");
            }

            return listPlayers;
        }

        private void btn_compare_Click(object sender, EventArgs e)
        {
            if (listPlayersFrom != null && listPlayersTo != null)
            {
                foreach (PlayerDB player in listPlayersTo)
                {
                    PlayerDB playComp = listPlayersFrom.FirstOrDefault(row => (row.Id == player.Id));
                    if (playComp != null)
                    {
                        // verify atrib
                        if (player.ASI != playComp.ASI)
                        {
                            player.ASIChange = (player.ASI - playComp.ASI);
                        }
                        if (player.Crossing != playComp.Crossing)
                        {
                            player.CrossingChange = (player.Crossing - playComp.Crossing);
                            player.CountChangesAttrib += (int)player.CrossingChange;
                        }
                        if (player.Finishing != playComp.Finishing)
                        {
                            player.FinishingChange = (player.Finishing - playComp.Finishing);
                            player.CountChangesAttrib += (int)player.FinishingChange;
                        }
                        if (player.Heading != playComp.Heading)
                        {
                            player.HeadingChange = (player.Heading - playComp.Heading);
                            player.CountChangesAttrib += (int)player.HeadingChange;
                        }
                        if (player.Longshots != playComp.Longshots)
                        {
                            player.LongshotsChange = (player.Longshots - playComp.Longshots);
                            player.CountChangesAttrib += (int)player.LongshotsChange;
                        }
                        if (player.Marking != playComp.Marking)
                        {
                            player.MarkingChange = (player.Marking - playComp.Marking);
                            player.CountChangesAttrib += (int)player.MarkingChange;
                        }
                        if (player.Pace != playComp.Pace)
                        {
                            player.PaceChange = (player.Pace - playComp.Pace);
                            player.CountChangesAttrib += (int)player.PaceChange;
                        }
                        if (player.Passing != playComp.Passing)
                        {
                            player.PassingChange = (player.Passing - playComp.Passing);
                            player.CountChangesAttrib += (int)player.PassingChange;
                        }
                        if (player.Positioning != playComp.Positioning)
                        {
                            player.PositioningChange = (player.Positioning - playComp.Positioning);
                            player.CountChangesAttrib += (int)player.PositioningChange;
                        }
                        if (player.SetPieces != playComp.SetPieces)
                        {
                            player.SetPiecesChange = (player.SetPieces - playComp.SetPieces);
                            player.CountChangesAttrib += (int)player.SetPiecesChange;
                        }
                        if (player.Stamina != playComp.Stamina)
                        {
                            player.StaminaChange = (player.Stamina - playComp.Stamina);
                            player.CountChangesAttrib += (int)player.StaminaChange;
                        }
                        if (player.Strength != playComp.Strength)
                        {
                            player.StrengthChange = (player.Strength - playComp.Strength);
                            player.CountChangesAttrib += (int)player.StrengthChange;
                        }
                        if (player.Tacking != playComp.Tacking)
                        {
                            player.TackingChange = (player.Tacking - playComp.Tacking);
                            player.CountChangesAttrib += (int)player.TackingChange;
                        }
                        if (player.Technique != playComp.Technique)
                        {
                            player.TechniqueChange = (player.Technique - playComp.Technique);
                            player.CountChangesAttrib += (int)player.TechniqueChange;
                        }
                        if (player.Workrate != playComp.Workrate)
                        {
                            player.WorkrateChange = (player.Workrate - playComp.Workrate);
                            player.CountChangesAttrib += (int)player.WorkrateChange;
                        }
                        
                    }
                }
            }

            if (listPlayersTo.Any())
            {
                var list = listPlayersTo.OrderBy(p => p.ASIChange).Cast<PlayerDB>().ToList();

                loadDateToGrid(list, true);
            }
        }

        private void loadDateToGrid(List<PlayerDB> list, bool colorReflash = false)
        {
            dgData.DataSource = list;
            // Columns width
            dgData.Columns[0].Width = 1;
            dgData.Columns[1].Width = 200;
            dgData.Columns[2].Width = 35;
            dgData.Columns[3].Width = 75;
            dgData.Columns[4].Width = 1;
            dgData.Columns[5].Width = 50;
            dgData.Columns[6].Width = 50;
            dgData.Columns[7].Width = 50;
            dgData.Columns[8].Width = 20;
            dgData.Columns[9].Width = 50;
            dgData.Columns[10].Width = 20;
            dgData.Columns[11].Width = 50;
            dgData.Columns[12].Width = 20;
            dgData.Columns[13].Width = 50;
            dgData.Columns[14].Width = 20;
            dgData.Columns[15].Width = 50;
            dgData.Columns[16].Width = 20;
            dgData.Columns[17].Width = 50;
            dgData.Columns[18].Width = 20;
            dgData.Columns[19].Width = 50;
            dgData.Columns[20].Width = 20;
            dgData.Columns[21].Width = 50;
            dgData.Columns[22].Width = 20;
            dgData.Columns[23].Width = 50;
            dgData.Columns[24].Width = 20;
            dgData.Columns[25].Width = 50;
            dgData.Columns[26].Width = 20;
            dgData.Columns[27].Width = 50;
            dgData.Columns[28].Width = 20;
            dgData.Columns[29].Width = 50;
            dgData.Columns[30].Width = 20;
            dgData.Columns[31].Width = 50;
            dgData.Columns[32].Width = 20;
            dgData.Columns[33].Width = 50;
            dgData.Columns[34].Width = 20;
            dgData.Columns[35].Width = 20;
  
            // Reflesh data
            dgData.Refresh();

            if (colorReflash)
            {
                reloadGridColors(list);
            }
        }

        private void reloadGridColors(List<PlayerDB> list)
        {
            // Color data
            dgData.Rows[0].Cells[0].Style.BackColor = Color.Red;

            int rowNumber = 0;
            int colASIChange = 6;
            int colStrengthChange = 8;
            int colStaminaChange = 10;
            int colPaceChange = 12;
            int colMarkingChange = 14;
            int colTackingChange = 16;
            int colWorkrateChange = 18;
            int colPositioningChange = 20;
            int colPassingChange = 22;
            int colCrossingChange = 24;
            int colTechniqueChange = 26;
            int colHeadingChange = 28;
            int colFinnishingChange = 30;
            int colLongshotsChange = 32;
            int colSetPiecesChange = 34;
            Color changeColor = Color.LightGray;
            
            foreach (PlayerDB player in list)
            {
                dgData.Rows[rowNumber].Cells[0].Style.BackColor = Color.Gray;
                // ASI 
                prepareColorGrid(colASIChange, rowNumber, player.ASIChange, changeColor);

                // Strength
                prepareColorGrid(colStrengthChange, rowNumber, player.StrengthChange, changeColor);

                // Stamina
                prepareColorGrid(colStaminaChange, rowNumber, player.StaminaChange, changeColor);

                // Pace
                prepareColorGrid(colPaceChange, rowNumber, player.PaceChange, changeColor);

                // Marking
                prepareColorGrid(colMarkingChange, rowNumber, player.MarkingChange, changeColor);

                // Tacking
                prepareColorGrid(colTackingChange, rowNumber, player.TackingChange, changeColor);

                // Workrate
                prepareColorGrid(colWorkrateChange, rowNumber, player.WorkrateChange, changeColor);

                // Positioning
                prepareColorGrid(colPositioningChange, rowNumber, player.PositioningChange, changeColor);

                // Passing
                prepareColorGrid(colPassingChange, rowNumber, player.PassingChange, changeColor);

                // Crossing
                prepareColorGrid(colCrossingChange, rowNumber, player.CrossingChange, changeColor);

                // Technique
                prepareColorGrid(colTechniqueChange, rowNumber, player.TechniqueChange, changeColor);

                // Heading
                prepareColorGrid(colHeadingChange, rowNumber, player.HeadingChange, changeColor);

                // Finnishing
                prepareColorGrid(colFinnishingChange, rowNumber, player.FinishingChange, changeColor);

                // Longshots
                prepareColorGrid(colLongshotsChange, rowNumber, player.LongshotsChange, changeColor);

                // SetPieces
                prepareColorGrid(colSetPiecesChange, rowNumber, player.SetPiecesChange, changeColor);

                rowNumber++;
            }

            // Color data
            dgData.Rows[0].Cells[0].Style.BackColor = Color.Green;
        }

        private Color prepareColorAtrib(double value)
        {
            if (value > 0)
                return Color.LawnGreen;
            else if (value < 0)
                return Color.OrangeRed;

            return Color.Transparent;
        }

        private void prepareColorGrid(int numCol, int numRow, double valueAtrib, Color changeColor)
        {
            Color color = prepareColorAtrib(valueAtrib);
            dgData.Rows[numRow].Cells[numCol - 1].Style.BackColor = color;

            if (!color.Equals(Color.Transparent))
                dgData.Rows[numRow].Cells[numCol].Style.BackColor = changeColor;
        }
    }
}
