namespace MiCrud5834249
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editDistanciaId;
        public MainPage(LocalDbService dbservice)
        {
            InitializeComponent();
            _dbService = dbservice;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetDistancia());
        }

        private async void CalcularBtn_Clicked(object sender, EventArgs e)
        {
            if(int.TryParse(EntrX1.Text, out int x1) && int.TryParse(EntrX2.Text, out int x2)
                && int.TryParse(EntrY1.Text, out int y1) && int.TryParse(EntrY2.Text, out int y2))
            {
                 //Propiedades para realizar la formula para calcular
                double sumaxy;
                double restax;
                double restay;
                double cuadradox;
                double cuadradoy;
                double distacia;

                restax = x1 - x2;
                restay = y1 - y2;
                cuadradox = Math.Pow(restax, 2);
                cuadradoy = Math.Pow(restay, 2);
                sumaxy = cuadradox + cuadradoy;
                distacia = Math.Sqrt(sumaxy);

                labelDistancia.Text = distacia.ToString();

                if (_editDistanciaId == 0)
                {
                    // Agrega cliente
                    await _dbService.Create(new Distancia
                    {
                        X1 = EntrX1.Text,
                        X2 = EntrX2.Text,
                        Y1 = EntrY1.Text,
                        Y2 = EntrY2.Text,
                        Distanciaa = distacia.ToString()  // Guarda la distancia como string
                    });
                }
                else
                {
                    // Edita cliente
                    await _dbService.Update(new Distancia
                    {
                        Id = _editDistanciaId,
                        X1 = EntrX1.Text,
                        X2 = EntrX2.Text,
                        Y1 = EntrY1.Text,
                        Y2 = EntrY2.Text,
                        Distanciaa = distacia.ToString()  // repetir el proceso de guardar la distancia como tipo cadena
                    });
                    _editDistanciaId = 0;
                }


                // Limpiar los campos
                EntrX1.Text = string.Empty;
                EntrX2.Text = string.Empty;
                EntrY1.Text = string.Empty;
                EntrY2.Text = string.Empty;
                labelDistancia.Text = string.Empty;

                // Actualizar los resultados
                listview.ItemsSource = await _dbService.GetDistancia();
            }

            else
            {
                //Mostrar un mensaje de error en caso de haber ingresado datos invalidos
                await DisplayAlert("Error", "Por favor ingrese números válidos.", "OK");
            }
        } 
        

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var distancia = (Distancia)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editDistanciaId = distancia.Id;
                    EntrX1.Text = distancia.X1;
                    EntrX2.Text = distancia.X2;
                    EntrY1.Text = distancia.Y1;
                    EntrY2.Text = distancia.Y2;
                    labelDistancia.Text = distancia.Distanciaa;
                    break;

                case "Delete":
                    await _dbService.Delete(distancia);
                    listview.ItemsSource = await _dbService.GetDistancia();
                    break;
            }
        }
    }

}
