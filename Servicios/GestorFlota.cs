using System;
using GestionVehiculos.Interfaces;
using GestionVehiculos.Modelos;

namespace GestionVehiculos.Servicios
{
    public class GestorFlota
    {
        private const int CapacidadMaxima = 100;
        private readonly Vehiculo[] flota = new Vehiculo[CapacidadMaxima];
        private int cantidad = 0;

        // ---------- Utilidades de lectura segura ----------
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

        public static int LeerEnteroPositivo(string mensaje)
        {
            int valor = LeerEntero(mensaje);
            while (valor < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("El valor no puede ser negativo. ");
                Console.ResetColor();
                valor = LeerEntero(mensaje);
            }
            return valor;
        }

        public static double LeerDouble(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor) || valor < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada no válida. Ingrese un número decimal positivo: ");
                Console.ResetColor();
            }
            return valor;
        }

        public static string LeerTexto(string mensaje)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(texto))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("El campo no puede estar vacío. Intente de nuevo: ");
                Console.ResetColor();
                texto = Console.ReadLine();
            }
            return texto.Trim();
        }

        public static bool LeerSiNo(string mensaje)
        {
            Console.Write(mensaje + " (s/n): ");
            string r = Console.ReadLine()?.Trim().ToLower();
            while (r != "s" && r != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Responda 's' o 'n': ");
                Console.ResetColor();
                r = Console.ReadLine()?.Trim().ToLower();
            }
            return r == "s";
        }

        public static string LeerOpcion(string mensaje, params string[] opciones)
        {
            Console.WriteLine(mensaje);
            for (int i = 0; i < opciones.Length; i++)
                Console.WriteLine($"  {i + 1}. {opciones[i]}");
            int sel = LeerEntero("Seleccione: ");
            while (sel < 1 || sel > opciones.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Opción fuera de rango (1-{opciones.Length}). ");
                Console.ResetColor();
                sel = LeerEntero("Seleccione: ");
            }
            return opciones[sel - 1];
        }

        // ---------- Búsqueda ----------
        private Vehiculo BuscarPorCodigo(string codigo)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (flota[i] != null && flota[i].Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return flota[i];
            }
            return null;
        }

        // ---------- Opción 1: Registro polimórfico ----------
        public void RegistrarVehiculo()
        {
            if (cantidad >= CapacidadMaxima)
            {
                Console.WriteLine("La flota está llena. No se pueden registrar más vehículos.");
                return;
            }

            Console.WriteLine("\n--- Registrar vehículo ---");
            Console.WriteLine("1. Automóvil");
            Console.WriteLine("2. Camioneta");
            Console.WriteLine("3. Motocicleta");
            int tipo = LeerEntero("Seleccione el tipo: ");
            while (tipo < 1 || tipo > 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Tipo inválido (1-3). ");
                Console.ResetColor();
                tipo = LeerEntero("Seleccione el tipo: ");
            }

            // Atributos generales
            string codigo = LeerTexto("Código: ");
            while (BuscarPorCodigo(codigo) != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ya existe un vehículo con ese código.");
                Console.ResetColor();
                codigo = LeerTexto("Código: ");
            }
            string marca = LeerTexto("Marca: ");
            string modelo = LeerTexto("Modelo: ");
            int anio = LeerEntero("Año: ");
            while (anio < 1950 || anio > DateTime.Now.Year + 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Año inválido (1950-{DateTime.Now.Year + 1}). ");
                Console.ResetColor();
                anio = LeerEntero("Año: ");
            }
            double km = LeerDouble("Kilometraje: ");
            double precio = LeerDouble("Precio de renta por día: ");

            Vehiculo nuevo;
            switch (tipo)
            {
                case 1:
                    int puertas = LeerEnteroPositivo("Número de puertas: ");
                    string transmision = LeerOpcion("Tipo de transmisión:", "Manual", "Automática");
                    string combustible = LeerOpcion("Tipo de combustible:", "Gasolina", "Diésel", "Híbrido");
                    bool aire = LeerSiNo("¿Tiene aire acondicionado?");
                    int pasajerosA = LeerEnteroPositivo("Número de pasajeros: ");
                    nuevo = new Automovil(codigo, marca, modelo, anio, km, precio,
                                          puertas, transmision, combustible, aire, pasajerosA);
                    break;
                case 2:
                    double carga = LeerDouble("Capacidad de carga (toneladas): ");
                    string traccion = LeerOpcion("Tipo de tracción:", "4x2", "4x4");
                    bool cabina = LeerSiNo("¿Tiene doble cabina?");
                    int pasajerosC = LeerEnteroPositivo("Número de pasajeros: ");
                    nuevo = new Camioneta(codigo, marca, modelo, anio, km, precio,
                                          carga, traccion, cabina, pasajerosC);
                    break;
                default:
                    int cc = LeerEnteroPositivo("Cilindrada (cc): ");
                    string tipoMoto = LeerOpcion("Tipo de motocicleta:", "Scooter", "Enduro", "Pistera");
                    bool baul = LeerSiNo("¿Tiene baúl?");
                    int cascos = LeerEnteroPositivo("Número de cascos: ");
                    nuevo = new Motocicleta(codigo, marca, modelo, anio, km, precio,
                                            cc, tipoMoto, baul, cascos);
                    break;
            }

            flota[cantidad++] = nuevo;
            Console.WriteLine($"\nVehículo {nuevo.Codigo} registrado correctamente.");
        }

        // ---------- Opción 2 ----------
        public void MostrarTodos()
        {
            Console.WriteLine("\n--- Todos los vehículos ---");
            if (cantidad == 0)
            {
                Console.WriteLine("No hay vehículos registrados.");
                return;
            }
            for (int i = 0; i < cantidad; i++)
                flota[i].MostrarInformacion(); // Polimorfismo: se ejecuta la versión de cada clase derivada
        }

        // ---------- Opción 3 ----------
        public void BuscarVehiculo()
        {
            Console.WriteLine("\n--- Buscar vehículo ---");
            string codigo = LeerTexto("Código a buscar: ");
            Vehiculo v = BuscarPorCodigo(codigo);
            if (v == null)
                Console.WriteLine("No se encontró ningún vehículo con ese código.");
            else
                v.MostrarInformacion();
        }

        // ---------- Opciones 4 y 5 ----------
        public void MostrarPorEstado(string estado)
        {
            Console.WriteLine($"\n--- Vehículos {estado.ToLower()}s ---");
            bool hay = false;
            for (int i = 0; i < cantidad; i++)
            {
                if (flota[i].Estado.Equals(estado, StringComparison.OrdinalIgnoreCase))
                {
                    flota[i].MostrarInformacion();
                    hay = true;
                }
            }
            if (!hay)
                Console.WriteLine($"No hay vehículos con estado '{estado}'.");
        }

        // ---------- Opción 6 ----------
        public void CambiarEstado()
        {
            Console.WriteLine("\n--- Cambiar estado ---");
            string codigo = LeerTexto("Código del vehículo: ");
            Vehiculo v = BuscarPorCodigo(codigo);
            if (v == null)
            {
                Console.WriteLine("No se encontró ningún vehículo con ese código.");
                return;
            }

            Console.WriteLine($"Estado actual: {v.Estado}");
            string nuevo = LeerOpcion("Nuevo estado:", "Disponible", "Rentado", "En Mantenimiento");

            if (nuevo == "En Mantenimiento")
                v.MarcarRequiereMantenimiento();
            else
                v.Estado = nuevo;

            Console.WriteLine($"Estado del vehículo {v.Codigo} actualizado a '{v.Estado}'.");
        }

        // ---------- Opción 7 ----------
        public void VehiculoMayorKilometraje()
        {
            Console.WriteLine("\n--- Vehículo con mayor kilometraje ---");
            if (cantidad == 0)
            {
                Console.WriteLine("No hay vehículos registrados.");
                return;
            }
            Vehiculo mayor = flota[0];
            foreach (var v in flota)
            {
                if (v != null && v.Kilometros > mayor.Kilometros)
                    mayor = v;
            }
            mayor.MostrarInformacion();
        }

        // ---------- Opción 8 ----------
        public void PrecioPromedioRenta()
        {
            Console.WriteLine("\n--- Precio promedio de renta ---");
            if (cantidad == 0)
            {
                Console.WriteLine("No hay vehículos registrados.");
                return;
            }
            double suma = 0;
            for (int i = 0; i < cantidad; i++)
                suma += flota[i].PrecioRenta;
            Console.WriteLine($"Precio promedio por día: ${suma / cantidad:F2}");
        }

        // ---------- Opción 9 ----------
        public void ContarPorTipo()
        {
            Console.WriteLine("\n--- Cantidad de vehículos por tipo ---");
            int autos = 0, camionetas = 0, motos = 0;
            foreach (var v in flota)
            {
                if (v is Automovil) autos++;
                else if (v is Camioneta) camionetas++;
                else if (v is Motocicleta) motos++;
            }
            Console.WriteLine($"Automóviles : {autos}");
            Console.WriteLine($"Camionetas  : {camionetas}");
            Console.WriteLine($"Motocicletas: {motos}");
        }

        // ---------- Opción 10 ----------
        public void RealizarMantenimiento()
        {
            Console.WriteLine("\n--- Mantenimiento ---");
            string codigo = LeerTexto("Código del vehículo: ");
            Vehiculo v = BuscarPorCodigo(codigo);
            if (v == null)
            {
                Console.WriteLine("No se encontró ningún vehículo con ese código.");
                return;
            }

            // Se usa a través del contrato de la interfaz IMantenimiento
            IMantenimiento m = v;
            m.ConsultarMantenimiento();

            if (LeerSiNo("¿Desea realizar el mantenimiento ahora?"))
            {
                m.RealizarMantenimiento();
                m.ConsultarMantenimiento();
            }
        }
    }
}
