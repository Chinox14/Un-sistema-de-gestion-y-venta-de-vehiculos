using System;
using GestionVehiculos.Servicios;

namespace GestionVehiculos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GestorFlota gestor = new GestorFlota();
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1: gestor.RegistrarVehiculo(); break;
                    case 2: gestor.MostrarTodos(); break;
                    case 3: gestor.BuscarVehiculo(); break;
                    case 4: gestor.MostrarPorEstado("Disponible"); break;
                    case 5: gestor.MostrarPorEstado("Rentado"); break;
                    case 6: gestor.CambiarEstado(); break;
                    case 7: gestor.VehiculoMayorKilometraje(); break;
                    case 8: gestor.PrecioPromedioRenta(); break;
                    case 9: gestor.ContarPorTipo(); break;
                    case 10: gestor.RealizarMantenimiento(); break;
                    case 11: Console.WriteLine("\nSaliendo del sistema. ¡Hasta pronto!"); break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Elija un número entre 1 y 11.");
                        Console.ResetColor();
                        break;
                }

                if (opcion != 11)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                }
            } while (opcion != 11);
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("===== SISTEMA DE RENTA DE VEHÍCULOS =====");
            Console.WriteLine("========================================\n");
            Console.WriteLine("1. Registrar vehículo");
            Console.WriteLine("2. Mostrar todos los vehículos");
            Console.WriteLine("3. Buscar vehículo por código");
            Console.WriteLine("4. Mostrar vehículos disponibles");
            Console.WriteLine("5. Mostrar vehículos rentados");
            Console.WriteLine("6. Cambiar estado");
            Console.WriteLine("7. Vehículo con mayor kilometraje");
            Console.WriteLine("8. Calcular precio promedio de renta");
            Console.WriteLine("9. Cantidad de vehículos por tipo");
            Console.WriteLine("10. Realizar mantenimiento");
            Console.WriteLine("11. Salir\n");
        }

        public static int LeerEntero(string mensaje)
        {
            int valor;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada no válida. Ingrese un número entero: ");
                Console.ResetColor();
            }
            return valor;
        }
    }
}
