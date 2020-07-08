# EVE Bitmap viewer 

This is a sample application for viewing bitmaps on the [Matrix Orbital 
EVE series Display](https://www.matrixorbital.com/ftdi-eve) using the [USB2SPI](https://www.matrixorbital.com/eve2-usb2spi-kit-a) module.

## Usage

- Start the application
- Select the display size of your module  
![](display_size.png)
- Click the Connect button
- Wait for the module to be detected  
![](connected.png)

If the display is detected properly you should now see the Matrix Orbital screen on your display.

Now you can drag any image file on to the application and it will appear on your display.

## Code

The sample application is written in C# with a DLL version of the [Matrix Orbital Eve Library](https://github.com/MatrixOrbital/EVE2-Library) to handle the communication with the module.

