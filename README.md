# TetrisPc

Este proyecto es un clon de Tetris desarrollado en C# usando Windows Forms.

## Ejecución del proyecto

Para ejecutar el proyecto, primero debes compilarlo. Sigue estos pasos desde la terminal en la raíz del repositorio:

1. Compila la solución:

```
dotnet build TetrisWinForms.sln
```

2. Ejecuta el proyecto:

```
dotnet run --project TetrisWinForms/TetrisWinForms.csproj
```

Esto generará el ejecutable y abrirá la ventana del juego.

**Nota:** No subas los archivos de la carpeta `bin/` ni `obj/` al repositorio, ya que son archivos generados automáticamente.

## Cómo generar un archivo .exe en el escritorio

Para crear un ejecutable (.exe) en el escritorio de tu usuario, ejecuta el siguiente comando en la raíz del repositorio (reemplaza `USUARIO` por tu nombre de usuario de Windows si es necesario):

```
dotnet publish TetrisWinForms/TetrisWinForms.csproj -c Release -r win-x64 -o "%USERPROFILE%\Desktop\TetrisPcExe"
```

Esto creará una carpeta `TetrisPcExe` en tu escritorio con el ejecutable listo para usar.
