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
        private List<string> listaPalabras = new List<string>() { "Jirafa", "Ascensor", "Fiesta", "Ahorcado", "Espectaculo" , "Chanclas",
        "Honor", "Guerra", "Hambre", "Hilo", "Programacion", "Pegaso", "Alfombra"};
        private string palabraAAdivinar;
        char[] caracteresPalabra;
        private TextBlock textBlockPalabra;
        int fallos = 4;
        int numeroGuiones;
        List<Button> listaBotones;
        public MainWindow()
        {
            InitializeComponent();
            AñadirLetraALista();
            CrearLetra();
            VisualizacionPalabraAAdivinar();
        }
        private void CrearLetra()
        {
            listaBotones = new List<Button>();
            foreach (string l in listaLetras)
            {
                Button boton = new Button();
                boton.Margin = new Thickness(5);
                boton.Content = l;
                boton.Tag = l;
                boton.Click += Button_Click;
                listaBotones.Add(boton);
                uniformGridLetras.Children.Add(boton);
            }
        }
        private void AñadirLetraALista()
        {
            for (int i = 65; i <= 90; i++)
            {
                listaLetras.Add(Convert.ToChar(i).ToString().ToUpper());
            }
        }
        private void VisualizacionPalabraAAdivinar()
        {
            Random seed = new Random();
            for (int i = 0; i < seed.Next(0, 14); i++)
                palabraAAdivinar = listaPalabras[i].ToUpper();

            numeroGuiones = palabraAAdivinar.Length;

            for (int i = 0; i < numeroGuiones; i++)
            {
                viewbox = new Viewbox();
                textBlockPalabra = new TextBlock();
                textBlockPalabra.Text += "_ ";
                textBlockPalabra.FontSize = 24;
                viewbox.Child = textBlockPalabra;
                wrapPanelPalabraAAdivinar.HorizontalAlignment = HorizontalAlignment.Center;
                wrapPanelPalabraAAdivinar.VerticalAlignment = VerticalAlignment.Center;
                wrapPanelPalabraAAdivinar.Children.Add(viewbox);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Viewbox viewbox;
            bool haFallado = true;
            Button boton = (Button)sender;
            string letra = boton.Tag.ToString();
            caracteresPalabra = palabraAAdivinar.ToCharArray();
            for (int i = 0; i < caracteresPalabra.Length; i++)
            {
                if (caracteresPalabra[i] == Convert.ToChar(letra))
                {
                    viewbox = (Viewbox)wrapPanelPalabraAAdivinar.Children[i];
                    ((TextBlock)viewbox.Child).Text = letra.ToString();
                    haFallado = false;
                }
            }
            if (haFallado)
            {
                fallos++;
                if (fallos < 10)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri("Assets/" + fallos + ".jpg", UriKind.Relative);
                    bi.EndInit();
                    imageAhorcado.Source = bi;
                }
                else
                    MessageBox.Show("Game over");
            }
            boton.IsEnabled = false;
        }

        private void nuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            wrapPanelPalabraAAdivinar.Children.Clear();
            fallos = 4;
            CambiarImagen();
            VisualizacionPalabraAAdivinar();
            foreach (Button boton in listaBotones)
            {
                boton.IsEnabled = true;
            }
        }
        private void CambiarImagen()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("Assets/" + fallos + ".jpg", UriKind.Relative);
            bi.EndInit();
            imageAhorcado.Source = bi;
        }

        private void rendirseButton_Click(object sender, RoutedEventArgs e)
        {
            wrapPanelPalabraAAdivinar.Children.Clear();
            TextBlock palabra = new TextBlock();
            palabra.Text = palabraAAdivinar;
            wrapPanelPalabraAAdivinar.Children.Add(palabra);
            
        }

        private void windowPrincipal_KeyUp(object sender, KeyEventArgs e)
        {
            bool haFallado = true;
            string tecla = Convert.ToString(e.Key);
            caracteresPalabra = palabraAAdivinar.ToCharArray();
            for (int i = 0; i < caracteresPalabra.Length; i++)
            {
                if (caracteresPalabra[i] == Convert.ToChar(tecla))
                {
                    ((TextBlock)wrapPanelPalabraAAdivinar.Children[i]).Text = tecla.ToString();
                    haFallado = false;
                }
            }
            if (haFallado)
            {
                fallos++;
                if (fallos < 10)
                {
                    CambiarImagen();
                }
                else
                    MessageBox.Show("Game over");
            }
        }
    }
}

