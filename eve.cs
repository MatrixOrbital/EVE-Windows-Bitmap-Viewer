using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EVEBitmapViewer
{
    public class EVE
    {
        public const UInt16 OPT_CENTER = 1536;
        public const UInt16 OPT_CENTERX = 512;
        public const UInt16 OPT_CENTERY = 1024;
        public const UInt16 OPT_FLASH = 64;
        public const UInt16 OPT_FLAT = 256;
        public const UInt16 OPT_FULLSCREEN = 8;
        public const UInt16 OPT_MEDIAFIFO = 16;
        public const UInt16 OPT_MONO = 1;
        public const UInt16 OPT_NOBACK = 4096;
        public const UInt16 OPT_NODL = 2;
        public const UInt16 OPT_NOHANDS = 49152;
        public const UInt16 OPT_NOHM = 16384;
        public const UInt16 OPT_NOPOINTER = 16384;
        public const UInt16 OPT_NOSECS = 32768;
        public const UInt16 OPT_NOTEAR = 4;
        public const UInt16 OPT_NOTICKS = 8192;
        public const UInt16 OPT_RGB565 = 0;
        public const UInt16 OPT_RIGHTX = 2048;
        public const UInt16 OPT_SIGNED = 256;
        public const UInt16 OPT_SOUND = 32;


        public const UInt32 BITMAPS                   = 1;
        public const UInt32 POINTS                    = 2;
        public const UInt32 LINES                     = 3;
        public const UInt32 LINE_STRIP                = 4;
        public const UInt32 EDGE_STRIP_R              = 5;
        public const UInt32 EDGE_STRIP_L              = 6;
        public const UInt32 EDGE_STRIP_A              = 7;
        public const UInt32 EDGE_STRIP_B              = 8;
        public const UInt32 RECTS                     = 9;

        public const int ARGB1555 = 0;
        public const int L1 = 1;
        public const int L4 = 2;
        public const int L8 = 3;
        public const int RGB332 = 4;
        public const int ARGB2 = 5;
        public const int ARGB4 = 6;
        public const int RGB565 = 7;
        public const int TEXT8X8 = 9;
        public const int TEXTVGA = 10;
        public const int BARGRAPH = 11;
        public const int PALETTED565 = 14;
        public const int PALETTED4444 = 15;
        public const int PALETTED8 = 16;
        public const int L2 = 17;

        // Bitmap Layout Format Definitions - BT81X Series Programming Guide Section 4.6;
        public const int COMPRESSED_RGBA_ASTC_4x4_KHR = 37808;  // 8.00;
        public const int COMPRESSED_RGBA_ASTC_5x4_KHR = 37809;  // 6.40;
        public const int COMPRESSED_RGBA_ASTC_5x5_KHR = 37810;  // 5.12;
        public const int COMPRESSED_RGBA_ASTC_6x5_KHR = 37811;  // 4.27;
        public const int COMPRESSED_RGBA_ASTC_6x6_KHR = 37812;  // 3.56;
        public const int COMPRESSED_RGBA_ASTC_8x5_KHR = 37813;  // 3.20;
        public const int COMPRESSED_RGBA_ASTC_8x6_KHR = 37814;  // 2.67;
        public const int COMPRESSED_RGBA_ASTC_8x8_KHR = 37815;  // 2.56;
        public const int COMPRESSED_RGBA_ASTC_10x5_KHR = 37816;  // 2.13;
        public const int COMPRESSED_RGBA_ASTC_10x6_KHR = 37817;  // 2.00;
        public const int COMPRESSED_RGBA_ASTC_10x8_KHR = 37818;  // 1.60;
        public const int COMPRESSED_RGBA_ASTC_10x10_KHR = 37819;  // 1.28;
        public const int COMPRESSED_RGBA_ASTC_12x10_KHR = 37820;  // 1.07;
        public const int COMPRESSED_RGBA_ASTC_12x12_KHR = 37821; // 0.89;

        // Bitmap Parameters;
        public const int REPEAT = 1;
        public const int BORDER = 0;
        public const int NEAREST = 0;
        public const int BILINEAR = 1;

        public const int DISPLAY_70 = 1;
        public const int DISPLAY_50 = 2;
        public const int DISPLAY_43 = 3;
        public const int DISPLAY_39 = 4;
        public const int DISPLAY_38 = 5;
        public const int DISPLAY_35 = 6;
        public const int DISPLAY_29 = 7;

        public const int BOARD_EVE2 = 1;
        public const int BOARD_EVE3 = 2;

        public const int TOUCH_TPN = 0;
        public const int TOUCH_TPR = 1;
        public const int TOUCH_TPC = 2;

        public const UInt32 CMD_APPEND = 0xFFFFFF1E;
        public const UInt32 CMD_BGCOLOR = 0xFFFFFF09;
        public const UInt32 CMD_BUTTON = 0xFFFFFF0D;
        public const UInt32 CMD_CALIBRATE = 0xFFFFFF15; // 4294967061UL;
        public const UInt32 CMD_CLOCK = 0xFFFFFF14;
        public const UInt32 CMD_COLDSTART = 0xFFFFFF32;
        public const UInt32 CMD_CRC = 0xFFFFFF18;
        public const UInt32 CMD_DIAL = 0xFFFFFF2D;
        public const UInt32 CMD_DLSTART = 0xFFFFFF00;
        public const UInt32 CMD_FGCOLOR = 0xFFFFFF0A;
        public const UInt32 CMD_GAUGE = 0xFFFFFF13;
        public const UInt32 CMD_GETMATRIX = 0xFFFFFF33;
        public const UInt32 CMD_GETPROPS = 0xFFFFFF25;
        public const UInt32 CMD_GETPTR = 0xFFFFFF23;
        public const UInt32 CMD_GRADCOLOR = 0xFFFFFF34;
        public const UInt32 CMD_GRADIENT = 0xFFFFFF0B;
        public const UInt32 CMD_INFLATE = 0xFFFFFF22;
        public const UInt32 CMD_INFLATE2 = 0xFFFFFF50;
        public const UInt32 CMD_INTERRUPT = 0xFFFFFF02;
        public const UInt32 CMD_KEYS = 0xFFFFFF0E;
        public const UInt32 CMD_LOADIDENTITY = 0xFFFFFF26;
        public const UInt32 CMD_LOADIMAGE = 0xFFFFFF24;
        public const UInt32 CMD_LOGO = 0xFFFFFF31;
        public const UInt32 CMD_MEDIAFIFO = 0xFFFFFF39;
        public const UInt32 CMD_MEMCPY = 0xFFFFFF1D;
        public const UInt32 CMD_MEMCRC = 0xFFFFFF18;
        public const UInt32 CMD_MEMSET = 0xFFFFFF1B;
        public const UInt32 CMD_MEMWRITE = 0xFFFFFF1A;
        public const UInt32 CMD_MEMZERO = 0xFFFFFF1C;
        public const UInt32 CMD_NUMBER = 0xFFFFFF38;
        public const UInt32 CMD_PLAYVIDEO = 0xFFFFFF3A;
        public const UInt32 CMD_PROGRESS = 0xFFFFFF0F;
        public const UInt32 CMD_REGREAD = 0xFFFFFF19;
        public const UInt32 CMD_ROTATE = 0xFFFFFF29;
        public const UInt32 CMD_SCALE = 0xFFFFFF28;
        public const UInt32 CMD_SCREENSAVER = 0xFFFFFF2F;
        public const UInt32 CMD_SCROLLBAR = 0xFFFFFF11;
        public const UInt32 CMD_SETBITMAP = 0xFFFFFF43;
        public const UInt32 CMD_SETFONT = 0xFFFFFF2B;
        public const UInt32 CMD_SETMATRIX = 0xFFFFFF2A;
        public const UInt32 CMD_SETROTATE = 0xFFFFFF36;
        public const UInt32 CMD_SKETCH = 0xFFFFFF30;
        public const UInt32 CMD_SLIDER = 0xFFFFFF10;
        public const UInt32 CMD_SNAPSHOT = 0xFFFFFF1F;
        public const UInt32 CMD_SPINNER = 0xFFFFFF16;
        public const UInt32 CMD_STOP = 0xFFFFFF17;
        public const UInt32 CMD_SWAP = 0xFFFFFF01;
        public const UInt32 CMD_TEXT = 0xFFFFFF0C;
        public const UInt32 CMD_TOGGLE = 0xFFFFFF12;
        public const UInt32 CMD_TRACK = 0xFFFFFF2C;
        public const UInt32 CMD_TRANSLATE = 0xFFFFFF27;
        public const UInt32 CMD_VIDEOFRAME = 0xFFFFFF41;
        public const UInt32 CMD_VIDEOSTART = 0xFFFFFF40;
        public const UInt32 CMD_ROMFONT = 0xFFFFFF3F;
        // BT81X COMMANDS ;
        public const UInt32 CMD_FLASHERASE = 0xFFFFFF44;
        public const UInt32 CMD_FLASHWRITE = 0xFFFFFF45;
        public const UInt32 CMD_FLASHREAD = 0xFFFFFF46;
        public const UInt32 CMD_FLASHUPDATE = 0xFFFFFF47;
        public const UInt32 CMD_FLASHDETACH = 0xFFFFFF48;
        public const UInt32 CMD_FLASHATTACH = 0xFFFFFF49;
        public const UInt32 CMD_FLASHFAST = 0xFFFFFF4A;
        public const UInt32 CMD_FLASHSPIDESEL = 0xFFFFFF4B;
        public const UInt32 CMD_FLASHSPITX = 0xFFFFFF4C;
        public const UInt32 CMD_FLASHSPIRX = 0xFFFFFF4D;
        public const UInt32 CMD_FLASHSOURCE = 0xFFFFFF4E;
        public const UInt32 CMD_CLEARCACHE = 0xFFFFFF4F;
 
        public static UInt32 CLEAR_COLOR_RGB(byte red, byte green, byte blue)
        {
            // CLEAR_COLOR_RGB - FT-PG Section 4.23
            return (uint)(((uint)2 << 24) | (((red) & 255) << 16) | (((green) & 255) << 8) | (((blue) & 255) << 0));                                                        
        }

        public static UInt32 COLOR_RGB(byte red, byte green, byte blue)
        {
            // COLOR_RGB - FT-PG Section 4.28
            return (uint)((4 << 24) | (((red) & 255) << 16) | (((green) & 255) << 8) | (((blue) & 255) << 0));
        }

        public static UInt32 CLEAR(int c, int s, int t)
        {
            return (uint) ((38 << 24) | (((c) & 1) << 2) | (((s) & 1) << 1) | (((t) & 1) << 0));
        }

        public static UInt32 POINT_SIZE(int sighs) {
            return (uint)((13 << 24) | (((sighs) & 8191) << 0));
        }

        public static UInt32 BEGIN(uint PrimitiveTypeRef) {
            return (uint) ((31 << 24) | (((PrimitiveTypeRef) & 15) << 0));
        }

        public static UInt32 DISPLAY()
        {
            // DISPLAY - FT-PG Section 4.29
            return (0 << 24);                                                                                                                                            
        }
        public static UInt32 END()
        {
            return (33 << 24);
        }

        public static UInt32 TAG(int s) {
            return (uint) ((3 << 24) | (((s) & 255) << 0));
        }

        public static UInt32 VERTEX2II(int x, int y, int handle, int cell)
        {
            return (uint)((2 << 30) | (((x) & 511) << 21) | (((y) & 511) << 12) | (((handle) & 31) << 7) | (((cell) & 127) << 0));
        }

        public static UInt32 BITMAP_HANDLE(int handle) {
            return (uint)((5 << 24) | (((handle) & 31) << 0));
        }

        public static UInt32 BITMAP_SOURCE(UInt32 addr)
        {
            return (UInt32)((1 << 24) | (((addr) & 1048575) << 0));
        }

        public static UInt32 BITMAP_LAYOUT(int format, int linestride, int height)
        {
            return (UInt32)((7 << 24) | (((format) & 31) << 19) | (((linestride) & 1023) << 9) | (((height) & 511) << 0));
        }
        
        public static UInt32 BITMAP_LAYOUTH(int linestride, int height)
        {
            int stride = (linestride >> 10) & 3;
            height = (height >> 10) & 3;
            return (UInt32)((0x28 << 24) | (stride << 2)  | height);
        }
        
        const string EveDLL = @"eve.dll";

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern int FT81x_Init(int display, int board, int touch);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void Send_CMD(UInt32 data);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void UpdateFIFO();

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void Cmd_Text(UInt16 x, UInt16 y, UInt16 font, UInt16 options, string str);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void Cmd_SetBitmap(UInt32 addr, UInt16 fmt, UInt16 width, UInt16 height);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern Int32 Display_Width();

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern Int32 Display_Height();


        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern int SPI_GetNumChannels(ref UInt32 numChannels);

        public static int SPI_NumChannels()
        {
            UInt32 channels = 0;
            SPI_GetNumChannels(ref channels);
            return (int)channels;
        }

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void Wait4CoProFIFOEmpty();

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void HAL_SPI_Disable();

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void StartCoProTransfer(UInt32 address, byte reading);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void CoProWrCmdBuf(IntPtr buffer, UInt32 count);

        [DllImport(EveDLL, CharSet = CharSet.Ansi)]
        public static extern void HAL_SPI_WriteBuffer(IntPtr buffer, UInt32 count);



    }
}
