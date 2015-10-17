using System;
using System.Collections.Generic;
using System.IO;

namespace AnalizadorLexicosEstados.AnalizadorLexico
{
    public class AnalLex
    {

        private int numLinea = 1;
        private String lexema;
        private String[] reservadas = { "if", "while", "do", "for", "class", "else", "true", "double", "final", "String", "int" };
        private List<String> lstPalabrasReservadas;
        private char car;
        private int val;
        private StreamReader file;

        public AnalLex(String nombreArchivo)
        {
            file = new StreamReader(nombreArchivo);
            lstPalabrasReservadas = new List<string>();

            foreach (var item in reservadas)
            {
                lstPalabrasReservadas.Add(item);
            }

            lexema = "";

            val = file.Read();
            car = (Char)val;

            q0();

            Console.ReadLine();
        }

        private void q0()
        {                        
            if (car == ' ' || car == '\n' || car == '\r' || car == '\t')
            {
                q6();
            }

            if (Char.IsNumber(car))
            {
                q1();
            }

            if (Char.IsLetter(car))
            {
                q4();
            }

            if (car == '"')
            {
                q7();
            }

            if(car == '/')
            {
                q11();
            }

            switch (car)
            {
                case '\r': break;
                case '=':
                case '+':
                case '.':
                case '-':
                case '*':
                case '/':
                case '&':
                case '!':
                case '|':
                case ';': q9(); break;
                case '<':
                case '>':                                
                case '(':
                case ')':
                case ',':
                case '{': 
                case '}': q10(); break;
                default: break;
            }

        }

        private void q1()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (Char.IsNumber(car))
            {
                q1();
            }
            else if (car == '.')
            {
                q2();
            }
            else
            {
                Mostrar("NUMERO");
                q0();
            }            
        }

        private void q2()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (Char.IsNumber(car))
            {
                q3();
            }
        }

        private void q3()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (Char.IsNumber(car))
            {
                q3();
            }
            else
            {
                Mostrar("NUMERO");
                q0();
            }
        }

        private void q4()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (Char.IsNumber(car) || Char.IsLetter(car) || car == '$' || car == '_')
            {
                q4();
            }
            else
            {
                q5();
            }
        }

        private void q5()
        {
            if (lstPalabrasReservadas.Contains(lexema))
            {
                Mostrar("RESERVADA");
                q0();
            }
            else
            {
                Mostrar("ID");
                q0();
            }
        }

        public void q6()
        {
            val = file.Read();
            car = (Char)val;

            if (car == ' ' || car == '\n' || car == '\r' || car == '\t')
            {
                if (car == '\n')
                {
                    numLinea++;
                }

                q6();
            }
            else
            {
                q0();
            }

        }

        public void q7()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if(car == '"')
            {
                q8();
            }
            else 
            {
                q7();
            }            
        }

        private void q8()
        {
            lexema = lexema + car;

            Mostrar("CADENA");

            val = file.Read();
            car = (Char)val;

            q0();
        }

        private void q9()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            Mostrar("OPERADOR");
            q0();
        }

        private void q10()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            Mostrar("SIMBOLO");
            q0();
        }

        private void q11()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (car == '/')
            {
                q12();
            }

            if (car == '*')
            {
                q13();
            }
        }

        private void q12()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (car == '\n' || car == '\r')
            {
                Mostrar("COMENTARIO SENCILLO");
                q0();
            }
            else
            {
                q12();
            }
        }


        private void q13()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (car == '\n')
            {
                numLinea++;
            }

            if (car == '*')
            {
                q14();
            }
            else
            {               
                q13();                
            }
        }

        private void q14()
        {
            lexema = lexema + car;

            val = file.Read();
            car = (Char)val;

            if (car == '/')
            {
                Mostrar("COMENTARIO MULTIPLE");

                val = file.Read();
                car = (Char)val;

                q0();
            }
            else
            {
                q13();
            }

        }

        private void Mostrar(String msg)
        {
            Console.WriteLine($"{numLinea} : {msg} ({lexema})");
            lexema = "";
        }

    }
}
