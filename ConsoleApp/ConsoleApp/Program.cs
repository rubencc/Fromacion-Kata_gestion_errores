using ConsoleApp.Handler;
using ConsoleApp.TimeWrapper;
using ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalService.Time;

namespace ConsoleApp
{
    public class Program
    {

        private static SyncApp.Handler.SyncAbstract synchandler;

        public static void Main(string[] args)
        {

            synchandler = new SyncHandlerImplementation(new TimeServiceWrapper());
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            Console.WriteLine("Los satelites GPS no responden. Se va a intentar la sincronizacion con las estaciones en tierra.");
            Console.WriteLine("Esta operacion puede tardar unos minutos. En caso de fallo vuelva a intentarlo.");
            Console.WriteLine("Si la ISS no se sincroniza cada 4 horas podria perder su orbita segura.");
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            while (key.Key != ConsoleKey.Y)
            {
                Sync();
                ShowZones();
                Console.WriteLine("¿Desea abandonar la aplicacion de sincronizacion y que la ISS se haga polvo contra la atmosfera?");
                Console.WriteLine("Pulse Y para salir. Pulse cualquier otra tecla para vivir otro dia.");
                key = Console.ReadKey();
                Console.Clear();
            }

            ByeBye();

        }

        private static void ShowZones()
        {
            Console.WriteLine("Hora en formato ruso: {0}", synchandler.GetTimeForРоскосмос());
            Console.WriteLine("Hora en formato japones: {0}", synchandler.GetTimeForJAXA());
            try
            {
                Console.WriteLine("Hora en formato usa: {0}", synchandler.GetTimeForNASA());
                Console.WriteLine("Hora en formato esa: {0}", synchandler.GetTimeForESA());
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Fallo al conseguir la hora");
            }
        }

        private static void Sync()
        {
            bool syncronized = false;
            int percentage = 0;
            int m = 0;

            while (syncronized == false)
            {

                for (int i = 0; i < 100; i++)
                {
                    syncronized = synchandler.SyncWithExternalClock();

                    if (syncronized)
                    {
                        Console.WriteLine("Sincronizando, espere: " + percentage + "% - Fallos {0}", m);
                        System.Threading.Thread.Sleep(75);
                        percentage += 1;
                        Console.Clear();
                    }
                    else
                    {
                        m++;
                        syncronized = synchandler.SyncWithExternalClock();

                        if (syncronized)
                        {
                            percentage += 1;
                        }
                        else
                        {
                            m++;
                            i--;
                        }

                        //Console.WriteLine("Sincronizando, espere: " + +percentage + "%");
                        //Console.WriteLine("Fallo en la sincronizacion. Pulse cualquier tecla para repetir.");
                        //percentage = 0;
                        //Console.ReadKey();
                        //Console.Clear();
                        //break;
                    }

                }

            }

            Console.WriteLine("Sincronizado a las {0}", new DateTime(synchandler.LastSyncTime));
        }

        private static void ByeBye()
        {
            Console.WriteLine(Messages.ByeByeES());
            Console.WriteLine();
            Console.WriteLine(" ----------------------- ");
            Console.WriteLine();
            Console.WriteLine(Messages.ByeByeEN());
            Console.ReadKey();
        }
    }
}
