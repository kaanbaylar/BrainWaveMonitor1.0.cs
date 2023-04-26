using System;
using System.Windows.Forms;
using ScottPlot;


//  Kod, bir beyin dalgas� monit�r� uygulamas� olu�turmak i�in 
//  ScottPlot k�t�phanesini kullanmaktad�r. 
//  Uygulama, bir cihaza ba�lanarak gelen verileri canl� 
//  olarak g�rselle�tirmektedir. Uygulama penceresi, 
//  ScottPlot grafi�i i�ermektedir ve bir "Ba�lan" d��mesi, 
//  "Ba�lant�y� Kes" d��mesi ve birka� di�er kontrol i�ermektedir. 
//  Ba�lan d��mesine t�kland���nda, uygulama belirtilen cihaza ba�lanacak 
//  ve canl� verileri g�r�nt�lemeye ba�layacakt�r. Ba�lant�y� Kes d��mesi, 
//  cihaz ba�lant�s�n� keser ve veri ak���n� durdurur.

//  The code uses the ScottPlot library to create a brainwave monitor application. 
//  The application visualizes the incoming data live by connecting to a device. 
//  The application window includes the ScottPlot graphic and includes a "Connect" button, 
//  "Disconnect" button, and several other controls. Clicking the connect button, 
//  the app will connect to the specified device and start displaying live data. 
//  The Disconnect button disconnects the device and stops the data flow.


namespace BrainWaveMonitor
{
public partial class Form1 : Form
{
private ScottPlot.Plot plt;
private int maxDataPoints = 1000;
private bool connectedToDevice = false;    
public Form1()
    {
        InitializeComponent();

        // create a new ScottPlot object and add it to the form
        plt = new ScottPlot.Plot();
        plt.Dock = DockStyle.Fill;
        this.Controls.Add(plt);

        // start the timer to update the plot every 50 milliseconds
        timer1.Interval = 50;
        timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        // add a new data point to the plot
        plt.PlotSignal(Math.Sin(DateTime.Now.Ticks / 10000000.0));

        // remove the oldest data point from the plot
        if (plt.GetSignalXs().Length > maxDataPoints)
        {
            plt.RemoveFirst();
        }

        // redraw the plot
        plt.AxisAuto();
        plt.Render();
    }

    private async void ConnectToDevice(string deviceName)
    {
        // code to connect to the selected device and open a stream for data

        while (connectedToDevice)
        {
            // read data from the stream
            double dataPoint = await ReadDataFromStreamAsync();

            // add the new data point to the plot
            plt.PlotSignal(dataPoint);

            // remove the oldest data point from the plot
            if (plt.GetSignalXs().Length > maxDataPoints)
            {
                plt.RemoveFirst();
            }

            // redraw the plot
            plt.AxisAuto();
            plt.Render();
        }
    }

    private async Task<double> ReadDataFromStreamAsync()
    {
        // code to read data from the device stream and return a double value
    }

    private void ConnectButton_Click(object sender, EventArgs e)
    {
        // code to connect to the selected device and open a stream for data

        connectedToDevice = true;
        ConnectButton.Enabled = false;
    }

    private void DisconnectButton_Click(object sender, EventArgs e)
    {
        // code to disconnect from the device and close the data stream

        connectedToDevice = false;
        ConnectButton.Enabled = true;
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




