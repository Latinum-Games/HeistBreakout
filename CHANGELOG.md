# Change Log
## Version 0.4beta - Multiplayer Rework - (BRANCH CON MAS AVANCE -> multiplayer-inventory)
### Cambios Mayores
- Añadir Modo de Juego Multijugador a Heist Ops con Photon
  - Creacion de Rooms para crear partida
  - Movimiento de personajes sincronizado
  - Punto de Extraccion + condicion de victoria
  - Condiciones de derrota
  - Interacciones con inteligencia artificial
- Rediseño de Interfaz de Menu Principal
  - Nuevo UI
  - Animaciones con Lean Tween

### Cambios Menores
- La skin de seleccion de personaje se refleja de manera correcta In Game
- Musica de Fondo en el menu principal :D

### Bugs y Mecanicas rotas
- Interactuar/recoger objetos del mapa en multiplayer no funciona todavia
- La deteccion del ruido de los personajes por los enemigos no funciona en multiplayer
- Los enemigos en multiplayer no persiguen al jugador
- UX del menu de pausa no esta implementado completamemnte

### Archivos importantes para este changelog
- Assets/Multiplayer/Scenes/SceneMainMenu
- Assets/Prefabs/Enemy/EnemyMulti
- Assets/Resources/Images/Maga
- Assets/Resources/Images/Mapache_ladrón
- Assets/Resources/Images/Robot
- Assets/Resources/Images/Slime.
- Assets/Resources/Music/Heist Breakout - Menu 1 Full Song 
- Assets/Resources/FieldOfViewMulti
- Assets/Resources/PlayerController2.0 Cat
- Assets/Resources/PlayerController2.0 Elf
- Assets/Scenes/UI Heist Breakout
- Assets/Scenes/Showcase Multiplayer/VrMissionMulti1
- Assets/Scenes/Showcase Multiplayer/VrMissionMulti1/Nav Mesh-Nav Mesh 1
- Assets/Scenes/Showcase Multiplayer/ VrMissionMulti1/Nav Mesh-Navmesh
- Assets/Scripts/Controllers/InputControllerMulti
- Assets/Scripts/Deprecated/Player/LoadPlayerVisual
- Assets/Scripts/Deprecated/Player/PlayerController
- Assets/Scripts/GameModes/Heist Ops/Multiplayer/VrMissionManagerMulti
- Assets/Scripts/Inventory/InventoryMulti
- Assets/Scripts/Player/PlayerController2.0/MovementV2Multi
- Assets/Scripts/UI/Menu/CharacterSelection
- Assets/Scripts/UI/Menu/ExitMenuMulti 
- Assets/Scripts/UI/Menu/LobbyMenuMulti
- Assets/Scripts/UI/Menu/MainMenu
- Assets/Scripts/UI/Menu/Menu
- Assets/Scripts/UI/Menu/MenuManager
- Assets/Scripts/UI/AnimateUI
- Assets/Scripts/_NetworkManager
- Assets/Scripts/MenuUIController
