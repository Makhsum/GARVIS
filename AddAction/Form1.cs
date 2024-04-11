using AddAction.Models;
using AddAction.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddAction
{
    public partial class Form1 : Form
    {
        string actionFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GARVISSETTINGS";
        string actionKeyFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GARVISSETTINGS";

        SoundPlayer player = new SoundPlayer();

        private ActionStartModel model = new ActionStartModel();
        private ActionKeys key = new ActionKeys();
        private SaveData<ActionStartModel> startData = null;
        private SaveData<ActionKeys> keyData = null;
        Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
            Directory.CreateDirectory(actionFilePath);
            Directory.CreateDirectory(actionKeyFilePath);
            startData = new SaveData<ActionStartModel>(actionFilePath + "\\StartModels.json");
            keyData = new SaveData<ActionKeys>(actionKeyFilePath + "\\actionKeys.json");

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string actionkey = actionKey.Text;
                if (actionkey != null || actionkey != "" || actionkey != " " && actionkey.Length > 5)
                {
                    model.ActionKey = actionkey;
                    key.ActionKeyss = actionkey;
                    model.ActionAdress = GetFilePath();

                    startData.SaveDAta(model);
                    keyData.SaveDAta(key);
                    MessageBox.Show("Enjoy hahahahahah");
                    string soundFolderPath = Path.GetTempPath() + "\\MyExtractedFolder\\sound\\jarvis-og\\absolute.wav";
                    // soundFolderPath1 = Path.Combine(currentDirectory, "sound");



                    if (File.Exists(soundFolderPath))
                    {

                        if (checkBox1.Checked)
                        {
                            player.SoundLocation = soundFolderPath;

                            form2.Show();
                            player.Play();
                            await Task.Delay(12000);
                            player.Stop();
                            form2.Hide();
                        }
                        else
                        {
                            player.Stop();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Запустите Джарвиса и снова попробуйте не могу найти звуковой файл");
                    }
                    if (checkBox2.Checked)
                    {
                        if (File.Exists("GARVIS"))
                        {

                            Process[] processes = Process.GetProcessesByName("GARVIS");

                            if (processes.Length > 0)
                            {
                                foreach (Process process in processes)
                                {
                                    try
                                    {
                                        process.Kill();
                                        Console.WriteLine($"Процесс {process.ProcessName} завершен успешно.");
                                        process.Start();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Ошибка при завершении процесса: {ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                Process.Start("GARVIS.exe");
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: "+ex.Message);
            }

           
           
        }
        private string GetFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файл";
                openFileDialog.Filter = "Все файлы (*.*)|*.*";

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }

                return null;
            }
        }

        private void musikkBox_CheckedChanged(object sender, EventArgs e)
        {
            
           string soundFolderPath = Path.GetTempPath()+ "\\MyExtractedFolder\\sound\\jarvis-og\\thunder-theme.wav";
            // soundFolderPath1 = Path.Combine(currentDirectory, "sound");

            player = new SoundPlayer();

            if (File.Exists(soundFolderPath))
             {

                if (musikkBox.Checked)
                {
                    player.SoundLocation = soundFolderPath;
                    player.Play();

                }
                else
                {
                    player.Stop();
                }
            }
            
            else
            {
                MessageBox.Show("Запустите Джарвиса и снова попробуйте не могу найти звуковой файл");
            }
           
            // soundFolderPath1 = Path.Combine(currentDirectory, "sound");
           
            
        }

    

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(actionFilePath + "\\StartModels.json"))
            {
                File.Delete(actionFilePath + "\\StartModels.json");
                MessageBox.Show("Enjoy");
            }
            if (File.Exists(actionFilePath + "\\actionKeys.json"))
            {
                File.Delete(actionFilePath + "\\actionKeys.json");
                MessageBox.Show("Enjoy");
            }
            else
            {
                MessageBox.Show("Было и так пусто пидр");
            }
        }

        private void actionKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar!= ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Блокировать ввод цифр
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string soundFolderPath = Path.GetTempPath() + "\\MyExtractedFolder\\sound\\jarvis-og\\thunder-theme.wav";
            // soundFolderPath1 = Path.Combine(currentDirectory, "sound");

            player = new SoundPlayer();

            if (File.Exists(soundFolderPath))
            {

                if (musikkBox.Checked)
                {
                    player.SoundLocation = soundFolderPath;
                    player.Play();

                }
                else
                {
                    player.Stop();
                }
            }

            else
            {
                MessageBox.Show("Запустите Джарвиса и снова попробуйте не могу найти звуковой файл");
            }
        }
    }
}
