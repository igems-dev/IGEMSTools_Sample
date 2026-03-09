namespace StraighterSample
{
    public partial class Form1 : Form
    {
        GSensor sensor;  // Represents the external sensor device

        public Form1()
        {
            InitializeComponent();

            // Initialize sensor instance
            sensor = new GSensor();

            // Subscribe to sensor events for connect/disconnect detection
            sensor.SensorFoundAgain += Sensor_FoundAgain;
            sensor.SensorLost += Sensor_Lost;
        }

        // Event handler: called when sensor is disconnected
        private void Sensor_Lost(object? sender, EventArgs e)
        {
            // Stop reading data when device is lost
            timer1.Enabled = false;
        }

        // Event handler: called when sensor is reconnected
        private void Sensor_FoundAgain(object? sender, EventArgs e)
        {
            // Attempt to reconnect to the sensor
            connectToSensor();
        }

        // Attempts to connect to the sensor
        private bool connectToSensor()
        {
            if (sensor.Connect())
            {
                sensor.SetFilter(0);      // Set filter to lowest frequency (0.125 Hz)
                Thread.Sleep(100);        // Allow some delay before starting reading

                timer1.Enabled = true;    // Enable periodic reading
                return true;
            }
            else
            {
                // Inform user if the sensor could not be found
                MessageBox.Show("Could not find IGEMS Straighter");
                timer1.Enabled = false;
                return false;
            }
        }

        // Periodic timer event: called on every tick to update UI with sensor values
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Read X and Y angles from the sensor
            sensor.GetXY(out double x, out double y);
            
            // Display angles in text boxes with 3 decimal places
            txtX.Text = x.ToString("F3");
            txtY.Text = y.ToString("F3"); 
        }

        // Event: called when the form is about to close
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure sensor connection is properly disposed
            if (sensor != null)
                sensor.Dispose();
        }

        // Handler for Connect button click
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Attempt to connect only if not already connected
            if (!sensor.Connected)
                connectToSensor();
        }

        // Handler for Reference (Zero) button click
        private void btnRef_Click(object sender, EventArgs e)
        {
            if (sensor.Connected)
            {
                // Set the current position as the new zero reference
                sensor.SetZero();
                Thread.Sleep(200);  // Delay to allow sensor to stabilize

                // Visually indicate successful zeroing by turning text green
                txtX.ForeColor = Color.Green;
                txtY.ForeColor = Color.Green;
            }
            else
            {
                MessageBox.Show("Not connected to IGEMS Straighter! Please connect first.");
            }
        }
    }
}
