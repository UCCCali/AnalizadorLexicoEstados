using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicosEstados
{
    class Program
    {
        static void Main(string[] args)
        {
            AnalizadorLexico.AnalLex objAnalLex = new AnalizadorLexico.AnalLex(@"C:\log\Ejemplo2.txt");            
        }
    }
}
