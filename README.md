# GestionVehiculos

Aplicación de consola en C# para gestionar una flota de vehículos de renta, aplicando POO: abstracción, herencia, polimorfismo (`virtual`/`override`), interfaces (`IMantenimiento`), casting y operador `is`.

## Ejecutar
```bash
dotnet run
```

## Estructura
- `Interfaces/IMantenimiento.cs`
- `Modelos/` → `Vehiculo` (abstracta), `Automovil`, `Camioneta`, `Motocicleta`
- `Servicios/GestorFlota.cs` → lógica de las 11 opciones del menú
- `Program.cs` → menú con `do-while` + `switch`

## Subir a GitHub
```bash
git init
git add .
git commit -m "Proyecto inicial: GestionVehiculos"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/GestionVehiculos.git
git push -u origin main
```
