using System;
using System.Windows.Forms;
using ScottPlot;


//  OpenBCI'nin Arduino ile çalışan EEG cihazı, sensörlerden gelen sinyalleri 
//  okur ve bu sinyalleri bir bilgisayara veya başka bir cihaza aktarır. 
//  Bu verileri okumak ve grafik olarak çizmek için birkaç yol vardır. 
//  C# kullanarak, öncelikle Arduino'ya bağlı bir seri bağlantı açmanız gerekecektir.

//  Aşağıdaki kaynaklar, C# ve Arduino arasında seri bağlantı kurmak 
//  ve veri okumak için farklı yaklaşımları göstermektedir:
//  https://www.c-sharpcorner.com/article/arduino-and-c-sharp-communication-via-serial-port/
//  https://www.instructables.com/Reading-From-Arduino-to-C-Sharp/

//  Seri bağlantıyı başarılı bir şekilde kurduktan sonra, 
//  sensörlerden gelen verileri okumak için bir döngü kullanabiliriz. 
//  Bu verileri grafik olarak çizmek için C#'da çeşitli 
//  grafik kütüphaneleri mevcuttur. 
//  Bazı popüler grafik kütüphaneleri şunlardır:
//  https://github.com/oxyplot/oxyplot
//  https://github.com/swharden/ScottPlot
//  https://github.com/microsoft/InteractiveDataDisplay.WPF

//  Bu kütüphaneleri kullanarak sensörlerden gelen verileri okuyabilir 
//  ve grafik olarak çizebiliriz. Ancak, EEG sinyalleri genellikle 
//  çok düşük frekanslarda olduğu için, veri işleme teknikleri ve 
//  filtreleme yöntemleri kullanmamız gerekebilir.

//  The EEG device from OpenBCI is an Arduino-based device 
//  that reads signals from sensors and transfers these signals 
//  to a computer or other device. There are several ways to read 
//  and graph this data. To do this using C#, you'll first need to 
//  establish a serial connection to the Arduino.

//  The following resources demonstrate different approaches 
//  to establishing a serial connection between C# and Arduino and reading data:
//  https://www.c-sharpcorner.com/article/arduino-and-c-sharp-communication-via-serial-port/
//  https://www.instructables.com/Reading-From-Arduino-to-C-Sharp/

//  Once the serial connection is established successfully, 
//  we can use a loop to read data from the sensors. 
//  There are several graphing libraries available 
//  in C# for plotting this data. Some popular graphing libraries include:
//  https://github.com/oxyplot/oxyplot
//  https://github.com/swharden/ScottPlot
//  https://github.com/microsoft/InteractiveDataDisplay.WPF

//  Using these libraries, we can read data from the sensors and plot it graphically. 
//  However, since EEG signals are typically at very low frequencies, 
//  we may need to use data processing techniques and filtering methods


namespace BrainWaveMonitor
{
    public partial class Form1 : Form
    {
        private ScottPlot.Plot plt;
        private double[] data;
        private int index = 0;

        public Form1()
        {
            InitializeComponent();

            // create a new ScottPlot object and add it to the form
            plt = new ScottPlot.Plot();
            plt.Dock = DockStyle.Fill;
            this.Controls.Add(plt);

            // create some fake data to plot
            data = new double[1000];
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = rand.NextDouble();
            }

            // start the timer to update the plot every 50 milliseconds
            timer1.Interval = 50;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // add a new data point to the plot
            plt.PlotSignal(data[index]);
            index++;

            // remove the oldest data point from the plot
            if (index > data.Length - 1)
            {
                plt.AxisAuto();
                plt.RemoveLast();
                index = data.Length - 1;
            }

            // redraw the plot
            plt.Render();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
