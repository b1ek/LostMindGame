using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LostMind.Engine.UI;
using LostMind.Engine.User;

namespace LostMind {
    class Benchmark {
        static void Main(string[] args) {
            UIButton button = new UIButton("Bye");
            Console.SetBufferSize(120, 30);
            Viewport view = new Viewport(2, 0, Console.BufferWidth, Console.BufferHeight, true);
            view.DrawOrder = DrawOrder.Horizontal;
            view.AddElement(new UIButton("Hi", () => { UserNativeLib.displayMessage("Hi!!!"); }));
            view.AddElement(button);
            button.OnClick += () => { button.buttonText += "e"; view.Draw(); };
            view.DumpArea();
            view.Mainloop();
        }
    }
}
