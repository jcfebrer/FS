Conjunto de librerias utilizadas por las aplicaciones FebrerSoftware.


----------------------------------------------------------------------------
#INSTALACIÓN DE NETCORE

    sudo pacman -S dotnet-runtime[-6.0][-8.0] sudo pacman -S dotnet-sdk

#COMPILAR (estando en la carpeta de .csproj)

    dotnet publish -c release -r linux-x64 --framework net8.0
