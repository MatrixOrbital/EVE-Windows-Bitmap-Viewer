using EVEBitmapViewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EVEBitmapViewer
{
    public partial class Form1 : Form
    {
        void MakeScreen_MatrixOrbital(int DotSize)
        {
            EVE.Send_CMD(EVE.CMD_DLSTART);                //Start a new display list
            EVE.Send_CMD(EVE.VERTEXFORMAT(0));                //setup VERTEX2F to take pixel coordinates
            EVE.Send_CMD(EVE.CLEAR_COLOR_RGB(0, 0, 0));   //Determine the clear screen color
            EVE.Send_CMD(EVE.CLEAR(1, 1, 1));             //Clear the screen and the curren display list
            EVE.Send_CMD(EVE.COLOR_RGB(26, 26, 192));     // change colour to blue
            EVE.Send_CMD(EVE.POINT_SIZE(DotSize * 16));   // set point size to DotSize pixels. Points = (pixels x 16)
            EVE.Send_CMD(EVE.BEGIN(EVE.POINTS));          // start drawing point
            EVE.Send_CMD(EVE.TAG(1));                     // Tag the blue dot with a touch ID
            EVE.Send_CMD(EVE.VERTEX2F(EVE.Display_Width()/2, EVE.Display_Height()/2));  // place blue point
            EVE.Send_CMD(EVE.END());                      // end drawing point
            EVE.Send_CMD(EVE.COLOR_RGB(255, 255, 255));   //Change color to white for text

            EVE.Cmd_Text((ushort)(EVE.Display_Width()/2), (ushort)(EVE.Display_Height()/2), 30, EVE.OPT_CENTER, " MATRIX         ORBITAL"); //Write text in the center of the screen

            EVE.Send_CMD(EVE.DISPLAY());                     //End the display list
            EVE.Send_CMD(EVE.CMD_SWAP);                      //Swap commands into RAM
            EVE.UpdateFIFO();                                //Trigger the CoProcessor to start processing the FIFO
        }

        void UploadToRam(UInt32 address, byte[] data)
        {
            GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();
            int size = data.Length;
            int blockSize = Math.Min(1024 * 16, data.Length);
            while (blockSize > 0)
            {
                EVE.StartCoProTransfer(address, 0);
                EVE.EVE_SPI_WriteBuffer(pointer, (uint)blockSize);
                EVE.EVE_SPI_Disable();                 
                size = size - blockSize;
                address = (uint)(address + blockSize);
                pointer = new IntPtr(pointer.ToInt64() + blockSize);
                blockSize = Math.Min(size, blockSize);
            }
            pinnedArray.Free();
        }

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragDrop += Form1_DragDrop;
            this.DragEnter += Form1_DragEnter;
            pictureBox1.Parent = label1;
            pictureBox1.BackColor = Color.Transparent;
            string PreviousDisplay = Settings.Default.Display; // Setting the datasource will reset this. 
            cbModel.ComboBox.DataSource = EVE.Displays;
            var disp = EVE.Displays.Where(x => x.description == PreviousDisplay).FirstOrDefault();
            if (disp != null)
            {
                cbModel.SelectedItem = disp;
            }
            cbScale.Text = Settings.Default.Scale;
            SetConnectButton();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats(true);
            if (IsEveConnected)
            {
                if (e.Data.GetDataPresent(DataFormats.Bitmap) || 
                    e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        enum PictureScale
        {
            Stretch,
            Proportional,
            None
        }
        void DisplayImage(Image img)
        {
            bool halfres = false;
            this.pictureBox1.Image = null;
            if (!CurrentBridgeDetectedState)
                return;
            int height = EVE.Display_Height();
            float height_fact = 1.0f;
            // Half the resolution if the bitmap does not fit in RAM 
            if (EVE.Display_Width() * EVE.Display_Height() *2 > 1024*1024)
            {
                height = height / 2;
                halfres = true;
                height_fact = 0.5f;
            }
            Bitmap source = new Bitmap(EVE.Display_Width(), height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            PictureScale scale = (PictureScale)Enum.Parse(typeof(PictureScale), cbScale.Text);
            using (Graphics g = Graphics.FromImage(source))
            {
                switch(scale)
                {
                    case PictureScale.Proportional:
                        {
                            // Scale along size the longest axis
                            if (img.Width > img.Height)
                            {
                                float fact = (float)source.Width / (float)img.Width;
                                float w = source.Width;
                                float h = img.Height * fact * height_fact;
                                float y = (source.Height - h) / 2;
                                g.Clear(Color.Black);
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, y, w, h);
                            }
                            else
                            {
                                float fact = (float)source.Height / (float)img.Height;
                                float w = img.Width * fact;
                                float h = source.Height * height_fact;
                                float x = (source.Width- w) / 2;
                                g.Clear(Color.Black);
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, x, 0, w, h);
                            }
                            break;
                        }
                    case PictureScale.Stretch:
                        {
                            float fact = ((float)EVE.Display_Width()) / source.Width;
                            float w = source.Width * fact;
                            float h = source.Height * fact;
                            g.Clear(Color.Black);
                            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                            g.DrawImage(img, 0, 0, w, h);
                            break;
                        }
                    case PictureScale.None:
                        g.Clear(Color.Black);
                        g.DrawImage(img, 0, 0);
                        break;
                }
            }
            if (halfres)
            {
                Bitmap doubleSource = new Bitmap(source.Width, source.Height * 2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(doubleSource))
                {
                    g.Clear(Color.Black);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(source, 0, 0, source.Width, source.Height * 2);
                }
                pictureBox1.Image = doubleSource;
            }
            else
            {
                pictureBox1.Image = source;
            }
            //Creat a 16 bit copy of it using the graphics class
            Bitmap dest = new Bitmap(EVE.Display_Width(), height, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            using (Graphics g = Graphics.FromImage(dest))
            {
                g.DrawImageUnscaled(source, 0, 0);
            }
            //Get a copy of the byte array you need to send to your tft
            Rectangle r = new Rectangle(0, 0, dest.Width, dest.Height);
            BitmapData bd = dest.LockBits(r, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            byte[] TftData = new byte[dest.Width * dest.Height * 2];
            Marshal.Copy(bd.Scan0, TftData, 0, TftData.Length);
            dest.UnlockBits(bd);
            UploadToRam(0, TftData);
            EVE.Send_CMD(EVE.CMD_DLSTART);                //Start a new display list
            EVE.Send_CMD(EVE.CLEAR_COLOR_RGB(0, 0, 0));   //Determine the clear screen color
            EVE.Send_CMD(EVE.CLEAR(1, 1, 1));             //Clear the screen and the curren display list

            EVE.Send_CMD(EVE.BITMAP_SOURCE(0));
            EVE.Send_CMD(EVE.BITMAP_LAYOUT(EVE.RGB565, EVE.Display_Width() * 2, height));
            EVE.Send_CMD(EVE.BITMAP_LAYOUTH(EVE.Display_Width() * 2, height));
            EVE.Send_CMD(EVE.BEGIN(EVE.BITMAPS));                                  // Begin bitmap placement
            if (halfres)
            {
                EVE.Send_CMD(EVE.CMD_LOADIDENTITY);
                EVE.Send_CMD(EVE.CMD_SCALE);
                EVE.Send_CMD(1 << 16);
                EVE.Send_CMD(2 << 16);
                EVE.Send_CMD(EVE.CMD_SETMATRIX);
            }
            EVE.Send_CMD(EVE.VERTEX2II(0, 0, 0, 0));              // Define the placement position of the previously defined holding area.
            EVE.Send_CMD(EVE.END());                                           // end placing bitmaps

            EVE.Send_CMD(EVE.DISPLAY());                     //End the display list
            EVE.Send_CMD(EVE.CMD_SWAP);                      //Swap commands into RAM
            EVE.UpdateFIFO();                                //Trigger the CoProcessor to start processing the FIFO
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!IsEveConnected)
                return;
            // Handle FileDrop data.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    for (int i = 0; i < files.Count(); i++)
                    {
                        CurrentImage = Image.FromFile(files[i]);
                        DisplayImage(CurrentImage);
                        Application.DoEvents();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error loading image.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                try
                {
                    CurrentImage = (Image)e.Data.GetData(DataFormats.Bitmap);
                    DisplayImage(CurrentImage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            this.Invalidate();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }
        bool CurrentBridgeDetectedState = false;
        bool IsEveConnected = false;
        Image CurrentImage = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                bool newBridgeState = EVE.SPI_NumChannels() > 0;
                txtBridgeDetected.Text = newBridgeState ? "Yes" : "No";
                if (newBridgeState != CurrentBridgeDetectedState)
                {
                    CurrentBridgeDetectedState = newBridgeState;
                    if (!newBridgeState)
                    {
                        this.pictureBox1.Image = null;
                        if (IsEveConnected)
                        {
                            IsEveConnected = false;
                        }
                    }
                    else
                    {

                    }
                    SetConnectButton();

                }
            }
            catch 
            {
                txtBridgeDetected.Text = "Error";
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Display = cbModel.SelectedItem.ToString();
            Settings.Default.Save();
        }

        void SetConnectButton()
        {
            tbConnect.Text = (!IsEveConnected) ? "Connect" : "Disconnect";
            txtEveID.Text = (IsEveConnected) ? txtEveID.Text : "No";
            txtEveID.BackgroundImageLayout = ImageLayout.Stretch;
            txtEveID.BackgroundImage = new Bitmap(1, 1);
            var g = Graphics.FromImage(txtEveID.BackgroundImage);
            if (IsEveConnected)
                g.Clear(Color.PaleGreen);
            else
                g.Clear(Color.Transparent);
            tbConnect.Enabled = CurrentBridgeDetectedState;
            cbModel.Enabled = !IsEveConnected;
        }


        private void tbConnect_Click(object sender, EventArgs e)
        {
            if (!IsEveConnected)
            {
                EVE.EveDisplay disp = cbModel.SelectedItem as EVE.EveDisplay;
                int model = disp.id;
                int res = EVE.FT81x_Init(model, EVE.BOARD_EVE2, EVE.TOUCH_TPN);
                if (res > 1)
                {
                    string EVE_ID = string.Format("Unknown 0x{0:x8}",res);
                    switch(res)
                    {
                        case 0x00011008:
                            EVE_ID = "BT810";
                            break;
                        case 0x00011108:
                            EVE_ID = "BT811";
                            break;
                        case 0x00011208:
                            EVE_ID = "BT812";
                            break;
                        case 0x00011308:
                            EVE_ID = "BT813";
                            break;
                        case 0x00011508:
                            EVE_ID = "BT815";
                            break;
                        case 0x00011608:
                            EVE_ID = "BT816";
                            break;
                        case 0x00011708:
                            EVE_ID = "BT817";
                            break;
                        case 0x00011808:
                            EVE_ID = "BT818";
                            break;
                    }
                    txtEveID.Text = "Yes, " + EVE_ID;
                    MakeScreen_MatrixOrbital(24);
                    IsEveConnected = true;
                }
                else
                {
                    MessageBox.Show("EVE not detected.");
                    txtEveID.Text = "No";
                }
            }
            else
            {
                EVE.wr8(EVE.RAM_REG + EVE.REG_PWM_DUTY, 0);
                IsEveConnected = false;
            }
            SetConnectButton();
        }

        private void cbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentImage != null && IsEveConnected)
            {
                DisplayImage(CurrentImage);
            }
        }
    }
}
