using SharpDX.Direct3D9;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleDirectXForms
{
    static class Program
    {
        static private void  DisplayThread()
        {
            RenderForm form = new RenderForm("SimpleCube");
            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Bounds = Screen.PrimaryScreen.Bounds;
            form.IsFullscreen = true;

            Device device = new Device(_d39, 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters(form.ClientSize.Width, form.ClientSize.Height));
            Direcr3dCubeDisplay display = new Direcr3dCubeDisplay(form, device);
            display.BuildCube();
            display.BuildEffect("MiniCube.fx");
            display.Run();
        }
        static private void DisplayTraingleThread()
        {
            RenderForm form = new RenderForm("SimpleTraingle");
            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Bounds = Screen.PrimaryScreen.Bounds;
            form.IsFullscreen = true;

            Device device = new Device(_d39, 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters(form.ClientSize.Width, form.ClientSize.Height));
            var display = new Direcr3dTriangleDisplay(form, device);
            display.BuildCube();
            display.BuildEffect("MiniCube.fx");
            display.Run();
        }
        private static Direct3D _d39 = new Direct3D();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Thread t1 = new Thread(new ThreadStart(DisplayThread));
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(DisplayTraingleThread));
            t2.Start();

            t1.Join();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
