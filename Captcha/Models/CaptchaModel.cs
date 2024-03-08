using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Captcha
{
    internal class CaptchaModel:ReactiveObject
    {

        public CaptchaModel(int length, double width, double height)
        {
            int abcLength = englishABCAndNumbersForCaptcha.Length;
            for (int i = 0; i < length; i++)
            {
                _text += englishABCAndNumbersForCaptcha[rnd.Next(0, abcLength)];
            }
            Image = CreateCaptcha(_text);
            CreateInterference(3, width, height);
        }



        private Grid CreateCaptcha(string text)//тут будет возврат Canvas
        {
            List<TextBlock> textForCaptcha = new List<TextBlock>();


            foreach(var letter in  text)
            {
                TextBlock letterTextBlock = new TextBlock();
                
                letterTextBlock.Text = letter.ToString();
                letterTextBlock.Margin = RandomTextMargin(0,8,0,10);//min ,max
                letterTextBlock.FontSize = RandomFontSize(15,25);//min,max

                letterTextBlock.Foreground = RandomColor();
                letterTextBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
                letterTextBlock.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;


                textForCaptcha.Add(letterTextBlock);
            }
            Grid gridForCaptcha = new Grid();


            int numberColumn = 0;
            foreach (var item in textForCaptcha)
            {
                gridForCaptcha.ColumnDefinitions.Add(new ColumnDefinition()); //добавляем новый столбец для буквы

                gridForCaptcha.Children.Add(item);  //добавляем букву в грид
                Grid.SetColumn(item, numberColumn); //закидываем букву в последний столбец

                numberColumn++;
            }
            return gridForCaptcha;
        }

        private void CreateInterference(int interferencesMultiplier, double width, double height)
        {
            Canvas canvas = new Canvas();

            canvas.Width = Image.Width;
            canvas.Height = Image.Height;

            for (int i = 0; i < 100; i++)
            {
                Line line = new Line()
                {
                    StartPoint = RandomPoint(width, height),
                    EndPoint = RandomPoint(width, height),

                    StrokeThickness = rnd.Next(2, 5),
                    Stroke = RandomColor(true)
                };
                canvas.Children.Add(line);
            }

            Image.Children.Add(canvas);
            Grid.SetColumnSpan(canvas, Image.ColumnDefinitions.Count);
        }



        #region Свойства

        public string Text { get => _text; }
        public Grid Image { get; }//ну да

        #endregion
        #region Поля

        private string _text = "";

        private Random rnd = new Random();
        /// <summary>
        /// позже надо бы разделить на части: алфавит/числа
        /// </summary>
        private const string englishABCAndNumbersForCaptcha = "abcdefghijklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789"; //I - тут не нужна, мешает

        #endregion


        /// <summary>
        /// отступ между сторонами, вычисляется к каждой стороне отдельно, от 0 до указанного
        /// </summary>
        /// <param name="marginLeft"></param>
        /// <param name="marginTop"></param>
        /// <param name="marginRight"></param>
        /// <param name="marginBotton"></param>
        /// <returns></returns>
        private Thickness RandomTextMargin(double marginLeft, double marginTop, double marginRight, double marginBotton)
        {
            return new Thickness(
                rnd.Next(0, Convert.ToInt32(Math.Round(marginLeft))), //left
                rnd.Next(0, Convert.ToInt32(Math.Round(marginTop))), //top
                rnd.Next(0, Convert.ToInt32(Math.Round(marginRight))), //right
                rnd.Next(0, Convert.ToInt32(Math.Round(marginBotton)))  //bottom
                );
        }
        /// <summary>
        /// размер текста
        /// </summary>
        /// <param name="minFontSize">минимальный размер текста</param>
        /// <param name="maxFontSize">максимальный размер текста</param>
        /// <returns></returns>
        private double RandomFontSize(double minFontSize = 10, double maxFontSize = 25)
        {
            return rnd.Next(
                Convert.ToInt32(Math.Round(minFontSize)),
                Convert.ToInt32(Math.Round(maxFontSize)));
        }
        /// <summary>
        /// цвет помех/текста
        /// </summary>
        /// <param name="isLine">Это линия? если == true, то цвет будет более прозрачным
        /// это необходимо для того, чтобы текст можно было различить</param>
        /// <returns></returns>
        private IBrush RandomColor(bool isLine = false)
        {
            byte a = (isLine) ? Convert.ToByte(rnd.Next(0, 50)) : 
                                Convert.ToByte(rnd.Next(70, 255));

            byte r = Convert.ToByte(rnd.Next(0, 255));
            byte g = Convert.ToByte(rnd.Next(0, 255));
            byte b = Convert.ToByte(rnd.Next(0, 255));

            return new SolidColorBrush(Color.FromArgb(a,r,g,b));
        }
        /// <summary>
        /// рандомная точкка
        /// </summary>
        /// <param name="widthCaptcha">ширина капчи</param>
        /// <param name="heightCaptcha">высота капчи</param>
        /// <returns></returns>
        private Point RandomPoint(double widthCaptcha, double heightCaptcha)
        {
            int width = Convert.ToInt32(Math.Round(widthCaptcha));
            int height = Convert.ToInt32(Math.Round(heightCaptcha));

            double x1 = rnd.Next(0, width);
            double y1 = rnd.Next(0, height);

            return new Point(x1,y1);
        }
        
    }
}
