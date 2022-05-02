using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SpiderX

    /*SpiderX big Wordlist generator and tools 
     * SpiderX License by DevoneSoft
     * creadted by - yasincan olcay
     * ©Copyright devonesoft 2022
     * @olcayyasincan@gmail.com
     */
{
    public partial class Form1 : Form
    {
        //VARIABLES
        //
        //LIST SELECTED BOOL VARIABLES FOR CONTROL
        bool secildi = false;
        bool secildi2 = false;

        //components class
        Components componentss = new Components();
        start_generate startGenerate = new start_generate();

        //------------------------------------------------//
        //special word string list

        List<string> takim = new List<string>();
        //ALPHABE SMALL
        List<string> fullname = new List<string>();
        //ALPHABE Big
        List<string> renkler = new List<string>();
        //Kelimelistesi LİST
        List<string> kelimeler = new List<string>();
       
        //File name
        public string dosyaIsmi;
        //kelimelistesi uzunluk
        public int kelimeListesiUzunlugu;
        public int kelimeListesiIndex = 0;
        int alphabeIndex = 0;
        int sayilarIndex = 0;
        int sozlukIndex = 0;
        public int kelimeadet = 0;
        //hız modu kontrolu
        bool quickMode = false;
        public int speed = 80;
        //----------------------------------------------//
        bool kelimelerEklendimi = false;
        bool kelimelerBirlestimi = false;
        bool ozelKarakterYapildimi = false;
        bool sayilarYapildimi = false;
        bool sozlukYapildimi = false;

        bool upper = false;
        bool lower = false;

        List<String> dic = new List<string>();

        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            Thread addDictonary = new Thread(new ThreadStart(AddDictonary));
            addDictonary.Start();
            //add components function
            componentss.Main();
        }
        void AddDictonary()
        {
            string dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
            FileStream fss = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fss);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                yazi = sw.ReadLine();
                dic.Add(yazi);
            }
            sw.Close();
            fss.Close();
        }
        //kelimeboxtextbox empty control
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            if(kelimeBox.Text != "")
            {
                kelimeListesi.Items.Add(kelimeBox.Text);
                kelimeler.Add(kelimeBox.Text);
                KelimeLİstesiUzunluk.Text = kelimeListesi.Items.Count.ToString();
                kelimeBox.Clear();
            }
        }

        //kelimebox key enter for kelimelistesi list add item
        private void kelimeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                if(kelimeBox.Text != "")
                {
                    kelimeListesi.Items.Add(kelimeBox.Text);
                    kelimeler.Add(kelimeBox.Text);
                    SendKeys.Send("{BACKSPACE}");
                    kelimeBox.Clear();
                    KelimeLİstesiUzunluk.Text = kelimeListesi.Items.Count.ToString();
                }
                SendKeys.Send("{BACKSPACE}");
            }
        }

        //parcala information messagebox guna2
        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
            guna2MessageDialog1.Text = "Bu özellik açık olduğunda, tüm işlemler bittikten sonra\nalfabetik sıraya göre, tüm kelimeler için wordlist\noluşturur";
            guna2MessageDialog1.Show();
        }

        //kelimelistesi list remove at selected item
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (secildi == true)
                {
                    kelimeListesi.Items.RemoveAt(kelimeListesi.SelectedIndex);
                    kelimeler.Clear();
                    secildi = false;
                    foreach (string item in kelimeListesi.Items)
                    {
                        kelimeler.Add(item);
                    }
                }
            }
            catch
            {}
        }

        private void kelimeListesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            secildi = true;
        }

        //*************************************
        //SpiderX ToolStripMenu functions
        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/yasincanolcay");
        }
        //*************************************
        //

        //clear Spiderx - all items remove
        private void AllClearButton_Click(object sender, EventArgs e)
        {
            kelimeListesi.Items.Clear();
            FootballTeamTextbox.Clear();
            FullNameTextbox.Clear();
            ColorTextBox.Clear();
            FileNameTextbox.Clear();
            specialWord.Items.Clear();
            minSizeTextbox.Clear();
            maxSizeTextbox.Clear();
            ozelKarakterCheck.Checked = false;
            sayilarCheck.Checked = false;
            sozlukCheck.Checked = false;
            spaceCheck.Checked = false;
            wordlistCheck.Checked = false;
            cityCheck.Checked = false;
            yavasModToogle.Checked = false;
            parcalaToggle.Checked = true;
            uzunlukToggle.Checked = true;
            KelimeLİstesiUzunluk.Text = kelimeListesi.Items.Count.ToString();
            specialUzunluk.Text = specialWord.Items.Count.ToString();
            fullname.Clear();
            takim.Clear();
            renkler.Clear();
            dosyaIsmi = "";
            kelimeler.Clear();
        }

        //uzunluk toggle
        private void uzunlukToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (uzunlukToggle.Checked == true)
            {
                minSizeTextbox.Enabled = true;
                maxSizeTextbox.Enabled = true;
            }
            else
            {
                minSizeTextbox.Enabled = false;
                maxSizeTextbox.Enabled = false;
            }
        }

        //parcala toggle
        private void parcalaToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (parcalaToggle.Checked == true)
            {
                parcalaLabel.Text = "aktif";
            }
            else
            {
                parcalaLabel.Text = "kapalı";
            }
        }
        //Filename add button - guna2 tilebutton
        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            //dosya ismi
            dosyaIsmi = "wordlists/"+FileNameTextbox.Text+".txt";
        }

        //SPECİALWORD ADD İTEMS BUTTON HOVER FUNCTİONS
        //********************************************************************
        private void FootballTeamTextbox_Enter(object sender, EventArgs e)
        {
            Addbutton1.Visible = true;
            Addbutton2.Visible = false;
            Addbutton3.Visible = false;
            Addbutton4.Visible = false;
        }

        private void FullNameTextbox_Enter(object sender, EventArgs e)
        {
            Addbutton1.Visible = false;
            Addbutton2.Visible = true;
            Addbutton3.Visible = false;
            Addbutton4.Visible = false;
        }

        private void ColorTextBox_Enter(object sender, EventArgs e)
        {
            Addbutton1.Visible = false;
            Addbutton2.Visible = false;
            Addbutton3.Visible = true;
            Addbutton4.Visible = false;
        }

        private void FileNameTextbox_Enter(object sender, EventArgs e)
        {
            Addbutton1.Visible = false;
            Addbutton2.Visible = false;
            Addbutton3.Visible = false;
            Addbutton4.Visible = true;
        }
        //*************************************************************//
        //
         
        //specialword list-footboolteam list add item
        private void Addbutton1_Click(object sender, EventArgs e)
        {
            if (FootballTeamTextbox.Text != "")
            {
                specialWord.Items.Add(FootballTeamTextbox.Text);
                takim.Add(FootballTeamTextbox.Text);
                specialUzunluk.Text = specialWord.Items.Count.ToString();
                FootballTeamTextbox.Clear();
            }
        }
        //specialword list-fullname list add item
        private void Addbutton2_Click(object sender, EventArgs e)
        {
            if(FullNameTextbox.Text != "")
            {
                specialWord.Items.Add(FullNameTextbox.Text);
                fullname.Add(FullNameTextbox.Text);
                specialUzunluk.Text = specialWord.Items.Count.ToString();
                FullNameTextbox.Clear();
            }
        }
        //specialword list-color list add item
        private void Addbutton3_Click(object sender, EventArgs e)
        {
            if(ColorTextBox.Text != "")
            {
                specialWord.Items.Add(ColorTextBox.Text);
                renkler.Add(ColorTextBox.Text);
                specialUzunluk.Text = specialWord.Items.Count.ToString();
                ColorTextBox.Clear();
            }
        }

        //Kelimelistesi list alphabethic sorted
        private void sıralaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kelimeListesi.Sorted = true;
            kelimeler.Sort();
        }

        //file name and locations shower messagebox guns2
        private void guna2GradientPanel1_Click(object sender, EventArgs e)
        {
            yol.Text = YolLabel.Text;
            yol.Show();
        }
        //file name and locations label
        private void YolLabel_Click(object sender, EventArgs e)
        {
            yol.Text = YolLabel.Text;
            yol.Show();
        }
        //
        //Specialword list selected item delete
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (secildi2 == true)
                {
                    takim.Remove(specialWord.Text);
                    fullname.Remove(specialWord.Text);
                    renkler.Remove(specialWord.Text);
                    specialWord.Items.RemoveAt(specialWord.SelectedIndex);
                }
            }
            catch
            { }
        }

        private void specialWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            secildi2 = true;
        }
        //specialword list alphabethic sorted
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            specialWord.Sorted = true;
            takim.Sort();
        }

        /*************************************************
         * 
         * Wordlist generate start button function
         * 
         *************************************************/
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (dosyaIsmi != "" && FileNameTextbox.Text != "" && kelimeListesi.Items.Count != 0)
            {
                label14.Text = "Yazıldı";
                kelimeLabel.Text = "";
                kelimeSayiLabel.Text = "";
                gucDurumu.Text = "";
                gucProgress.Value = 3;
                kelimeListesiUzunlugu = kelimeler.Count;
                label13.Text = "Bekleniyor";
                label13.ForeColor = Color.FromArgb(20, 70, 150);
                kalanProgress.Value = 90;
                taskCompletedProggress.Value = 0;
                kalanProgress.Animated = true;
                dosyaIsmi = "wordlists/"+FileNameTextbox.Text+".txt";
                toplamSure.Enabled = true;
                toplamSure.Start();
                StartButton.Enabled = false;
                AllClearButton.Enabled = false;
                KaristirBtn.Enabled = false;
                kalanProgress.UseWaitCursor = true;
                KelimeRadialGuerge.UseWaitCursor = true;
                label7.UseWaitCursor = true;
                ustBilgiLabel.Text = "İşleniyor";
                ustLoadingBar.Visible = true;
                ustLoadingBar.Start();
            }
            Thread generate = new Thread(new ThreadStart(generateList));
            generate.Start();
        }
        
        private void generateListItem()
        {
            if (kelimeListesiIndex < kelimeListesiUzunlugu-1)
            {
                kelimeListesiIndex++;
                taskCompletedProggress.Value += 5;
                ustBilgiLabel.Text = "Yazılıyor";
                Thread generate2 = new Thread(new ThreadStart(generateList));
                generate2.Start();
            }
            else
            {
                kelimeListesiUzunlugu = kelimeler.Count;
                kelimeListesiIndex = 0;
                if (ozelKarakterCheck.Checked == true && ozelKarakterYapildimi==false)
                {
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndAlphabeGenerate));
                    generate2.Start();
                }
                else if (sayilarCheck.Checked == true && sayilarYapildimi == false && ozelKarakterCheck.Checked == true)
                {
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndNumberGenerate));
                    generate2.Start();
                }
                else if(sozlukCheck.Checked == true && sozlukYapildimi == false)
                {
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndDictonaryGenerate));
                    generate2.Start();
                }
                else
                {
                    taskCompletedProggress.Value = 100;
                    label13.ForeColor = Color.FromArgb(20, 200, 150);
                    bekleniyorIconHidePanel.Visible = false;
                    label13.Text = "Başarılı";
                    kalanProgress.Animated = false;
                    kalanProgress.Value = 100;
                    toplamSure.Enabled = false;
                    taskCompletedProggress.Value = 100;
                    StartButton.Enabled = true;
                    AllClearButton.Enabled = true;
                    KaristirBtn.Enabled = true;
                    sonucLabel.Text = "100%";
                    KelimeRadialGuerge.UseWaitCursor = false;
                    kalanProgress.UseWaitCursor = false;
                    label7.UseWaitCursor = false;
                    ustBilgiLabel.Text = "Bitti";
                    ustLoadingBar.Visible = false;
                    ustLoadingBar.Stop();
                }
            }
        }
        public void generateList()
        {
            if (FileNameTextbox.Text != "" && dosyaIsmi!="" && kelimeListesi.Items.Count != 0)
            {
                FileStream fs = new FileStream(dosyaIsmi, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();
                ustBilgiLabel.Text = "Alfabe ekleniyor";

                for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                //------------------------------------------------//
                var kelimeChar = kelimeListesi.Items[kelimeListesiIndex].ToString().ToCharArray();
                String caseWord = "";
                ustBilgiLabel.Text = "Kelimeler parçalanıyor";
                for (int i = 0; i < kelimeChar.Length; i++)
                {
                    Thread.Sleep(speed);
                    caseWord += kelimeChar[i].ToString().ToUpper();
                    caseWord += kelimeChar[i].ToString().ToLower();
                }
                ustBilgiLabel.Text = "Kelimeler ekleniyor";
                for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, caseWord + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, caseWord + componentss.alphabeBig[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + caseWord + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + caseWord;
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord;
                kelimeSayiLabel.Text = kelimeadet.ToString();
                caseWord = "";
                Thread.Sleep(speed);
                ustBilgiLabel.Text = "Kelimeler parçalanıyor";
                for (int i = 0; i < kelimeChar.Length; i++)
                {
                    Thread.Sleep(speed);
                    caseWord += kelimeChar[i].ToString().ToUpperInvariant();
                }
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord;
                kelimeSayiLabel.Text = kelimeadet.ToString();
                caseWord = "";
                Thread.Sleep(speed);
                for (int i = 0; i < kelimeChar.Length; i++)
                {
                    Thread.Sleep(speed);
                    caseWord += kelimeChar[i].ToString().ToLowerInvariant();
                }
                ustBilgiLabel.Text = "Kelimeler ekleniyor...";
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord;
                kelimeSayiLabel.Text = kelimeadet.ToString();
                Thread.Sleep(speed);
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + caseWord + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + caseWord;
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                caseWord = "";
                Thread.Sleep(speed);
                upper = true;
                for (int i = 0; i < kelimeChar.Length; i++)
                {
                    Thread.Sleep(speed);
                    if (upper == true)
                    {
                        caseWord += kelimeChar[i].ToString().ToUpper();
                        upper = false;
                    }
                    else
                    {
                        caseWord += kelimeChar[i].ToString().ToLower();
                        upper = true;

                    }
                }
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord;
                kelimeSayiLabel.Text = kelimeadet.ToString();
                caseWord = "";
                Thread.Sleep(speed);
                lower = true;
                for (int i = 0; i < kelimeChar.Length; i++)
                {
                    Thread.Sleep(speed);
                    if (lower == true)
                    {
                        caseWord += kelimeChar[i].ToString().ToLower();
                        lower = false;
                    }
                    else
                    {
                        caseWord += kelimeChar[i].ToString().ToUpper();
                        lower = true;

                    }
                }
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord;
                kelimeSayiLabel.Text = kelimeadet.ToString();
                Thread.Sleep(speed);
                ustBilgiLabel.Text = "İşlemler devam ediyor...";
                //------------------------------------------//
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                ustBilgiLabel.Text = "Alfabe ekleniyor";
                for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                //1
                if(kelimelerEklendimi == false)
                {
                    ustBilgiLabel.Text = "Kelimeler ekleniyor";
                    for (int item = 0; item < kelimeListesi.Items.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }

                    if (spaceCheck.Checked == true)
                    {
                        ustBilgiLabel.Text = "Boşluklar uygulanıyor";
                        for (int item = 0; item < kelimeListesi.Items.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, " " + kelimeListesi.Items[item] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();

                        }
                    }
                    kelimelerEklendimi = true;
                    Thread.Sleep(speed);
                }
                //--------------------------------------------------------//
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                ustBilgiLabel.Text = "İşlemler devam ediyor";
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[item] + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                //2
                //----------------------------------------------------------//
                if(kelimelerBirlestimi == false)
                {
                    ustBilgiLabel.Text = "Kelimeler birleştiriliyor";
                    for (int item = 0; item < kelimeListesi.Items.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < kelimeListesi.Items.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < kelimeListesi.Items.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < kelimeListesi.Items.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString() + kelimeListesi.Items[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    //----------------------------------------//
                    if (spaceCheck.Checked == true)
                        ustBilgiLabel.Text = "Boşluklar uygulanıyor";
                    {
                        for (int item = 0; item < kelimeListesi.Items.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < kelimeListesi.Items.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " +  kelimeListesi.Items[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < kelimeListesi.Items.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < kelimeListesi.Items.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString() + " " + kelimeListesi.Items[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                    }
                    kelimelerBirlestimi = true;
                    Thread.Sleep(speed);
                    //-----------------------------------------------//
                }

                //----------------------------------------------//
                ustBilgiLabel.Text = "Alfabe ekleniyor";
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeBig[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeBig[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                //--------------------------------------------------//
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                for (int item = 0; item < componentss.alphabeBig.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeBig[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();

                }
                //3
                //---------------------------------------------------//
                if (ozelKarakterCheck.Checked == true)
                {
                    ustBilgiLabel.Text = "karakter ekleniyor";
                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }

                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }

                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, caseWord + componentss.specialCharacters[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = caseWord + componentss.specialCharacters[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }

                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + caseWord+ "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.specialCharacters[item] + caseWord;
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    //-----
                    upper = true;
                    caseWord = "";
                    for (int i = 0; i < kelimeChar.Length; i++)
                    {
                        Thread.Sleep(speed);
                        if (upper == true)
                        {
                            caseWord += kelimeChar[i].ToString().ToUpper();
                            upper = false;
                        }
                        else
                        {
                            caseWord += kelimeChar[i].ToString().ToLower();
                            upper = true;

                        }
                    }

                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, caseWord + componentss.specialCharacters[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = caseWord + componentss.specialCharacters[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }

                    for (int item = 0; item < componentss.specialCharacters.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + caseWord + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.specialCharacters[item] + caseWord;
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    if (spaceCheck.Checked == true)
                    {
                        caseWord = "";
                        for (int i = 0; i < kelimeChar.Length; i++)
                        {
                            Thread.Sleep(speed);
                            if (upper == true)
                            {
                                caseWord += kelimeChar[i].ToString().ToUpper();
                                upper = false;
                            }
                            else
                            {
                                caseWord += kelimeChar[i].ToString().ToLower();
                                upper = true;

                            }
                        }
                        ustBilgiLabel.Text = "Boşluklar uygulanıyor";
                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }

                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex];
                            kelimeSayiLabel.Text = kelimeadet.ToString();

                        }

                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, caseWord + " " + componentss.specialCharacters[item] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = caseWord + componentss.specialCharacters[item];
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }

                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + caseWord + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.specialCharacters[item] + caseWord;
                            kelimeSayiLabel.Text = kelimeadet.ToString();

                        }
                        lower = true;
                        caseWord = "";
                        for (int i = 0; i < kelimeChar.Length; i++)
                        {
                            Thread.Sleep(speed);
                            if (lower == true)
                            {
                                caseWord += kelimeChar[i].ToString().ToLower();
                                lower = false;
                            }
                            else
                            {
                                caseWord += kelimeChar[i].ToString().ToUpper();
                                lower = true;

                            }
                        }
                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, caseWord + " " + componentss.specialCharacters[item] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = caseWord + componentss.specialCharacters[item];
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }

                        for (int item = 0; item < componentss.specialCharacters.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + caseWord + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.specialCharacters[item] + caseWord;
                            kelimeSayiLabel.Text = kelimeadet.ToString();

                        }
                    }
                }
                
                if (sayilarCheck.Checked == true)
                {
                    caseWord = "";
                    lower = true;
                    for (int i = 0; i < kelimeChar.Length; i++)
                    {
                        Thread.Sleep(speed);
                        if (lower == true)
                        {
                            caseWord += kelimeChar[i].ToString().ToLower();
                            lower = false;
                        }
                        else
                        {
                            caseWord += kelimeChar[i].ToString().ToUpper();
                            lower = true;

                        }
                    }
                    ustBilgiLabel.Text = "sayılar ekleniyor";
                    //--------------------------------------------//
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + kelimeListesi.Items[kelimeListesiIndex];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    //-------------lowerCase
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, caseWord + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = caseWord + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + caseWord + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + caseWord;
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + caseWord + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + caseWord + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    caseWord = "";
                    upper = true;
                    for (int i = 0; i < kelimeChar.Length; i++)
                    {
                        Thread.Sleep(speed);
                        if (upper == true)
                        {
                            caseWord += kelimeChar[i].ToString().ToUpper();
                            upper = false;
                        }
                        else
                        {
                            caseWord += kelimeChar[i].ToString().ToLower();
                            upper = true;

                        }
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, caseWord + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = caseWord + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + caseWord + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + caseWord;
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.sayilar.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + caseWord + componentss.sayilar[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.sayilar[item].ToString() + caseWord + componentss.sayilar[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    //----------//
                    if (spaceCheck.Checked == true)
                    {
                        ustBilgiLabel.Text = "Boşluklar uygulanıyor";
                        //--------------------------------------------//
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex];
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        //-------------------------------------//
                        //uppercase
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, caseWord + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = caseWord + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + caseWord + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + caseWord;
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + caseWord + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + caseWord + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        lower = true;
                        caseWord = "";
                        for (int i = 0; i < kelimeChar.Length; i++)
                        {
                            Thread.Sleep(speed);
                            if (lower == true)
                            {
                                caseWord += kelimeChar[i].ToString().ToLower();
                                lower = false;
                            }
                            else
                            {
                                caseWord += kelimeChar[i].ToString().ToUpper();
                                lower = true;

                            }
                        }
                        //-------------------------------------//
                        //lowercase
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, caseWord + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = caseWord + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + caseWord + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + caseWord;
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                        for (int item = 0; item < componentss.sayilar.Count; item++)
                        {
                            Thread.Sleep(speed);
                            File.AppendAllText(dosyaIsmi, componentss.sayilar[item].ToString() + " " + caseWord + " " + componentss.sayilar[item].ToString() + "\n");
                            kelimeadet++;
                            kelimeLabel.Text = componentss.sayilar[item].ToString() + " " + caseWord + " " + componentss.sayilar[item].ToString();
                            kelimeSayiLabel.Text = kelimeadet.ToString();
                        }
                    }
                    //-------------------------------------------//
                }
                caseWord = "";
                if(sozlukCheck.Checked == true)
                {
                    upper = true;
                    caseWord = "";
                    for (int i = 0; i < kelimeChar.Length; i++)
                    {
                        Thread.Sleep(speed);
                        if (upper == true)
                        {
                            caseWord += kelimeChar[i].ToString().ToUpper();
                            upper = false;
                        }
                        else
                        {
                            caseWord += kelimeChar[i].ToString().ToLower();
                            upper = true;

                        }
                    }


                    ustBilgiLabel.Text = "Sözlük alınıyor";
                    List<String> a = new List<string>();
                    string dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
                    FileStream fss = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw = new StreamReader(fss);
                    string yazi = sw.ReadLine();
                    while (yazi != null)
                    {
                        yazi = sw.ReadLine();
                        a.Add(yazi);
                    }
                    sw.Close();
                    fss.Close();
                    int alfabeİndex = 0;
                    ustBilgiLabel.Text = "Sözlük yazılıyor";
                    for (int item = 0; item < a.Count-1; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, a[item].ToString() + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = a[item].ToString() + kelimeListesi.Items[kelimeListesiIndex];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                        if(item == a.Count-2 && alfabeİndex < componentss.alphabeBig.Count-1)
                        {
                            alfabeİndex++;
                            item = 0;
                            a.Clear();
                            ustBilgiLabel.Text = "Sözlük alınıyor..";
                            dosya_yolu = "SpiderXWord/turkish-dictonary/"+componentss.alphabeBig[alfabeİndex]+".txt";
                            FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                            StreamReader sw2 = new StreamReader(fss2);
                            string yazi2 = sw2.ReadLine();
                            while (yazi2 != null)
                            {
                                yazi2 = sw2.ReadLine();
                                a.Add(yazi2);
                            }
                            sw2.Close();
                            fss2.Close();
                            continue;
                        }
                    }
                    a.Clear();
                    dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
                    FileStream fss3 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw3 = new StreamReader(fss3);
                    string yazi3 = sw3.ReadLine();
                    while (yazi3 != null)
                    {
                        yazi3 = sw3.ReadLine();
                        a.Add(yazi3);
                    }
                    sw3.Close();
                    fss3.Close();
                    alfabeİndex = 0;
                    ustBilgiLabel.Text = "Sözlük yazılıyor";

                    for (int item = 0; item < a.Count - 1; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + a[item].ToString() +  "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + a[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                        if (item == a.Count - 2 && alfabeİndex < componentss.alphabeBig.Count - 1)
                        {
                            alfabeİndex++;
                            item = 0;
                            a.Clear();
                            ustBilgiLabel.Text = "Sözlük alınıyor..";
                            dosya_yolu = "SpiderXWord/turkish-dictonary/" + componentss.alphabeBig[alfabeİndex] + ".txt";
                            FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                            StreamReader sw2 = new StreamReader(fss2);
                            string yazi2 = sw2.ReadLine();
                            while (yazi2 != null)
                            {
                                yazi2 = sw2.ReadLine();
                                a.Add(yazi2);
                            }
                            sw2.Close();
                            fss2.Close();
                            continue;
                        }
                    }
                    a.Clear();
                    dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
                    FileStream fss4 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw4 = new StreamReader(fss4);
                    string yazi4 = sw4.ReadLine();
                    while (yazi4 != null)
                    {
                        yazi4 = sw4.ReadLine();
                        a.Add(yazi4);
                    }
                    sw4.Close();
                    fss4.Close();
                    alfabeİndex = 0;
                    ustBilgiLabel.Text = "Sözlük yazılıyor";
                    for (int item = 0; item < a.Count - 1; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, caseWord + a[item].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = caseWord + a[item].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                        if (item == a.Count - 2 && alfabeİndex < componentss.alphabeBig.Count - 1)
                        {
                            alfabeİndex++;
                            item = 0;
                            a.Clear();
                            ustBilgiLabel.Text = "Sözlük alınıyor..";
                            dosya_yolu = "SpiderXWord/turkish-dictonary/" + componentss.alphabeBig[alfabeİndex] + ".txt";
                            FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                            StreamReader sw2 = new StreamReader(fss2);
                            string yazi2 = sw2.ReadLine();
                            while (yazi2 != null)
                            {
                                yazi2 = sw2.ReadLine();
                                a.Add(yazi2);
                            }
                            sw2.Close();
                            fss2.Close();
                            continue;
                        }
                    }
                }
                //-----------------------------------------------//
                //Boşluk
                if (spaceCheck.Checked == true)
                {
                    ustBilgiLabel.Text = "Boşluklar ve alfabe uygulanıyor";

                    for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "Bir kahve alın..";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeSmall.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                    ustBilgiLabel.Text = "Boşluklar uygulanıyor";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "Arkanıza yaslanın";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    //----------------------//
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "İşlemler devam ediyor";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[item] + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "Hala çalışıyoruz";
                    //-------------------------------//
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeBig[item] + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeBig[item];
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "Bunlar harika";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "işlemler devam ediyor";
                    //--------------------------------------------------//
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();

                    }
                    ustBilgiLabel.Text = "Az kaldı";
                    for (int item = 0; item < componentss.alphabeBig.Count; item++)
                    {
                        Thread.Sleep(speed);
                        File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                        kelimeadet++;
                        kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeBig[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                        kelimeSayiLabel.Text = kelimeadet.ToString();
                    }
                }
                generateListItem();
            }
        }
        //Özel karakterler ve alfabe eklenecek
        void SpecialAndAlphabe()
        {
            if(alphabeIndex < componentss.alphabeBig.Count-1)
            {
                alphabeIndex++;
                Thread generate2 = new Thread(new ThreadStart(SpecialAndAlphabeGenerate));
                generate2.Start();
            }
            else
            {
                if (kelimeListesiIndex < kelimeListesiUzunlugu - 1)
                {
                    kelimeListesiIndex++;
                    alphabeIndex = 0;
                    taskCompletedProggress.Value += 5;
                    ustBilgiLabel.Text = "Yazılıyor";
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndAlphabeGenerate));
                    generate2.Start();
                }
                else
                {
                    ozelKarakterYapildimi = true;
                    kelimeListesiUzunlugu = kelimeler.Count;
                    kelimeListesiIndex = 0;
                    if (sayilarCheck.Checked == true && sayilarYapildimi == false && ozelKarakterCheck.Checked == true)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndNumberGenerate));
                        generate2.Start();
                    }
                    else if (sozlukCheck.Checked == true && sozlukYapildimi == false)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndDictonaryGenerate));
                        generate2.Start();
                    }
                    else
                    {
                        taskCompletedProggress.Value = 100;
                        label13.ForeColor = Color.FromArgb(20, 200, 150);
                        bekleniyorIconHidePanel.Visible = false;
                        label13.Text = "Başarılı";
                        kalanProgress.Animated = false;
                        kalanProgress.Value = 100;
                        toplamSure.Enabled = false;
                        taskCompletedProggress.Value = 100;
                        StartButton.Enabled = true;
                        AllClearButton.Enabled = true;
                        KaristirBtn.Enabled = true;
                        sonucLabel.Text = "100%";
                        KelimeRadialGuerge.UseWaitCursor = false;
                        kalanProgress.UseWaitCursor = false;
                        label7.UseWaitCursor = false;
                        ustBilgiLabel.Text = "Bitti";
                        ustLoadingBar.Visible = false;
                        ustLoadingBar.Stop();
                    }
                }
            }
        }
        void SpecialAndAlphabeGenerate()
        {
            var kelimeChar = kelimeListesi.Items[kelimeListesiIndex].ToString().ToCharArray();
            String caseWord = "";
            lower = true;
            caseWord = "";
            for (int i = 0; i < kelimeChar.Length; i++)
            {
                Thread.Sleep(speed);
                if (lower == true)
                {
                    caseWord += kelimeChar[i].ToString().ToLower();
                    lower = false;
                }
                else
                {
                    caseWord += kelimeChar[i].ToString().ToUpper();
                    lower = true;

                }
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //----------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Hala çalışıyoruz";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] +  componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] +  componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi,caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Hala çalışıyoruz";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + caseWord+ componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + caseWord + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + caseWord + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + caseWord + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //--------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //-----------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, caseWord + componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = caseWord + componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "İşlemler devam ediyor";
            //-----------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] +  componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text =  componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] +  componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] +  componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //-------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //-------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Hala çalışıyoruz";
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi,  componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi,  componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text =  componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //---------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter, alfabe devam ediyor...";
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] +  kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            //---------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + caseWord + componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] + caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + componentss.specialCharacters[item] + caseWord + componentss.specialCharacters[item] + componentss.alphabeBig[alphabeIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //----------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex]+ componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex]+ componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex]+ componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex]+ componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Eklemeler devam ediyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex]+ kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex]+  kelimeListesi.Items[kelimeListesiIndex]+ componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex]+ kelimeListesi.Items[kelimeListesiIndex].ToString() +  kelimeListesi.Items[kelimeListesiIndex]+ componentss.specialCharacters[item]  + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item]+ componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            //---------------------------------------------------------------------//
            //----------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "İşlemler devam ediyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Hala çalışıyoruz";
            //----------------------------------------------------------------------//
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text =  componentss.specialCharacters[item]+ componentss.alphabeSmall[alphabeIndex]  + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + componentss.alphabeSmall[alphabeIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.alphabeSmall[alphabeIndex] + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }

            if(spaceCheck.Checked == true)
            {
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Hala çalışıyoruz";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                //--------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "İşlemler devam ediyor";
                //-----------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                //-------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Hala çalışıyoruz";
                //---------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                //---------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter, alfabe devam ediyor...";
                //---------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.alphabeBig[alphabeIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                //----------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Eklemeler devam ediyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeBig[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                //---------------------------------------------------------------------//
                //----------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "İşlemler devam ediyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Hala çalışıyoruz";
                //----------------------------------------------------------------------//
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "karakter ve alfabe ekleniyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.alphabeSmall[alphabeIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.alphabeSmall[alphabeIndex] + " " + kelimeListesi.Items[kelimeListesiIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
            }
            SpecialAndAlphabe();
        }


        void SpecialAndNumbers()
        {
            if (sayilarIndex < componentss.sayilar.Count - 1)
            {
                sayilarIndex++;
                Thread generate2 = new Thread(new ThreadStart(SpecialAndNumberGenerate));
                generate2.Start();
            }
            else
            {
                if (kelimeListesiIndex < kelimeListesiUzunlugu - 1)
                {
                    kelimeListesiIndex++;
                    sayilarIndex = 0;
                    taskCompletedProggress.Value += 5;
                    ustBilgiLabel.Text = "Sayılar eklenecek";
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndNumberGenerate));
                    generate2.Start();
                }
                else
                {
                    sayilarYapildimi = true;
                    kelimeListesiUzunlugu = kelimeler.Count;
                    kelimeListesiIndex = 0;
                    if (ozelKarakterCheck.Checked == true && ozelKarakterYapildimi == false)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndAlphabeGenerate));
                        generate2.Start();
                    }
                    else if (sozlukCheck.Checked == true && sozlukYapildimi == false)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndDictonaryGenerate));
                        generate2.Start();
                    }
                    else
                    {
                        taskCompletedProggress.Value = 100;
                        label13.ForeColor = Color.FromArgb(20, 200, 150);
                        bekleniyorIconHidePanel.Visible = false;
                        label13.Text = "Başarılı";
                        kalanProgress.Animated = false;
                        kalanProgress.Value = 100;
                        toplamSure.Enabled = false;
                        taskCompletedProggress.Value = 100;
                        StartButton.Enabled = true;
                        AllClearButton.Enabled = true;
                        KaristirBtn.Enabled = true;
                        sonucLabel.Text = "100%";
                        KelimeRadialGuerge.UseWaitCursor = false;
                        kalanProgress.UseWaitCursor = false;
                        label7.UseWaitCursor = false;
                        ustBilgiLabel.Text = "Bitti";
                        ustLoadingBar.Visible = false;
                        ustLoadingBar.Stop();
                    }
                }
            }
        }
        void SpecialAndNumberGenerate()
        {
            ustBilgiLabel.Text = "Sayılar ve özel karakter ekleniyor";

            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "İşlemler devam ediyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Sayılar, özel karakterler eklenecektir";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex]  + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex]  + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString();
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Hala çalışıyoruz";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                kelimeadet++;
                kelimeLabel.Text =  componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString();
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "İşlemler devam ediyor...";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "SpiderX çalışıyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Bunlar harika oldu";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "İşlemler devam ediyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Kahvenizi alın, çalışıyoruz..";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Sayılar ve özel karakter ekleniyor";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            ustBilgiLabel.Text = "Son rütuşlar...";
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.specialCharacters[item] + kelimeListesi.Items[kelimeListesiIndex].ToString() + componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item]  + componentss.sayilar[sayilarIndex] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.specialCharacters[item] + componentss.sayilar[sayilarIndex];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }
            for (int item = 0; item < componentss.specialCharacters.Count; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = componentss.sayilar[sayilarIndex] + componentss.sayilar[sayilarIndex] + componentss.specialCharacters[item] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
            }

            if(spaceCheck.Checked == true)
            {
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "İşlemler devam ediyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Sayılar, özel karakterler eklenecektir";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Hala çalışıyoruz";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString();
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "İşlemler devam ediyor...";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "SpiderX çalışıyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Bunlar harika oldu";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "İşlemler devam ediyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Kahvenizi alın, çalışıyoruz..";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Sayılar ve özel karakter ekleniyor";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                ustBilgiLabel.Text = "Son rütuşlar...";
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + " " + kelimeListesi.Items[kelimeListesiIndex].ToString() + " " + componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.specialCharacters[item] + " " + componentss.sayilar[sayilarIndex];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
                for (int item = 0; item < componentss.specialCharacters.Count; item++)
                {
                    Thread.Sleep(speed);
                    File.AppendAllText(dosyaIsmi, componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item] + "\n");
                    kelimeadet++;
                    kelimeLabel.Text = componentss.sayilar[sayilarIndex] + " " + componentss.sayilar[sayilarIndex] + " " + componentss.specialCharacters[item] + " " + componentss.specialCharacters[item];
                    kelimeSayiLabel.Text = kelimeadet.ToString();
                }
            }
            SpecialAndNumbers();
        }
        void SpecialAndDictonary()
        {
            if (sozlukIndex < dic.Count - 1)
            {
                sozlukIndex++;
                Thread generate2 = new Thread(new ThreadStart(SpecialAndDictonaryGenerate));
                generate2.Start();
            }
            else
            {
                if (kelimeListesiIndex < kelimeListesiUzunlugu - 1)
                {
                    kelimeListesiIndex++;
                    sozlukIndex = 0;
                    taskCompletedProggress.Value += 5;
                    ustBilgiLabel.Text = "Sözlük eklenecek";
                    Thread generate2 = new Thread(new ThreadStart(SpecialAndDictonaryGenerate));
                    generate2.Start();
                }
                else
                {
                    sozlukYapildimi = true;
                    kelimeListesiUzunlugu = kelimeler.Count;
                    kelimeListesiIndex = 0;
                    if (ozelKarakterCheck.Checked == true && ozelKarakterYapildimi == false)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndAlphabeGenerate));
                        generate2.Start();
                    }
                    else if (sayilarCheck.Checked == true && sayilarYapildimi == false && ozelKarakterCheck.Checked == true)
                    {
                        Thread generate2 = new Thread(new ThreadStart(SpecialAndNumberGenerate));
                        generate2.Start();
                    }
                    else
                    {
                        taskCompletedProggress.Value = 100;
                        label13.ForeColor = Color.FromArgb(20, 200, 150);
                        bekleniyorIconHidePanel.Visible = false;
                        label13.Text = "Başarılı";
                        kalanProgress.Animated = false;
                        kalanProgress.Value = 100;
                        toplamSure.Enabled = false;
                        taskCompletedProggress.Value = 100;
                        StartButton.Enabled = true;
                        AllClearButton.Enabled = true;
                        KaristirBtn.Enabled = true;
                        sonucLabel.Text = "100%";
                        KelimeRadialGuerge.UseWaitCursor = false;
                        kalanProgress.UseWaitCursor = false;
                        label7.UseWaitCursor = false;
                        ustBilgiLabel.Text = "Bitti";
                        ustLoadingBar.Visible = false;
                        ustLoadingBar.Stop();
                    }
                }
            }
        }
        int alfabeIndex2 = 0;
        String dosya_yolu = "";
        void SpecialAndDictonaryGenerate()
        {
            ustBilgiLabel.Text = "Karakter ve sözlük";
            for (int item = 0; item < componentss.specialCharacters.Count - 1; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, dic[sozlukIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = dic[sozlukIndex].ToString() + kelimeListesi.Items[kelimeListesiIndex] + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
                if (item == dic.Count - 2 && sozlukIndex < componentss.alphabeBig.Count - 1)
                {
                    alfabeIndex2++;
                    item = 0;
                    dic.Clear();
                    ustBilgiLabel.Text = "Sözlük alınıyor..";
                    dosya_yolu = "SpiderXWord/turkish-dictonary/" + componentss.alphabeBig[alfabeIndex2] + ".txt";
                    FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw2 = new StreamReader(fss2);
                    string yazi2 = sw2.ReadLine();
                    while (yazi2 != null)
                    {
                        yazi2 = sw2.ReadLine();
                        dic.Add(yazi2);
                    }
                    sw2.Close();
                    fss2.Close();
                    continue;
                }
            }
            alfabeIndex2 = 0;
            dic.Clear();
            ustBilgiLabel.Text = "Sözlük alınıyor..";
            dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
            FileStream fss3 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw3 = new StreamReader(fss3);
            string yazi3 = sw3.ReadLine();
            while (yazi3 != null)
            {
                yazi3 = sw3.ReadLine();
                dic.Add(yazi3);
            }
            sw3.Close();
            fss3.Close();


            for (int item = 0; item < componentss.specialCharacters.Count - 1; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex] + dic[sozlukIndex].ToString() + componentss.specialCharacters[item] + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex] + dic[sozlukIndex].ToString() + componentss.specialCharacters[item];
                kelimeSayiLabel.Text = kelimeadet.ToString();
                if (item == dic.Count - 2 && sozlukIndex < componentss.alphabeBig.Count - 1)
                {
                    alfabeIndex2++;
                    item = 0;
                    dic.Clear();
                    ustBilgiLabel.Text = "Sözlük alınıyor..";
                    dosya_yolu = "SpiderXWord/turkish-dictonary/" + componentss.alphabeBig[alfabeIndex2] + ".txt";
                    FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw2 = new StreamReader(fss2);
                    string yazi2 = sw2.ReadLine();
                    while (yazi2 != null)
                    {
                        yazi2 = sw2.ReadLine();
                        dic.Add(yazi2);
                    }
                    sw2.Close();
                    fss2.Close();
                    continue;
                }
            }

            alfabeIndex2 = 0;
            dic.Clear();
            ustBilgiLabel.Text = "Sözlük alınıyor..";
            dosya_yolu = "SpiderXWord/turkish-dictonary/A.txt";
            FileStream fss4 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw4 = new StreamReader(fss4);
            string yazi4 = sw4.ReadLine();
            while (yazi4 != null)
            {
                yazi4 = sw4.ReadLine();
                dic.Add(yazi4);
            }
            sw4.Close();
            fss4.Close();


            for (int item = 0; item < componentss.specialCharacters.Count - 1; item++)
            {
                Thread.Sleep(speed);
                File.AppendAllText(dosyaIsmi, kelimeListesi.Items[kelimeListesiIndex]  + componentss.specialCharacters[item] + dic[sozlukIndex].ToString() + "\n");
                kelimeadet++;
                kelimeLabel.Text = kelimeListesi.Items[kelimeListesiIndex]  + componentss.specialCharacters[item] + dic[sozlukIndex].ToString();
                kelimeSayiLabel.Text = kelimeadet.ToString();
                if (item == dic.Count - 2 && sozlukIndex < componentss.alphabeBig.Count - 1)
                {
                    alfabeIndex2++;
                    item = 0;
                    dic.Clear();
                    ustBilgiLabel.Text = "Sözlük alınıyor..";
                    dosya_yolu = "SpiderXWord/turkish-dictonary/" + componentss.alphabeBig[alfabeIndex2] + ".txt";
                    FileStream fss2 = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw2 = new StreamReader(fss2);
                    string yazi2 = sw2.ReadLine();
                    while (yazi2 != null)
                    {
                        yazi2 = sw2.ReadLine();
                        dic.Add(yazi2);
                    }
                    sw2.Close();
                    fss2.Close();
                    continue;
                }
            }
            SpecialAndDictonary();
        }

        //gecen zaman hesaplama
        int hour;
        int minutes;
        int second;
        private void toplamSure_Tick(object sender, EventArgs e)
        {
            //arttırma kontrolu
            if (second <= 60)
            {
                second++;
            }
            if (second == 60)
            {
                minutes++;
                second = 0;
            }
            if (minutes == 59)
            {
                minutes = 0;
                hour++;
            }
            if (hour >= 23)
            {
                hour = 0;
            }
            //yazdırma kontrolu
            if (minutes < 10 && second < 10)
            {
                label7.Text = hour + ":" + "0" +minutes + ":" + "0" + second;

            }
            else if(minutes < 10)
            {
                label7.Text = hour + ":" + "0" + minutes + ":" + second;
            }
            else if(second < 10)
            {
                label7.Text = hour + ":" + minutes + ":" + "0" + second;

            }
            else
            {
                label7.Text = hour + ":" + minutes + ":" + second;

            }
            //------------------------------
            //guc durumu kontrolu
            if (kelimeadet < 1000)
            {
                gucDurumu.Text = "kötü";
                gucProgress.Value = 5;
            }
            if (kelimeadet > 1000 && kelimeadet < 5000)
            {
                gucDurumu.Text = "yetersiz";
                gucProgress.Value = 9;
            }
            if (kelimeadet > 5000 && kelimeadet < 15000)
            {
                gucDurumu.Text = "çok zayıf";
                gucProgress.Value = 12;
            }
            if (kelimeadet > 15000 && kelimeadet < 45000)
            {
                gucDurumu.Text = "zayıf";
                gucProgress.Value = 20;
            }
            if (kelimeadet > 45000 && kelimeadet < 60000)
            {
                gucDurumu.Text = "düşük";
                gucProgress.Value = 25;
            }
            if (kelimeadet > 60000 && kelimeadet < 100000)
            {
                gucDurumu.Text = "orta";
                gucProgress.Value = 28;
            }
            if (kelimeadet > 100000 && kelimeadet < 150000)
            {
                gucDurumu.Text = "normal";
                gucProgress.Value = 30;
            }
            if (kelimeadet > 150000 && kelimeadet < 200000)
            {
                gucDurumu.Text = "güzel";
                gucProgress.Value = 36;
            }
            if (kelimeadet > 200000 && kelimeadet < 300000)
            {
                gucDurumu.Text = "Çok iyi";
                gucProgress.Value = 40;
            }
            if (kelimeadet > 300000 && kelimeadet < 400000)
            {
                gucDurumu.Text = "yeterli";
                gucProgress.Value = 50;
            }
            if (kelimeadet > 400000 && kelimeadet < 600000)
            {
                gucDurumu.Text = "süper";
                gucProgress.Value = 62;
            }
            if (kelimeadet > 600000 && kelimeadet < 750000)
            {
                gucDurumu.Text = "güçlü";
                gucProgress.Value = 70;
            }
            if (kelimeadet > 750000)
            {
                gucDurumu.Text = "Çok güçlü";
                gucProgress.Value = 100;
            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (quickMode == false)
            {
                quickMode = true;
                guna2TileButton1.Image = Image.FromFile("icons/quick_mode_on.png");
                yavasModToogle.Checked = false;
                speed = 35;
            }
            else
            {
                quickMode = false;
                speed = 80;
                guna2TileButton1.Image = Image.FromFile("icons/quickmode.png");

            }
        }

        private void yavasModToogle_CheckedChanged(object sender, EventArgs e)
        {
            if (yavasModToogle.Checked == true)
            {
                quickMode = false;
                speed = 200;
                guna2TileButton1.Image = Image.FromFile("icons/quickmode.png");
            }
            else
            {
                speed = 80;
            }
        }
    }
}
