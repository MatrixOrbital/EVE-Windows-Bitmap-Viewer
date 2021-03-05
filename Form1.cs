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
            EVE.Send_CMD(EVE.VERTEXFORMAT(0));            //setup VERTEX2F to take pixel coordinates
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
            cbModel.Text = Settings.Default.Display;
            SetConnectButton(true);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap) || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void DisplayImage(Image img)
        {
            bool halfres = false;
            this.pictureBox1.Image = null;
            if (!CurrentState)
                return;
            int height = EVE.Display_Height();
            /* 10.1 uses too much ram for the buffer available in the EVE */
            if (EVE.Display_Width() == 1280 && EVE.Display_Height() == 800)
            {
                height = height / 2;
                halfres = true;
            }
            Bitmap source = new Bitmap(EVE.Display_Width(), height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(source))
            {
                float fact = ((float)EVE.Display_Width()) / source.Width;
                float w = source.Width * fact;
                float h = source.Height * fact;
                g.Clear(Color.Black);
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.DrawImage(img, 0, 0,w,h);
            }
            pictureBox1.Image = source;
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
            // Handle FileDrop data.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    DisplayImage(Image.FromFile(files[0]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                try
                {
                    DisplayImage((Image)e.Data.GetData(DataFormats.Bitmap));
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
        bool CurrentState = false;
        bool ConnectEnable = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ConnectEnable)
            {
                return;
            }
            try
            {
                bool newState = EVE.SPI_NumChannels() > 0;
                if (newState != CurrentState)
                {
                    if (newState)
                    {
                        int model = cbModel.SelectedIndex+1; 
                        int res = EVE.FT81x_Init(model, model == EVE.DISPLAY_101 ? EVE.BOARD_EVE4 : EVE.BOARD_EVE2, EVE.TOUCH_TPN);
                        if (res != 0)
                        {
                            SetConnectButton(false);
                            CurrentState = newState;
                            txtBridgeDetected.Text =  "Yes";
                            MakeScreen_MatrixOrbital(24);
                        }
                        else
                        {
                            txtBridgeDetected.Text = "Scanning";
                        }
                    }
                    else
                    {
                        this.pictureBox1.Image = null;
                        txtBridgeDetected.Text = "No";
                        CurrentState = newState;
                        SetConnectButton(true);
                    }
                }
                else if (newState == false)
                {
                    this.pictureBox1.Image = null;
                    txtBridgeDetected.Text = "No";
                    CurrentState = false;
                    SetConnectButton(true);
                }
            }
            catch
            {
                txtBridgeDetected.Text = "Error";
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Display = cbModel.Text;
            Settings.Default.Save();
            CurrentState = false;
        }

        void SetConnectButton(bool enableConnect)
        {
            ConnectEnable = !enableConnect;
            tbConnect.Text = enableConnect ? "Connect" : "Disconnect";
            cbModel.Enabled = enableConnect;
            CurrentState = false;
        }


        private void tbConnect_Click(object sender, EventArgs e)
        {
            SetConnectButton(ConnectEnable);
        }
    }
}
