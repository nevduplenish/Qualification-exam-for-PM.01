using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog
{
    public partial class Form1 : Form
    {
        // поля
        private OpenFileDialog _openFile;

        private Label _aboutCoordOne;
        private Label _aboutCoordTwo;
        
        private TextBox _xCoordInput;
        private TextBox _yCoordInput;
        
        private Button _calculateButton;

        private double _xCoord;
        private double _yCoord;

        // инициализация компонентов
        public Form1()
        {
            InitializeComponent();
            _openFile = new OpenFileDialog();
            _openFile.Filter = "Web page files (*.html)|*.html";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            toolStripStatusLabel2.Text = "Программа запущена";
        }

        // метод для закрытия приложения
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // метод для открытия html файла 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _openFile.ShowDialog();
            webBrowser1.Navigate(_openFile.FileName);

            // если файл был выбран
            if (_openFile.FileName != String.Empty)
            { 
                ConfigureUI();  // изменение UI
            }

            toolStripStatusLabel2.Text = "Сатрница загружена";
        }
        // изменение UI
        private void ConfigureUI()
        {
            this.Height = 600;
            panel1.Height = 150;

            _xCoordInput = new TextBox();
            _yCoordInput = new TextBox();
            _calculateButton = new Button();

            _aboutCoordOne = new Label();
            _aboutCoordTwo = new Label();

            panel1.Controls.Add(_xCoordInput);
            panel1.Controls.Add(_yCoordInput);
            panel1.Controls.Add(_calculateButton);
            panel1.Controls.Add(_aboutCoordOne);
            panel1.Controls.Add(_aboutCoordTwo);

            _aboutCoordOne.Text = "координата x";
            _aboutCoordTwo.Text = "координата y";

            _aboutCoordOne.Location = new Point(0, 25);
            _aboutCoordTwo.Location = new Point(0, 50);

            _xCoordInput.Location = new Point(75, 25);
            _yCoordInput.Location = new Point(75, 50);

            _calculateButton.Text = "определить";
            _calculateButton.Location = new Point(0, 75);
            _calculateButton.Click += _calculateButton_Click;

        }
        // мето для вычисления длины точки отностиельно центра окружности тестовой области
        private double CalcLength(double x,double y)
        {
            return Math.Sqrt((x - 1) * (x - 1) + (y - 0) * (y - 0));
        }
        // метод активирующийся при клике на кнопку для определения принадлежности точки 
        private void _calculateButton_Click(object sender, EventArgs e)
        {
            // если введённые данные преобразуемы в координаты точек
            if (double.TryParse(_xCoordInput.Text, out _xCoord) && double.TryParse(_yCoordInput.Text, out _yCoord))
            {
                // если точка принадлежит области
                if ((_yCoord <= -(_xCoord - 3)) && _yCoord >= (_xCoord - 3) && CalcLength(_xCoord, _yCoord) <= 2)
                {
                    // если точка принадлежит границе области
                    if (CalcLength(_xCoord, _yCoord) == 2 || _yCoord == -(_xCoord - 3) || _yCoord == (_xCoord - 3))
                    {
                        toolStripStatusLabel2.Text = "Точка лежит на границе";
                        MessageBox.Show("Точка лежит на границе");
                        return;
                    }

                    toolStripStatusLabel2.Text = "Точка входит в область";
                    MessageBox.Show("Точка входит в область");
                }
                // иначе точка не принадлдежит области
                else
                {
                    toolStripStatusLabel2.Text = "Точка не входит в область";
                    MessageBox.Show("Точка не входит в область");
                }
            }
            // иначе введённые данные не преобразуемы в координаты точки
            else
            {
                toolStripStatusLabel2.Text = "Некорректный ввод";
            }
        }
        
        // метод длявывода данных о создателе
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создатель: Рогожин Денис ПКсп-119 Вариант 7");
        }

    }
}
