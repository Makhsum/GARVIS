using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using Newtonsoft.Json;
using AddAction.Models;
using Memory;
using System.Runtime.InteropServices;

namespace GARVIS.Servises
{

    public class Jarvis
    {
        private Mem memory = new Mem();
        cshack cshack = new cshack();
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
         int dwDesiredAccess,
         bool bInheritHandle,
         int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(
          IntPtr hProcess,
          IntPtr lpAddress,
          uint dwSize,
          uint flAllocationType,
          uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,

          byte[] lpBuffer,
          uint nSize,
          out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          IntPtr lpStartAddress,
          IntPtr lpParameter,
          uint dwCreationFlags,
          IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);
     
        private string actionFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GARVISSETTINGS"+ "\\StartModels.json";
        private string actionKeyFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GARVISSETTINGS"+"\\actionKeys.json";
        string screenshotsFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots");
        private string soundFolderPath = null;
        private string[] yesfilenames = null;
        private string[] okfilenames = null;
        private string[] thanksfilenames = null;
        private string[] hifilenames = null;
        private  string[] _commands;
        private string[] _commands1 = { "as"};
        private readonly string _key;

        Random rnd = new Random();
        string currentDirectory = Directory.GetCurrentDirectory();
        SoundPlayer player = new SoundPlayer();
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new CultureInfo("ru-RU"));
        public Jarvis(string[] commands)
        {
           
            _commands = commands;
           
            // gpt = new ChatGPT(key);
            // _soundToText = new SoundToText(key);
        }
        public async void Start()
        {
            try
            {
                soundFolderPath = Path.Combine(Path.GetTempPath(), "MyExtractedFolder");
               // soundFolderPath1 = Path.Combine(currentDirectory, "sound");
                yesfilenames = Directory.GetFiles(soundFolderPath + "\\sound\\jarvis-og\\yes");
                okfilenames = Directory.GetFiles(soundFolderPath + "\\sound\\jarvis-og\\ok");
                thanksfilenames = Directory.GetFiles(soundFolderPath + "\\sound\\jarvis-og\\thanks");
                hifilenames = Directory.GetFiles(soundFolderPath + "\\sound\\jarvis-og\\hi");
                player.SoundLocation = hifilenames[rnd.Next(hifilenames.Length)];
                player.Play();
                recognizer.SetInputToDefaultAudioDevice();
                ReadCommands();
                var grammar = new Grammar(new GrammarBuilder(new Choices(_commands)));
                recognizer.LoadGrammar(grammar);
                // _soundToText.Start();
                recognizer.SpeechRecognized += Recognizer;
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // string textresponse = await AnswerAi(_response.text);
                // SpeakText(textresponse);
                Console.WriteLine("Говорите что-нибудь... (нажмите Enter для завершения) klass jarvis");
               while (true)
                {
                    await Task.Delay(3000);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Извините но тупой даун не нашел программу в общем или вы указали не правильное местоположение или вы даун!");
            }

        }
        private void Recognizer(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                string res = e.Result.Text.ToLower();
                Console.WriteLine("Распознано: " + e.Result.Text);
                GarvisUi.label1.Text = "Распознано: " + e.Result.Text;


                Answer(res);
            }

        }
        private async void Answer(string res)
        {
            try
            {
                var addreses = ReadAddress();
                if (addreses != null)
                {
                   
                    foreach (var item in addreses)
                    {
                        if (res == item.ActionKey)
                        {
                            player.SoundLocation = yesfilenames[rnd.Next(yesfilenames.Length)];
                            Process.Start(item.ActionAdress);
                            player.Play();
                            return;
                        }
                        //
                    }
                }

                switch (res)
                {
                  
                    case "джарвис":
                        player.SoundLocation = yesfilenames[rnd.Next(yesfilenames.Length)];
                        player.Play();
                        break;

                    case "включи суперспособности":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                       
                        break;
                    case "телепортация к терорам":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        cshack.TeleportToTeror();
                        break;
                    case "максимальное здоровье":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        cshack.Health();
                        break;
                    case "деньги":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        cshack.GetMoney();
                        break;
                    case
                       "телепортация к ментам":
                       
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        cshack.TeleportToMeant();
                        break;
                    case "я ниньдзя":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                    
                        break;

                    case "открой ютуб":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        // Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", "youtube.com");
                        Process.Start("chrome.exe", "youtube.com");
                        break;
                    case "открой браузер":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("chrome.exe");
                        break;

                    case "открой текстовый редактор":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("notepad.exe");
                        break;

                    case "покажи текущее время":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Console.WriteLine("Текущее время: " + DateTime.Now.ToString("HH:mm:ss"));
                        break;

                    case "выключи компьютер":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("shutdown", "/s /t 0");
                        break;

                    case "запусти калькулятор":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("calc.exe");
                        break;

                    case "открой файловый менеджер":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("explorer.exe");
                        break;

                    case "покажи список файлов в текущей папке":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
                        Console.WriteLine("Файлы в текущей папке:");
                        foreach (string file in files)
                        {
                            Console.WriteLine(System.IO.Path.GetFileName(file));
                        }
                        break;

                    case "выведи сообщение":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Console.Write("Введите сообщение: ");
                        string message = Console.ReadLine();
                        Console.WriteLine("Сообщение: " + message);
                        break;

                    case "заверши программу":
                        player.SoundLocation = thanksfilenames[rnd.Next(thanksfilenames.Length)];

                        player.Play();
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    case "открой календарь":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("outlookcal:");
                        break;

                    case "сделай скриншот":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

                        // Создаем объект Graphics для рисования на Bitmap
                        using (Graphics gfx = Graphics.FromImage(screenshot))
                        {
                            // Захватываем изображение текущего экрана
                            gfx.CopyFromScreen(SystemInformation.VirtualScreen.X, SystemInformation.VirtualScreen.Y, 0, 0, screenshot.Size);
                        }

                        // Сохраняем снимок экрана в файл (формат изображения PNG)
                        string screeenshotname = rnd.Next(1000000) + "screenshot.png";
                        screenshot.Save(screenshotsFolderPath + screeenshotname, ImageFormat.Png);

                        Console.WriteLine("Снимок экрана сохранен как " + screeenshotname + ".");

                        // Освобождаем ресурсы
                        screenshot.Dispose();
                        // Process.Start("snippingtool.exe");
                        Process.Start("explorer.exe", screenshotsFolderPath);
                        Process.Start(screenshotsFolderPath + screeenshotname);
                        break;

                    case "покажи список процессов":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("taskmgr.exe");
                        break;

                    case "создай текстовый файл":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Console.Write("Введите имя файла: ");
                        string fileName = Console.ReadLine();
                        System.IO.File.CreateText(fileName + ".txt");
                        Console.WriteLine("Текстовый файл создан: " + fileName + ".txt");
                        break;

                    case "открой командную строку":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("cmd.exe");
                        break;

                    case "включи музыку":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("wmplayer.exe");
                        break;

                    case "покажи ip-адрес":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("ipconfig.exe");
                        break;

                    case "открой редактор изображений":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("mspaint.exe");
                        break;

                    case "запусти игру":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("game.exe");
                        break;

                    case "установи напоминание":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Console.Write("Введите текст напоминания: ");
                        string reminder = Console.ReadLine();
                        Console.Write("Введите время (чч:мм): ");
                        string time = Console.ReadLine();
                        Console.WriteLine("Установлено напоминание: " + reminder + " в " + time);
                        break;

                    case "открой электронную почту":
                        player.SoundLocation = okfilenames[rnd.Next(okfilenames.Length)];
                        player.Play();
                        Process.Start("chrome.exe", "https://mail.google.com/mail/u/0/#inbox");
                        break;
                    default:
                        player.SoundLocation = soundFolderPath + "\\sound\\jarvis-og\\not found\\not_found.wav";
                        player.Play();
                        break;
                }
            }
            catch (Exception exception)
            {
                player.SoundLocation = soundFolderPath + "\\sound\\jarvis-og\\not found\\not_found.wav";
                player.Play();
             
            }
            async Task<string> GetFilePath()
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
        }

