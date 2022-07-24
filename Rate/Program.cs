using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("a = ");
                int a = int.Parse(Console.ReadLine());
                Console.Write("b = ");
                int b = int.Parse(Console.ReadLine());
                string text = $"{(b * 1.9021130325903 - a * 0.30901699437495) / (a * 0.98768834059514 - b * 0.31286893008046):#.#####}";
                System.Windows.Forms.Clipboard.SetText($"{(b * 1.9021130325903 - a * 0.30901699437495) / (a * 0.98768834059514 - b * 0.31286893008046):#.#####}");
            }
        }
    }
}
