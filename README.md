# IoT Fan Simulator

IoT Fan Simulator är ett projekt som demonstrerar en IoT-lösning med WPF och MAUI/Blazor hybrid. Projektet inkluderar funktioner för att hantera en IoT-enhet, skicka dess status till Azure IoT Hub, samt lagra och uppdatera inställningar som ConnectionString och Email. Projektet uppfyller även krav för e-postaviseringar när en enhet tas bort med hjälp av Azure Communication Services.

## Funktioner

### WPF-applikation
- **Enhetshantering:** Hantering av IoT-enhetens tillstånd med hjälp av MVVM och Azure IoT Hub.
- **Inställningssida:** Lagring av ConnectionString, DeviceId och Email i en JSON-baserad lagringslösning.
- **Rapportering till IoT Hub:** Enhetens tillstånd skickas till enhetens device twin i Azure IoT Hub.

### MAUI/Blazor-applikation
- **Lista IoT-enheter:** Alla IoT-enheter visas på startsidan.
- **Inställningssida:** Uppdatering av ConnectionString och Email via MVVM.
- **E-postaviseringar:** När en IoT-enhet tas bort skickas ett e-postmeddelande till en angiven adress med hjälp av Azure Communication Services.

## Installation

### Förkrav
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Ett Azure-konto med tillgång till IoT Hub och Communication Services.
