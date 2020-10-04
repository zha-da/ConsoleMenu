using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleMenu
{
    /// <summary>
    /// Класс настроек для меню
    /// </summary>
    public class MenuSettings
    {
        /// <summary>
        /// Фраза-приветствие
        /// </summary>
        public string OpeningPhrase { get; set; } = "Выберите один из следующих пунктов меню";
        /// <summary>
        /// Фраза-прощание
        /// </summary>
        public string ClosingPhrase { get; set; } = "Завершение работы меню";
        /// <summary>
        /// Клавиша для завершения работы меню
        /// </summary>
        public ConsoleKey ExitKey { get; set; } = ConsoleKey.Escape;
        /// <summary>
        /// Слово для завершения работы меню
        /// </summary>
        public string ExitWord { get; set; } = "exit";
        /// <summary>
        /// Каким цветом будет подсвечиваться пункт меню
        /// </summary>
        public ConsoleColor HighlightColor { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// Цвет пункта меню в состоянии покоя
        /// </summary>
        public ConsoleColor RestingColor { get; set; } = ConsoleColor.White;
        /// <summary>
        /// Режим запуска меню
        /// </summary>
        public MenuModes CurrentMode { get; set; } = MenuModes.Buttons;
    }
}
