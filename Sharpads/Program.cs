using Sharpads.KeyHooks;
using Sharpads.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sharpads
{
    class Program
    {
        public static string localPlayer = "base+04020228,0,18,B8,";
        public static string localPlayer_cameraX = "140";

        public static EventHandler<EventArgs> mainLoop;

        static void Main(string[] args)
        {
            Process.Start("minecraft://");

            mem.openGame();
            new KeybindHandler();

            new KEYS(); // not needed

            Thread.Sleep(100);

            Thread uiApp = new Thread(() => {
                Render ui = new Render();
                Application.Run(ui);
            });
            uiApp.Start();

            Thread.Sleep(100);

            while (true)
            {
                try
                {
                    if (mem.isMinecraftFocused())
                    {
                        if (Render.handle.Opacity != 100)
                            Render.handle.Opacity = 100;
                        mainLoop.Invoke(null, new EventArgs());
                    }
                    else if (Render.handle.Opacity != 0)
                        Render.handle.Opacity = 0;
                }
                catch { }
            }
        }

        public static int fontSize = 28;
        public static Font textFont = new Font(new FontFamily("Arial"), fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        public static SolidBrush textColor = new SolidBrush(Color.FromArgb(255, 255, 255));

        public static string[] SPL_Compass = { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };

        public static void Update(object sender, PaintEventArgs e) // HexHandler is my fancy little memory byte system i use so i can be lazy :sunglasses:
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            HexHandler.ath(localPlayer_cameraX, 0); // Camera X

            e.Graphics.FillRectangle(textColor, new Rectangle(10, 10, 60, 60));

            // e.Graphics.FillRectangle(selectedBlue, 0, (guiSize * scale) * c, catWidth * scale, guiSize * scale);
            // e.Graphics.DrawString(category.name, textFont, quinary, 0, (guiSize * scale) * c);
        }
    }
}
