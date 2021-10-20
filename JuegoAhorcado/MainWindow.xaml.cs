using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JuegoAhorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NUM_FILAS = 3;
        private const int NUM_COLUMNAS = 9;
        private List<string> listaLetras = new List<string>();
        private List<string> listaPalabras = new List<string>() {"Jirafa", "Ascensor", "Fiesta", "Ahorcado", "Espectaculo"};
        private string palabraAAdivinar;
        public MainWindow()
        {
            InitializeComponent();
            AñadirLetraALista();
            CrearLetra();
            VisualizacionPalabraAAdivinar();
        }
        private void CrearLetra()
        {
            foreach (string l in listaLetras)
            {
                Button boton = new Button();
                boton.Margin = new Thickness(5);
                boton.Content = l;
                uniformGridLetras.Children.Add(boton);
            }
        }
        private void AñadirLetraALista()
        {
            for (int i = 65; i <=90; i++)
            {
                listaLetras.Add(Convert.ToChar(i).ToString());
            }
        }
        private void VisualizacionPalabraAAdivinar()
        {
            int numeroGuiones;
            Random seed = new Random();
            for (int i = 0; i < seed.Next(0,6); i++)
                palabraAAdivinar = listaPalabras[i]; 

            numeroGuiones = palabraAAdivinar.Length;
            for (int i = 0; i < numeroGuiones; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text += "_ ";
                textBlock.FontSize = 24;
                wrapPanelPalabraAAdivinar.Children.Add(textBlock);
            }
        }
        
    }
}
