# Jarvis Voice Assistant Repository

This repository contains a voice assistant named Jarvis, developed on .NET Framework 4.7. Jarvis utilizes the Speech Platform library for speech recognition and is designed specifically to operate in the Russian language environment. Additionally, the repository includes a configuration tool, allowing users to create custom commands for Jarvis.

## Default Commands

Jarvis comes pre-configured with the following default commands:

- "включи плейлист" (turn on playlist)
- "телепортация к ментам" (teleport to cops)
- "максимальное здоровье" (maximum health)
- "деньги" (money)
- "телепортация к террорам" (teleport to terrorists)
- "Открой браузер" (open browser)
- "Открой текстовый редактор" (open text editor)
- "Покажи текущее время" (show current time)
- "Запусти калькулятор" (launch calculator)
- "Открой файловый менеджер" (open file manager)
- "Покажи список файлов в текущей папке" (show list of files in current folder)
- "Выведи сообщение" (display message)
- "Заверши программу" (terminate program)
- "Открой календарь" (open calendar)
- "Сделай скриншот" (take screenshot)
- "Покажи список процессов" (show list of processes)
- "Создай текстовый файл" (create text file)
- "Открой командную строку" (open command prompt)
- "Включи музыку" (play music)
- "Покажи IP-адрес" (show IP address)
- "Открой редактор изображений" (open image editor)
- "Запусти игру" (launch game)
- "Установи напоминание" (set reminder)
- "Открой электронную почту" (open email)
- "Джарвис, открой ютуб" (Jarvis, open YouTube)

## Configuration Tool

In addition to the default commands, this repository includes a configuration tool that allows users to customize Jarvis by creating their own commands and modifying existing ones. The configuration tool simplifies the process of extending Jarvis's functionality according to specific user needs.

## Requirements
To install and run Jarvis on your system, the following prerequisites are necessary:

1. **Operating System**: Windows with Russian language support.
2. **Dependencies**: 
   - Microsoft Speech Platform SDK
   - Microsoft Speech Platform Runtime
   - Microsoft Speech Platform Language Pack (Russian)
   - .NET Framework 4.7

## Installation Instructions
To set up Jarvis on your machine, follow these steps:

1. **Install Dependencies**:
   - Download and install the Microsoft Speech Platform SDK.
   - Install the Microsoft Speech Platform Runtime.
   - Install the Russian language pack for the Speech Platform.

2. **Build the Application**:
   - Clone the repository to your local machine.
   - Open the solution in Visual Studio.
   - Restore NuGet packages to ensure all dependencies are up to date.
   - Build the solution.

3. **Run Jarvis**:
   - Once the build is successful, run the Jarvis application.
   - Ensure your microphone is correctly set up and configured.

By default, Jarvis is configured to respond to the above commands. Use the configuration tool to enhance and customize Jarvis further.

## Note
Jarvis is optimized for Russian language recognition and operates effectively in a Russian language environment on Windows. For any issues or suggestions, please refer to the repository's issue tracker.

## Author
Created by Makhsum

Feel free to contribute and enhance Jarvis's functionality by submitting pull requests or reporting issues. Happy coding!
