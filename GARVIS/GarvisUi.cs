using GARVIS.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace GARVIS
{
    public partial class GarvisUi : Form
    {
        public GarvisUi()
        {
            InitializeComponent();
            
        }
            private void GarvisUi_Load(object sender, EventArgs e)
            {
            string archiveName = "GARVIS.sound.zip";

            // Имя временной папки, куда будет извлекаться содержимое архива

            string extractPath = Path.Combine(Path.GetTempPath(), "MyExtractedFolder");

            Directory.CreateDirectory(extractPath);

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(archiveName))
            {
                if (resourceStream != null)
                {
                    using (ZipInputStream zipStream = new ZipInputStream(resourceStream))
                    {
                        ZipEntry entry;
                        while ((entry = zipStream.GetNextEntry()) != null)
                        {
                            string entryPath = Path.Combine(extractPath, entry.Name.Replace('/', '\\'));

                            // Создаем подкаталоги при необходимости
                            string entryDirectory = Path.GetDirectoryName(entryPath);

                            if (!string.IsNullOrEmpty(entryDirectory) && !Directory.Exists(entryDirectory))
                            {
                                Directory.CreateDirectory(entryDirectory);
                            }

                            // Проверяем, что директория создана перед созданием файла
                            if (Directory.Exists(entryDirectory))
                            {
                                try
                                {
                                    using (FileStream outputStream = File.Create(entryPath))
                                    {
                                        zipStream.CopyTo(outputStream);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    
                                }
                            }
                            else
                            {
                                Console.WriteLine("Директория не существует: " + entryDirectory);
                            }
                        }
                    }
                }
            }
            string[] commands = new string[]
                   {
                        "включи плейлист",
                       "телепортация к ментам",
                       "максимальное здоровье",
                       "деньги",
                       "телепортация к терорам",
                    "Открой браузер",
                    "Открой текстовый редактор",
                    "Покажи текущее время",
                    "Запусти калькулятор",
                    "Открой файловый менеджер",
                    "Покажи список файлов в текущей папке",
                    "Выведи сообщение",
                    "Заверши программу",
                    "Открой календарь",
                    "Сделай скриншот",
                    "Покажи список процессов",
                    "Создай текстовый файл",
                    "Открой командную строку",
                    "Включи музыку",
                    "Покажи IP-адрес",
                    "Открой редактор изображений",
                    "Запусти игру",
                    "Установи напоминание",
                    "Открой электронную почту",
                    "Джарвис", "открой ютуб",
                   
                   };
            try
            {
                Jarvis jarvis = new Jarvis(commands);
                jarvis.Start();
              
                
            }
            catch (Exception)
            {
                ClickableLinkMessageBox.Show("Зависимостри программы не установлены! чтобы их установить найдите в телеграм канале канал с названием garvis_exe.", "Открыть ссылку", "https://t.me/garvis_exe");
            

                Application.Exit();
            }
                
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    } 
