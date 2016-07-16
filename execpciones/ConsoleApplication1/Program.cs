using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args) //metodo main no se puede marcar como asincrono
        {
            FileStream file = null;
            try
            {
                Console.WriteLine("Nuemro");
                int d = 1;
                int e = 1;//0 para probar la excepciòn div por cero       
                int R = d / e;

                file = new FileStream("./Miarchivo.bin", FileMode.OpenOrCreate);
                //procesarArchivo(file);//procesa en el hilo principal
                Console.WriteLine("Antes de procesar"); 
                procesarArchivoAsync(file);//procesa en un hilo independiente
                Console.WriteLine("Despues de procesar");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Div por cero");
            }
            catch (Exception)
            {
                Console.WriteLine("error");
                Console.ReadLine();
            }
            finally
            {
                //file.Close();
                Console.ReadLine();
            }
        }

        private static async void procesarArchivoAsync(FileStream paramFile)
        {
            Console.WriteLine("Va a escribir async - await");
            var msg = "Hola Mundo";
            var bytes = Encoding.UTF8.GetBytes(msg);
            for (int i = 0; i < 1000; i++)
            {
                await paramFile.WriteAsync(bytes, 0, bytes.Length);
            }
            Console.WriteLine("Termino de escribir async - await");
        }

        private static void procesarArchivo(FileStream paramFile)
        {
            Console.WriteLine("Va a escribir");
            var msg = "HOla mundo";
            var bytes = Encoding.UTF8.GetBytes(msg);
            for (int i = 0; i < 1000; i++)
            {
                paramFile.Write(bytes, 0, bytes.Length);
            }
            Console.WriteLine("Termino de escribir");
        }
    }
}