        private void ReadCommands ()
        {
            if (File.Exists(actionKeyFilePath))
            {
                string jsonContent = File.ReadAllText(actionKeyFilePath);
                var deserializedData = JsonConvert.DeserializeObject<List<ActionKeys>>(jsonContent);
                List<string> commands = new List<string>();
                foreach (var item in deserializedData)
                {
                    commands.Add(item.ActionKeyss);
                }
                string[] combinedArray = new string[_commands.Length + commands.Count];
                _commands.CopyTo(combinedArray, 0);
                commands.CopyTo(combinedArray, _commands.Length);
                _commands = combinedArray;
            }
            else
            {
                MessageBox.Show("Параметры не настроены! Сделайте сначала настройки хотябы 1 приложения");
                if (File.Exists("AddAction.exe"))
                {
                    Process.Start("AddAction.exe");
                }
                Application.Exit();
            }
           
        } 
        private List<ActionStartModel> ReadAddress ()
        {
            if (File.Exists(actionFilePath))
            {
                string jsonContent = File.ReadAllText(actionFilePath);
                var deserializedData = JsonConvert.DeserializeObject<List<ActionStartModel>>(jsonContent);
         
                return deserializedData;
            
            }
            else
            {
                MessageBox.Show("Параметры не настроены! Сделайте сначала настройки хотябы 1 приложения");
                if (File.Exists("AddAction.exe"))
                {
                    Process.Start("AddAction.exe");
                }
                Application.Exit();
                return null;
            }
           
        }
        private void Cham3D()
        {
            Process[] processesByName = Process.GetProcessesByName("HD-Player");
            if (processesByName.Length != 0)
            {
                IntPtr num1 = OpenProcess(2035711, false, processesByName[0].Id);
                if (num1 != IntPtr.Zero)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("C:\\Windows\\hk4x.dll");
                    IntPtr num2 = VirtualAllocEx(num1, IntPtr.Zero, (uint)bytes.Length, 4096U, 64U);
                    WriteProcessMemory(num1, num2, bytes, (uint)bytes.Length, out int _);
                    IntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                    int num3 = (int)WaitForSingleObject(CreateRemoteThread(num1, IntPtr.Zero, 0U, procAddress, num2, 0U, IntPtr.Zero), uint.MaxValue);
                    CloseHandle(num1);

                    Console.Beep(600, 300);
                }

            }
            else
            {

                Console.Beep(200, 300);
            }
        }

    }
}
