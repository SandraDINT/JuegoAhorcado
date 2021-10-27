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
        "Honor", "Guerra", "Hambre", "Hilo", "Programacion", "Pegaso", "Alfombra", "HETERNOMASCLOIDEO"};
        private string palabraAAdivinar;
        char[] caracteresPalabra;
        private TextBlock textBlockPalabra;
        int fallos = 4;
        int numeroGuiones;
        List<Button> listaBotones;
        Viewbox viewBox;
        int contadorGanar = 0;
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
                Button boton = new Button {
                    Style = (Style)this.Resources["estiloBotonesLetras"]
                };
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
            for (int i = 0; i < seed.Next(0, listaPalabras.Count + 1); i++)
                palabraAAdivinar = listaPalabras[i].ToUpper();

            numeroGuiones = palabraAAdivinar.Length;

            for (int i = 0; i < numeroGuiones; i++)
            {
                textBlockPalabra = new TextBlock
                {
                    Style = (Style)this.Resources["estiloTextBlockLetras"]
                };
                textBlockPalabra.Text += "_ ";
                wrapPanelPalabraAAdivinar.HorizontalAlignment = HorizontalAlignment.Center;
                wrapPanelPalabraAAdivinar.VerticalAlignment = VerticalAlignment.Center;
                wrapPanelPalabraAAdivinar.Children.Add(textBlockPalabra);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool haFallado = true;
            Button boton = (Button)sender;
            string letra = boton.Tag.ToString();
            bool haGanado = false;
            caracteresPalabra = palabraAAdivinar.ToCharArray();
            for (int i = 0; i < caracteresPalabra.Length; i++)
            {
                if (caracteresPalabra[i] == Convert.ToChar(letra))
                {
                    viewBox = (Viewbox)wrapPanelPalabraAAdivinar.Children[i];
                    ((TextBlock)viewBox.Child).Text = letra.ToString();
                    haFallado = false;
                    contadorGanar++;
                    if (contadorGanar == palabraAAdivinar.Length)
                        haGanado = true;
                    if (haGanado)
                        MessageBox.Show("¡HAS GANADO!");
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
        private void HabilitarBotones()
        {
            foreach (Button boton in listaBotones)
            {
                boton.IsEnabled = true;
            }
        }

        private void nuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            wrapPanelPalabraAAdivinar.Children.Clear();
            ReiniciarFallos();
            CambiarImagen();
            VisualizacionPalabraAAdivinar();
            HabilitarBotones();
            ReiniciarContadorPalabrasAcertadas();
        }
        private void ReiniciarContadorPalabrasAcertadas()
        {
            contadorGanar = 0;
        }
        private void ReiniciarFallos()
        {
            fallos = 4;
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
            palabra.FontSize = 36;
            palabra.Text = palabraAAdivinar;
            wrapPanelPalabraAAdivinar.Children.Add(palabra);

        }

        private void windowPrincipal_KeyUp(object sender, KeyEventArgs e)
        {
            bool haGanado = false;
            Viewbox viewbox;
            bool haFallado = true;
            string tecla = e.Key.ToString();
            foreach (Button boton in listaBotones)
            {
                if (boton.Tag.Equals(tecla))
                    boton.IsEnabled = false;
            }

            caracteresPalabra = palabraAAdivinar.ToCharArray();
            for (int i = 0; i < caracteresPalabra.Length; i++)
            {
                if (caracteresPalabra[i] == Convert.ToChar(tecla))
                {
                    viewbox = (Viewbox)wrapPanelPalabraAAdivinar.Children[i];
                    ((TextBlock)viewbox.Child).Text = tecla.ToString();
                    haFallado = false;
                    contadorGanar++;
                    if (contadorGanar == palabraAAdivinar.Length)
                        haGanado = true;
                    if (haGanado)
                        MessageBox.Show("¡HAS GANADO!");
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
        }
    }
}

