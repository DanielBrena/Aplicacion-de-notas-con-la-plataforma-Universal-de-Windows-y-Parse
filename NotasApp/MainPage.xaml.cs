using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Parse;
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NotasApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ParseClient.Initialize("7ppSp3vsWgdAPzu5aHCMPdrvVYLFSsfzZmkay7gw", "Vw0gEJQUvwl0uh4JZdHvUtmGR0qT2KhfNFsM3Jcq");
            this.refrescar();
        }

        private async void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            ParseObject nota = new ParseObject("Nota");
            nota["titulo"] = this.txtTitulo.Text;
            nota["nota"] = this.getNota();
            await nota.SaveAsync();
            this.refrescar();
            this.limpiarCampos();
        }

        private String getNota()
        {
            String contenido = "";
            this.txtNota.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out contenido);
            return contenido;
        }
        private void limpiarCampos()
        {
            this.txtTitulo.Text = "";
            this.txtNota.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
        }

        private async void refrescar()
        {
            this.lista.Items.Clear();
            var query = ParseObject.GetQuery("Nota").OrderByDescending("createdAt");
            IEnumerable<ParseObject> resultados = await query.FindAsync();
            foreach(ParseObject p in resultados)
            {
                this.lista.Items.Add(p.Get<String>("titulo") + ": " + p.Get<String>("nota"));
            }
        }
    }
}
