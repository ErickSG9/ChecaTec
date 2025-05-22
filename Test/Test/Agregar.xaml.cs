using System;
using System.Linq;
using Xamarin.Forms;
using Test.Data;
using Test.Models;

namespace Test
{
    public partial class Agregar : ContentPage
    {
        private int receptorId;

        public Agregar(int idReceptor)
        {
            InitializeComponent();
            receptorId = idReceptor;
            Generar.SelectedIndexChanged += Generar_SelectedIndexChanged;
            CrearBloqueMedicamento(); // Crea al menos uno al inicio
        }

        private void Generar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = Generar.SelectedItem as string;

            ConsultaLayout.IsVisible = false;
            OperacionLayout.IsVisible = false;
            RecetaLayout.IsVisible = false;

            if (seleccion == "Consulta")
                ConsultaLayout.IsVisible = true;

            if (seleccion == "Operacion")
                OperacionLayout.IsVisible = true;

            if (seleccion == "Receta")
                RecetaLayout.IsVisible = true;
        }

        private async void OnGuardarCClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(motivoEntry.Text) || estadoCPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var nuevaConsulta = new Consulta
            {
                Fecha = DateTime.Today,
                Hora = DateTime.Now.ToString("hh:mm tt"),
                Motivo = motivoEntry.Text,
                Estado = estadoCPicker.SelectedItem.ToString(),
                PresionArterial = presionEntry.Text,
                FrecuenciaCardiaca = int.TryParse(frecuenciaEntry.Text, out int fc) ? fc : 0,
                Temperatura = double.TryParse(temperaturaEntry.Text, out double temp) ? temp : 0.0,
                Observaciones = observacionesCEditor.Text,
                IdUsuario = receptorId,
                IdDoctor = App.UsuarioActual.IdUsuario
            };

            try
            {
                Database.InsertarConsulta(nuevaConsulta);
                await DisplayAlert("Éxito", "Consulta registrada correctamente.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la consulta.\n{ex.Message}", "OK");
            }
        }

        private async void OnGuardarOClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tipoEntry.Text) ||
                string.IsNullOrWhiteSpace(cirujanoEntry.Text) ||
                string.IsNullOrWhiteSpace(quirofanoEntry.Text) ||
                estadoOPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Completa todos los campos obligatorios.", "OK");
                return;
            }

            var nuevaOperacion = new Operacion
            {
                Fecha = DateTime.Today,
                Tipo = tipoEntry.Text,
                Cirujano = cirujanoEntry.Text,
                Hora = horaPicker.Time.ToString(@"hh\:mm") + (horaPicker.Time.Hours < 12 ? " AM" : " PM"),
                Quirofano = quirofanoEntry.Text,
                Estado = estadoOPicker.SelectedItem.ToString(),
                Observaciones = observacionesOEditor.Text,
                IdUsuario = receptorId
            };

            try
            {
                Database.InsertarOperacion(nuevaOperacion);
                await DisplayAlert("Éxito", "Operación registrada correctamente.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la operación.\n{ex.Message}", "OK");
            }
        }

        private void CrearBloqueMedicamento()
        {
            var stack = new StackLayout
            {
                Spacing = 10
            };

            // Medicamento
            stack.Children.Add(new Label { Text = "Medicamento", TextColor = Color.Black, FontSize = 20 });

            var medicamentoFrame = new Frame
            {
                CornerRadius = 15,
                Padding = 5,
                HasShadow = false,
                BorderColor = Color.Black,
                BackgroundColor = Color.White,
                Content = new Entry
                {
                    Placeholder = "Nombre del medicamento",
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.Black
                }
            };
            stack.Children.Add(medicamentoFrame);

            // Dosis
            stack.Children.Add(new Label { Text = "Dosis", TextColor = Color.Black, FontSize = 20 });

            var dosisFrame = new Frame
            {
                CornerRadius = 15,
                Padding = 5,
                HasShadow = false,
                BorderColor = Color.Black,
                BackgroundColor = Color.White,
                Content = new Entry
                {
                    Placeholder = "Dosis",
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.Black
                }
            };
            stack.Children.Add(dosisFrame);

            // Instrucciones
            stack.Children.Add(new Label { Text = "Instrucciones", TextColor = Color.Black, FontSize = 20 });

            var instruccionesFrame = new Frame
            {
                CornerRadius = 15,
                Padding = 5,
                HasShadow = false,
                BorderColor = Color.Black,
                BackgroundColor = Color.White,
                Content = new Entry
                {
                    Placeholder = "Instrucciones",
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.Black
                }
            };
            stack.Children.Add(instruccionesFrame);

            // Agregar al contenedor principal
            MedicamentosLayout.Children.Add(stack);
        }


        private void OnAgregarMedicamentoClicked(object sender, EventArgs e)
        {
            CrearBloqueMedicamento();
        }

        private async void OnGuardarRClicked(object sender, EventArgs e)
        {
            var bloques = MedicamentosLayout.Children.OfType<StackLayout>().ToList();

            if (bloques.Count == 0)
            {
                await DisplayAlert("Error", "Debe ingresar al menos un medicamento.", "OK");
                return;
            }

            var recetasFinal = bloques.Select(b =>
            {
                var entradas = b.Children
                    .OfType<Frame>()
                    .Select(f => f.Content)
                    .OfType<Entry>()
                    .ToList();

                if (entradas.Count < 3)
                    return null;

                return new
                {
                    Medicamento = entradas[0].Text?.Trim() ?? "",
                    Dosis = entradas[1].Text?.Trim() ?? "",
                    Instrucciones = entradas[2].Text?.Trim() ?? ""
                };
            })
            .Where(x => x != null && !string.IsNullOrWhiteSpace(x.Medicamento))
            .ToList();

            if (recetasFinal.Count == 0)
            {
                await DisplayAlert("Error", "Debe ingresar al menos un medicamento con información.", "OK");
                return;
            }

            var paciente = Database.GetPacientePorUsuario(receptorId);
            if (paciente == null)
            {
                await DisplayAlert("Error", "No se encontró el paciente para registrar la receta.", "OK");
                return;
            }

            string medicamentos = string.Join(" | ", recetasFinal.Select(x => x.Medicamento));
            string dosis = string.Join(" | ", recetasFinal.Select(x => x.Dosis));
            string instrucciones = string.Join(" | ", recetasFinal.Select(x => x.Instrucciones));

            var receta = new Receta
            {
                IdPaciente = paciente.IdPaciente,
                IdProfesional = App.UsuarioActual?.IdUsuario ?? 0,
                Medicamento = medicamentos,
                Dosis = dosis,
                Instrucciones = instrucciones,
                Nota = NotaEntry.Text?.Trim() ?? "",
                FechaEmision = DateTime.Today,
                Activa = true
            };

            try
            {
                Database.InsertarReceta(receta);
             

                await DisplayAlert("Éxito", "Receta registrada correctamente.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar la receta.\n{ex.Message}", "OK");
            }
        }
    }
}
